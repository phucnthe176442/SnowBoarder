using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using snow_boarder.Core;

namespace snow_boarder.UI
{
    public class LoadSceneData
    {
        public string sceneName;
        public float minLoadTime;
        public Func<bool> launchCondition;
        public Action onCompleted;
    }

    public class LoadingView : SingletonDontDestroy<LoadingView>
    {
        [SerializeField] private RectTransform[] texts;
        [SerializeField] private float introDelay = 22f;
        [SerializeField] private GameObject viewGo;
        [SerializeField] private Image progress;

        private bool _loading;
        private int _textIndex;
        private float _lastTimeChangeText;
        private WaitForSeconds _delayBeforeLoad;

        private void Start()
        {
            _delayBeforeLoad = new WaitForSeconds(0.5f);
            LoadScene(new LoadSceneData()
            {
                sceneName = Constant.MENU_SCENE,
                minLoadTime = introDelay,
                launchCondition = () => true,
                onCompleted = null
            });
        }

        public void LoadScene(LoadSceneData data)
        {
            StartCoroutine(IeLoadScene(data));
        }

        private IEnumerator IeLoadScene(LoadSceneData data)
        {
            if (_loading) yield break;
            _loading = true;
            viewGo.gameObject.SetActive(true);

            var ao = SceneManager.LoadSceneAsync(data.sceneName, LoadSceneMode.Single);
            ao.allowSceneActivation = false;

            var t = 0f;

            while (t < data.minLoadTime || ao.progress < 0.9f)
            {
                if (!_loading) t = data.minLoadTime - Time.unscaledDeltaTime;
                if (Time.time - _lastTimeChangeText >= 0.35f)
                {
                    _lastTimeChangeText = Time.time;
                    texts[_textIndex].gameObject.SetActive(false);
                    _textIndex++;
                    if (_textIndex == texts.Length) _textIndex = 0;
                    texts[_textIndex].gameObject.SetActive(true);
                }
                t += Time.unscaledDeltaTime;
                var progressValue = Mathf.Min(t / data.minLoadTime, ao.progress / 0.9f);
                progress.transform.localScale = new Vector3(progressValue, 1f, 1f);

                yield return null;
            }

            if (data.launchCondition != null)
            {
                yield return new WaitUntil(data.launchCondition);
            }

            yield return _delayBeforeLoad;
            ao.allowSceneActivation = true;
            yield return null;
            _loading = false;
            data.onCompleted?.Invoke();
            viewGo.gameObject.SetActive(false);
        }

        private void Update()
        {
            DOTween.ManualUpdate(Time.deltaTime, Time.unscaledDeltaTime);
        }
    }
}