using TMPro;
using UnityEngine;

namespace snow_boarder
{
    public class RotationText : MonoBehaviour
    {
        [SerializeField, Space] private TextMeshProUGUI text;

        private void OnEnable()
        {
            GameManager.Instance.OnChangedScore += OnChanged;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnChangedScore -= OnChanged;
        }

        private void OnChanged(float value)
        {
            text.text = $"Rotation time: {(value - GameManager.Instance.Star).ToString("F2")}";
        }
    }
}