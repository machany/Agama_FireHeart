using Agama.Scripts.Events;
using DG.Tweening;
using Scripts.EventChannel;
using Scripts.UI.Inven.ItemPanelUI;
using UnityEngine;

namespace Scripts.UI
{
    public class MoveUI : MonoBehaviour
    {
        [field: SerializeField] public UIType myType { get; protected set; }
        [SerializeField] protected float _parentHeight = 800f;
        [SerializeField] protected EventChannelSO _uiChannel;
        private PanelUI[] _panels;

        protected RectTransform _rectTrm;
        protected Vector2 _initPosition;
        public virtual void Initialize()
        {
            _panels = GetComponentsInChildren<PanelUI>();
            _rectTrm = transform as RectTransform;
        }

        protected virtual void Start()
        {
            SetPanels(false);
            _initPosition = _rectTrm.anchoredPosition;
            _rectTrm.anchoredPosition = _initPosition + new Vector2(0f, -_parentHeight);
        }

        public virtual void Open()
        {
            SetPanels(true);
            _rectTrm.DOAnchorPos(_initPosition, 0.5f).SetUpdate(true);
        }

        public virtual void Close()
        {
            Vector2 hidePosition = _initPosition + new Vector2(0f, -_parentHeight); ;
            _rectTrm.DOAnchorPos(hidePosition, 0.5f).SetUpdate(true).OnComplete(()=>SetPanels(false));
        }
        private void SetPanels(bool active)
        {
            foreach (var item in _panels)
                item.gameObject.SetActive(active);
        }
    }
}
