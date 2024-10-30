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

        private void OnSeledtedLevel()
        {
            Controller.gameObject.SetActive(false);
        }
    }
}