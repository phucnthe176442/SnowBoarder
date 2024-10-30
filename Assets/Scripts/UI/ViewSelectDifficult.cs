namespace snow_boarder.UI
{
    public class ViewSelectDifficult : ViewBase
    {
        public void OnSelectLevel(int level)
        {
            Controller.gameObject.SetActive(false);
            var diff = level switch
            {
                1 => ELevelDifficult.Easy,
                2 => ELevelDifficult.Medium,
                3 => ELevelDifficult.Hard,
                _ => throw new System.Exception("missing difficult level")
            };
            LoadingView.Instance.LoadScene(new LoadSceneData()
            {
                sceneName = Constant.GAMEPLAY_SCENE,
                minLoadTime = 1f,
                launchCondition = () => true,
                onCompleted = () => GameManager.Instance.Setup(diff, level),
            });
        }
    }
}