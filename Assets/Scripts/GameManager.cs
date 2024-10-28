using snow_boarder.Core;
using System;
using System.Linq;
using UnityEngine;

namespace snow_boarder
{
    [Serializable]
    public class LevelBinding
    {
        public ELevelDifficult difficult;
        public GameObject groundPrefab;
    }

    public class GameManager : SingletonDontDestroy<GameManager>
    {
        [SerializeField] private LevelBinding[] configs;

        public event Action<float> OnChangedHighestScore;
        public float HighestScore
        {
            get => PlayerPrefs.GetFloat("highest_score", 0f);
            set
            {
                PlayerPrefs.SetFloat("highest_score", value);
                OnChangedHighestScore?.Invoke(value);
            }
        }
        private float _score;
        public event Action<float> OnChangedScore;
        public float Score
        {
            get => _score;
            set
            {
                if (value >= HighestScore) HighestScore = value;
                _score = value;
                OnChangedScore?.Invoke(value);
            }
        }

        public void Setup(ELevelDifficult difficult)
        {
            var config = configs.First(c => c.difficult == difficult);
            var ground = Instantiate(config.groundPrefab);
            ground.transform.position = Vector3.zero;
            Debug.LogError("???");
        }
    }

    public enum ELevelDifficult
    {
        Easy = 0,
        Medium = 1,
        Hard = 2,
    }
}