using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DevPeixoto.UI.GlobalUiEvents
{
    [RequireComponent(typeof(Selectable))]
    public abstract class UiEventForwarderBase<T>: MonoBehaviour, IPointerClickHandler, ISubmitHandler, IMoveHandler, ISelectHandler where T : GlobalEventsData
    {
        [HideInInspector] public UnityEvent<GlobalEventsData> onClick = new UnityEvent<GlobalEventsData>();
        [HideInInspector] public UnityEvent<GlobalEventsData> onMove = new UnityEvent<GlobalEventsData>();

        internal GlobalUiEventsManager manager;
        protected Selectable selectable;
        protected GameObject caller;
        protected SelectableEventOverrider overrider;
        protected static T cachedGlobalEvntData;

        public Selectable Selectable
        {
            get
            {
                if (selectable == null)
                    selectable = GetComponent<Selectable>();

                return selectable;
            }
        }

        public SelectableEventOverrider Overrider { get => overrider; }

        protected virtual void Awake()
        {
            caller = gameObject;
            selectable = GetComponent<Selectable>();
        }

        public virtual void OnSubmit(BaseEventData eventData)
        {                
            cachedGlobalEvntData.caller = selectable;
            cachedGlobalEvntData.overrider = overrider;
            cachedGlobalEvntData.eventData = eventData;
            onClick?.Invoke(cachedGlobalEvntData);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            cachedGlobalEvntData.caller = selectable;
            cachedGlobalEvntData.overrider = overrider;
            cachedGlobalEvntData.eventData = eventData;
            onClick?.Invoke(cachedGlobalEvntData);
        }

        public virtual void OnMove(AxisEventData eventData)
        {
            cachedGlobalEvntData.caller = selectable;

            int instanceId = eventData.selectedObject.GetInstanceID();
            SelectableEventOverrider selectedOverrider = manager.GetOverrider(instanceId);
            cachedGlobalEvntData.overrider = selectedOverrider;

            cachedGlobalEvntData.eventData = eventData;
            onMove?.Invoke(cachedGlobalEvntData);
        }

        public virtual void OnSelect(BaseEventData eventData)
        {
            cachedGlobalEvntData.caller = selectable;
            cachedGlobalEvntData.overrider = overrider;
            cachedGlobalEvntData.eventData = eventData;
        }

        protected virtual void OnDisable()
        {
            onClick?.RemoveAllListeners();
            onMove?.RemoveAllListeners();
        }
    }

    public class GlobalEventsData
    {
        public Selectable caller;
        public SelectableEventOverrider overrider;
        public BaseEventData eventData;
    }
}