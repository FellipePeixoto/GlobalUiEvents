using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DevPeixoto.UI.GlobalUiEvents
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    [AddComponentMenu("DevPeixoto/Global UI Events/Selectables Listener Manager")]
    public class SelectablesListenerManager : MonoBehaviour
    {
        [Header("Events")]
        public UnityEvent onSelect;

        [Header("Sounds")]
        [SerializeField] AudioPresetSO _audioPreset;

        Dictionary<int, SelectableEventOverrider> overridersDict = new();

        AudioSrcPool _audioSrcPool;

        private void Awake()
        {
            SetupSelectables();
            _audioSrcPool = AudioSrcPool.Instance;
            _audioSrcPool.SetMixer(_audioPreset.Mixer);
        }

        void SetupSelectables()
        {
            Selectable[] selectables = GetComponentsInChildren<Selectable>(true);
            foreach (Selectable selectable in selectables)
            {
                AddSelectable(selectable);
            }
        }

        // REVIEW: It Should remove the unused as well?
        public void RefreshSelectables()
        {
            GameObject[] included = overridersDict.Values.Select(x => x.gameObject).ToArray();
            Selectable[] all = GetComponentsInChildren<Selectable>(true);

            foreach (Selectable item in all)
            {
                if (overridersDict.ContainsKey(item.GetInstanceID()))
                    continue;

                AddSelectable(item);
            }
        }

        void AddSelectable(Selectable selectable)
        {
            switch (selectable)
            {
                case Slider slider:
                    var sliderEvntFwd = selectable.gameObject.AddComponent<SliderEventForwarder>();
                    sliderEvntFwd.manager = this;
                    sliderEvntFwd.Init();
                    sliderEvntFwd.onClick.AddListener(OnClick);
                    sliderEvntFwd.onMove.AddListener(OnMove);

                    if (sliderEvntFwd.Overrider != null)
                        overridersDict.Add(selectable.gameObject.GetInstanceID(), sliderEvntFwd.Overrider);
                    break;

                default:
                    var genericEvntFwd = selectable.gameObject.AddComponent<SelectableEventForwarder>();
                    genericEvntFwd.manager = this;
                    genericEvntFwd.Init();
                    genericEvntFwd.onClick.AddListener(OnClick);
                    genericEvntFwd.onMove.AddListener(OnMove);
                    
                    if (genericEvntFwd.Overrider != null)
                        overridersDict.Add(selectable.gameObject.GetInstanceID(), genericEvntFwd.Overrider);
                    break;
            }
        }

        void OnMove(GlobalEventsData eventData)
        {
            if ((eventData.eventData is AxisEventData axisEventData) && (eventData.caller is Slider slider))
            {
                if (axisEventData.moveDir == MoveDirection.Right || axisEventData.moveDir == MoveDirection.Left)
                {
                    SliderEventOverrider sliderOverrider = eventData.overrider as SliderEventOverrider;

                    if (sliderOverrider != null)
                        sliderOverrider.onMove?.Invoke();

                    if (sliderOverrider != null && sliderOverrider.overrideSlideAudio)
                    {
                        _audioSrcPool.PlayAudio(sliderOverrider.slideAudioClip);
                    }
                    else if (sliderOverrider != null)
                    {
                        _audioSrcPool.PlayAudio(sliderOverrider.slideAudioClip);
                        _audioSrcPool.PlayAudio(_audioPreset.SlideAudio);
                    }
                    else
                    {
                        _audioSrcPool.PlayAudio(_audioPreset.SlideAudio);
                    }

                    return;
                }
            }

            if (eventData.caller.gameObject != eventData.eventData.selectedObject)
            {
                if (eventData.overrider != null && eventData.overrider.overrideSelectEvent)
                {
                    eventData.overrider.onSelect?.Invoke();
                }
                else if (eventData.overrider != null)
                {
                    eventData.overrider.onSelect?.Invoke();
                    onSelect?.Invoke();
                }
                else
                {
                    onSelect?.Invoke();
                }

                if (eventData.overrider != null && eventData.overrider.overrideSelectAudio)
                {
                    _audioSrcPool.PlayAudio(eventData.overrider.selectAudioClip);
                }
                else if (eventData.overrider != null)
                {
                    _audioSrcPool.PlayAudio(eventData.overrider.selectAudioClip);
                    _audioSrcPool.PlayAudio(_audioPreset.SelectAudio);
                }
                else
                {
                    _audioSrcPool.PlayAudio(_audioPreset.SelectAudio);
                }
            }
            else
            {
                _audioSrcPool.PlayAudio(_audioPreset.NoNextAudio);
            }
        }

        void OnClick(GlobalEventsData eventData)
        {
            if (eventData.caller.IsInteractable())
            {
                if (eventData.overrider != null && eventData.overrider.overrideClickAudio)
                {
                    _audioSrcPool.PlayAudio(eventData.overrider.clickAudioClip);
                }
                else if (eventData.overrider != null)
                {
                    _audioSrcPool.PlayAudio(eventData.overrider.clickAudioClip);
                    _audioSrcPool.PlayAudio(_audioPreset.ClickAudio);
                }
                else
                {
                    _audioSrcPool.PlayAudio(_audioPreset.ClickAudio);
                }
            }
            else
            {
                if (eventData.overrider != null && eventData.overrider.overrideClickDisabledAudio)
                {
                    _audioSrcPool.PlayAudio(eventData.overrider.clickDisabledAudioClip);
                }
                else if (eventData.overrider != null)
                {
                    _audioSrcPool.PlayAudio(eventData.overrider.clickDisabledAudioClip);
                    _audioSrcPool.PlayAudio(_audioPreset.ClickDisabledAudio);
                }
                else
                {
                    _audioSrcPool.PlayAudio(_audioPreset.ClickDisabledAudio);
                }
            }
        }

        public SelectableEventOverrider GetOverrider(int instanceId)
        {

            if (!overridersDict.ContainsKey(instanceId))
                return null;

            if (overridersDict[instanceId] == null)
            {
                overridersDict.Remove(instanceId);
                return null;
            }

            return overridersDict[instanceId];
        }
    }
}