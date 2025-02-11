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

        protected RectTransform _rectTrm;
        protected Vector2 _initPosition;
        public virtual void Initialize()
        {
            _rectTrm = transform as RectTransform;
        }

        protected virtual void Start()
        {
            _initPosition = _rectTrm.anchoredPosition;
            _rectTrm.anchoredPosition = _initPosition + new Vector2(0f, -_parentHeight);
        }

        public virtual void Open()
        {
            _rectTrm.DOAnchorPos(_initPosition, 0.5f).SetUpdate(true);
        }

        public virtual void Close()
        {
            Vector2 hidePosition = _initPosition + new Vector2(0f, -_parentHeight); ;
            _rectTrm.DOAnchorPos(hidePosition, 0.5f).SetUpdate(true);
        }

    }
}
