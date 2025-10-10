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

        internal SelectablesListenerManager manager;
        protected Selectable selectable;
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

        public abstract void Init();

        public virtual void OnSubmit(BaseEventData eventData)
        {                
            cachedGlobalEvntData.caller = Selectable;
            cachedGlobalEvntData.overrider = overrider;
            cachedGlobalEvntData.eventData = eventData;
            onClick?.Invoke(cachedGlobalEvntData);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            cachedGlobalEvntData.caller = Selectable;
            cachedGlobalEvntData.overrider = overrider;
            cachedGlobalEvntData.eventData = eventData;
            onClick?.Invoke(cachedGlobalEvntData);
        }

        public virtual void OnMove(AxisEventData eventData)
        {
            cachedGlobalEvntData.caller = Selectable;

            // The Overrider must be the selected object and not the caller
            int instanceId = eventData.selectedObject.GetInstanceID();
            // Get the overrider hold by the his manager
            SelectableEventOverrider selectedOverrider = manager.GetOverrider(instanceId);
            cachedGlobalEvntData.overrider = selectedOverrider;

            cachedGlobalEvntData.eventData = eventData;
            onMove?.Invoke(cachedGlobalEvntData);
        }

        public virtual void OnSelect(BaseEventData eventData)
        {
            cachedGlobalEvntData.caller = Selectable;
            cachedGlobalEvntData.overrider = overrider;
            cachedGlobalEvntData.eventData = eventData;
        }

        protected virtual void OnDestroy()
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