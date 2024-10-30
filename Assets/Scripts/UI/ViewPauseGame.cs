using UnityEngine;

namespace snow_boarder.UI
{
    public class ViewPauseGame : ViewBase
    {
        public void OnClickHome()
        {
            Time.timeScale = 1f;
            Controller.gameObject.SetActive(false);
            LoadingView.Instance.LoadScene(new LoadSceneData()
            {
                sceneName = Constant.MENU_SCENE,
                minLoadTime = 2f,
                launchCondition = () => true,
                onCompleted = null,
            });
        }

        public void OnClickSetting()
        {
            Controller.Show<ViewSettings>(true);
        }

        protected override void BeforeShow(object data = null)
        {
            base.BeforeShow(data);
            Time.timeScale = 0f;
        }

        public override void Close()
        {
            base.Close();
            Time.timeScale = 1f;
        }
    }
}