
namespace DevPeixoto.UI.GlobalUiEvents
{
    public class SliderEventForwarder : UiEventForwarderBase<SliderGlobalEvntsData> 
    {
        protected override void Awake()
        {
            base.Awake();

            overrider = GetComponent<SelectableEventOverrider>();
            if (cachedGlobalEvntData == null)
                cachedGlobalEvntData = new SliderGlobalEvntsData();
        }
    }

    public class SliderGlobalEvntsData : GlobalEventsData { }
}