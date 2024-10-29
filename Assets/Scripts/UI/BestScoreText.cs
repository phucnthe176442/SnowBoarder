using TMPro;
using UnityEngine;

namespace snow_boarder
{
    public class BestScoreText : MonoBehaviour
    {
        [SerializeField, Space] private TextMeshProUGUI text;

        private void OnEnable()
        {
            OnChanged(GameManager.Instance.HighestScore);
            GameManager.Instance.OnChangedHighestScore += OnChanged;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnChangedHighestScore -= OnChanged;
        }

        private void OnChanged(float value)
        {
            text.text = $"Highest Score: {value.ToString("F2")}";
        }
    }
}