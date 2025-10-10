
namespace DevPeixoto.UI.GlobalUiEvents
{
    public class SelectableEventForwarder : UiEventForwarderBase<SelectableGlobalEvntsData> 
    {
        public override void Init()
        {
            overrider = GetComponent<SelectableEventOverrider>();
            if (cachedGlobalEvntData == null)
                cachedGlobalEvntData = new SelectableGlobalEvntsData();
        }
    }
    
    public class SelectableGlobalEvntsData: GlobalEventsData { }
}