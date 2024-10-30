using UnityEngine;

namespace snow_boarder.UI
{
    public class ViewMainMenu : ViewBase
    {
        public void OnClickGuide() => Controller.Show<ViewGuide>(true);

        public void OnClickPlay() => Controller.Show<ViewSelectDifficult>(true);

        public void OnClickSettings() => Controller.Show<ViewSettings>(true);

        public void OnClickQuit() => Application.Quit();
    }
}