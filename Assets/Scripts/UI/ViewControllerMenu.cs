namespace snow_boarder.UI
{
    public class ViewControllerMenu : ViewControllerBase
    {
        protected override void Initialize()
        {
            base.Initialize();

            Show<ViewMainMenu>(false);
        }
    }
}