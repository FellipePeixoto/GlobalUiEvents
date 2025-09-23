
namespace DevPeixoto.UI.GlobalUiEvents
{
    public class GenericEventForwarder : UiEventForwarderBase<GenericGlobalEvntsData> 
    {
        protected override void Awake()
        {
            base.Awake();

            overrider = GetComponent<SelectableEventOverrider>();
            if (cachedGlobalEvntData == null)
                cachedGlobalEvntData = new GenericGlobalEvntsData();
        }
    }
    
    public class GenericGlobalEvntsData: GlobalEventsData { }
}