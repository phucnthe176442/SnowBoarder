using TMPro;
using UnityEngine;

namespace snow_boarder.UI
{
    public class StarText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;
        private void OnEnable()
        {
            GameManager.Instance.OnChangedStar += OnChanged;
        }

        private void OnChanged(float obj)
        {
            text.text = $"<sprite=0/>: {(int)obj}";
        }

        private void OnDisable()
        {
            GameManager.Instance.OnChangedStar -= OnChanged;
        }
    }
}