namespace snow_boarder.UI
{
    public class ViewControllerGameplay : ViewControllerBase
    {
        protected override void Initialize()
        {
            base.Initialize();

            Show<ViewGameplay>(false);
        }
    }
}