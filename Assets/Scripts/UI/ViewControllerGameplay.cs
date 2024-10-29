using System.Collections;
using UnityEngine;

namespace snow_boarder.UI
{
    public class ViewControllerGameplay : ViewControllerBase
    {
        protected override void Initialize()
        {
            base.Initialize();

            Show<ViewGameplay>(false);
        }

        private void OnEnable()
        {
            GameManager.Instance.OnGameEnd += OnEndGame;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnGameEnd -= OnEndGame;
        }

        private void OnEndGame()
        {
            StartCoroutine(IeDelay());
        }

        private IEnumerator IeDelay()
        {
            yield return new WaitForSeconds(0.5f);
            Show<ViewEndGame>(false, EShowAction.PauseCurrent);
        }
    }
}