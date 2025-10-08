
namespace DevPeixoto.UI.GlobalUiEvents
{
    public class SliderEventForwarder : UiEventForwarderBase<SliderGlobalEvntsData> 
    {
        public override void Init()
        {
            overrider = GetComponent<SliderEventOverrider>();
            if (cachedGlobalEvntData == null)
                cachedGlobalEvntData = new SliderGlobalEvntsData();
        }
    }

    public class SliderGlobalEvntsData : GlobalEventsData { }
}