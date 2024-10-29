namespace snow_boarder.UI
{
    public class ViewEndGame : ViewBase
    {
        public void GoHome()
        {
            LoadingView.Instance.LoadScene(new LoadSceneData()
            {
                sceneName = Constant.MENU_SCENE,
                minLoadTime = 2f,
                launchCondition = () => true,
                onCompleted = null
            });
            Controller.gameObject.SetActive(false);
        }
    }
}