using snow_boarder.UI;
using System;
using UnityEngine;

namespace snow_boarder
{
    public class LevelOptions : MonoBehaviour
    {
        public event Action<ELevelDifficult> OnLoadSceneEvent;

        public void OnSelectEasy()
        {
            OnSelected(ELevelDifficult.Easy);
        }

        public void OnSelectMedium()
        {
            OnSelected(ELevelDifficult.Medium);
        }

        public void OnSelectHard()
        {
            OnSelected(ELevelDifficult.Hard);
        }

        private void OnSelected(ELevelDifficult difficult)
        {
            OnLoadSceneEvent?.Invoke(difficult);
        }
    }
}
