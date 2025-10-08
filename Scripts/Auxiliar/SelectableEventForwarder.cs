
namespace DevPeixoto.UI.GlobalUiEvents
{
    public class SelectableEventForwarder : UiEventForwarderBase<GenericGlobalEvntsData> 
    {
        public override void Init()
        {
            overrider = GetComponent<SelectableEventOverrider>();
            if (cachedGlobalEvntData == null)
                cachedGlobalEvntData = new GenericGlobalEvntsData();
        }
    }
    
    public class GenericGlobalEvntsData: GlobalEventsData { }
}