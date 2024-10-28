using System.Collections.Generic;
using System;
using UnityEngine;

namespace snow_boarder.UI
{
    [RequireComponent(typeof(Canvas))]
    public class ViewControllerBase : MonoBehaviour
    {
        [SerializeField] ViewBase[] popupPrefabs;
        [SerializeField] bool manualInitialize;

        public ViewBase Current => _popups.Last?.Value;

        private Canvas _canvas;
        private Dictionary<Type, ViewBase> _popupDict;
        private LinkedList<ViewBase> _popups;

        private bool _initialized;

        private void Start()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            if (_initialized) return;

            _canvas = GetComponent<Canvas>();

            _popupDict = new Dictionary<Type, ViewBase>();
            _popups = new LinkedList<ViewBase>();
            if (!manualInitialize)
            {
                foreach (var prefab in popupPrefabs)
                {
                    var popup = Instantiate(prefab, transform);
                    popup.name = prefab.name;
                }
            }

            var ps = GetComponentsInChildren<ViewBase>(true);
            foreach (var popup in ps)
            {
                popup.gameObject.SetActive(false);

                popup.Initialize(this);
                popup.SetOrder(_canvas.sortingOrder + 1);

                var type = popup.GetType();
                _popupDict.Add(type, popup);
            }

            _initialized = true;
        }

        public void Show<T>(bool animated, EShowAction showAction = EShowAction.DoNothing, object data = null)
        {
            if (!_popupDict.TryGetValue(typeof(T), out var popup))
            {
                Debug.Log("Cannot find that popup!");
                return;
            }

            Show(popup, animated, showAction, data);
        }

        public void Show(ViewBase basePopup, bool animated, EShowAction showAction = EShowAction.DoNothing, object data = null)
        {
            var t = GetTopPopup();
            if (t == basePopup) return;

            if (t != null)
            {
                switch (showAction)
                {
                    case EShowAction.DoNothing:
                        break;
                    case EShowAction.DismissCurrent:
                        RemoveLast();
                        t.Dismiss(animated);
                        break;
                    case EShowAction.PauseCurrent:
                        t.Pause(animated);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(showAction), showAction, null);
                }
            }

            AddLast(basePopup);

            basePopup.Show(animated, data);
        }

        public void Dismiss<T>(bool animated)
        {
            if (!_popupDict.TryGetValue(typeof(T), out var popup))
            {
                Debug.Log("Cannot find that popup!");
                return;
            }

            Dismiss(popup, animated);
        }

        public void DismissCurrent(bool animated)
        {
            var last = _popups.Last;
            if (last != null)
            {
                Dismiss(last.Value, animated);
            }
        }

        public void Dismiss(ViewBase basePopup, bool animated)
        {
            if (Remove(basePopup))
            {
                basePopup.Dismiss(animated);
                var t = GetTopPopup();
                if (t == null) return;

                t.Resume(animated);
            }
        }

        void Reorder()
        {
            var p = _popups.First;
            var i = _canvas.sortingOrder;
            while (p != null)
            {
                p.Value.SetOrder(++i);
                p = p.Next;
            }
        }

        void AddLast(ViewBase basePopup)
        {
            if (_popups.Contains(basePopup))
            {
                _popups.Remove(basePopup);
            }

            _popups.AddLast(basePopup);
            Reorder();
        }

        bool Remove(ViewBase basePopup)
        {
            if (_popups.Remove(basePopup))
            {
                Reorder();
                return true;
            }

            return false;
        }

        void RemoveLast()
        {
            _popups.RemoveLast();
            Reorder();
        }

        ViewBase GetTopPopup() { return _popups.Last?.Value; }

        public T GetPopup<T>() where T : ViewBase { return _popupDict[typeof(T)] as T; }

        public enum EShowAction
        {
            DoNothing,
            DismissCurrent,
            PauseCurrent
        }
    }
}