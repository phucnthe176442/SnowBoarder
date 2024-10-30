using UnityEngine;

namespace snow_boarder.UI
{
    public class ViewSelectLevels : ViewBase
    {
        [SerializeField] private LevelOptions levelOptions;

        private void OnEnable()
        {
            levelOptions.OnLoadSceneEvent += OnSeledtedLevel;
        }

        private void OnDisable()
        {
            levelOptions.OnLoadSceneEvent -= OnSeledtedLevel; 
        }

        private void OnSeledtedLevel(ELevelDifficult diff)
        {
            Controller.Show<ViewSelectDifficult>(true, ViewControllerBase.EShowAction.DoNothing, diff);
        }
    }
}