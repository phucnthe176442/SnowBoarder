using UnityEngine;

namespace snow_boarder.UI
{
    public class ViewSelectLevels : ViewBase
    {
        [SerializeField] private LevelOptions levelOptions;

        private void OnEnable()
        {
            levelOptions.OnLoadSceneEvent += Close;
        }

        private void OnDisable()
        {
            levelOptions.OnLoadSceneEvent -= Close; 
        }

        public override void Close()
        {
            base.Close();

            Controller.gameObject.SetActive(false);
        }
    }
}