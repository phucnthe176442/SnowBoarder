namespace snow_boarder.UI
{
    public class ViewGameplay : ViewBase
    {
        public void OnClickPause()
        {
            Controller.Show<ViewPauseGame>(true);
        }
    }
}