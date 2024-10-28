namespace snow_boarder.UI
{
    public class ViewMainMenu : ViewBase
    {
        public void OnClickPlay()
        {

        }

        public void OnClickSettings() => Controller.Show<ViewSettings>(true);
    }
}