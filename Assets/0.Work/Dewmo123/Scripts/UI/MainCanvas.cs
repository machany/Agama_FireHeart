using Agama.Scripts.Core;
using Agama.Scripts.Events;
using DG.Tweening;
using Scripts.EventChannel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.UI
{
    public class MainCanvas : MonoBehaviour
    {
        public enum UIWindowStatus
        {
            Closed, Closing, Opening, Opened
        }

        [SerializeField] private EventChannelSO _uiEventChannel;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private PlayerInputSO _playerInput;

        private UIWindowStatus _uiWindowStatus = UIWindowStatus.Closed;

        private Dictionary<UIType, MoveUI> _menuPanels;
        private MoveUI _currentUI;

        private void Awake()
        {
            //_uiEventChannel.AddListener<OpenInventoryEvent>(HandleOpenInventoryEvent);
            _uiEventChannel.AddListener<OpenUI>(OpenUIHandler);
            _uiEventChannel.AddListener<CloseUI>(CloseUIHandler);
            _menuPanels = new Dictionary<UIType, MoveUI>();

            GetComponentsInChildren<MoveUI>().ToList().ForEach(panel => _menuPanels.Add(panel.myType, panel));
            _menuPanels.Values.ToList().ForEach(panel => panel.Initialize());
        }

        private void CloseUIHandler(CloseUI uI)
        {
            _currentUI.Close();
            _currentUI.gameObject.SetActive(false);
            _currentUI = null;
        }

        private void OpenUIHandler(OpenUI evt)
        {
            OpenPanel(evt.type);
        }

        public void OpenPanel(UIType uiType)
        {
            _currentUI?.Close();
            _currentUI = _menuPanels[uiType];
            _currentUI.gameObject.SetActive(true);
            _currentUI.Open();
        }

        private void OnDestroy()
        {
            //_uiEventChannel.RemoveListener<OpenInventoryEvent>(HandleOpenInventoryEvent);
            _uiEventChannel.RemoveListener<OpenUI>(OpenUIHandler);
            _uiEventChannel.RemoveListener<CloseUI>(CloseUIHandler);
        }
        #region ConfigUI
        //private void HandleOpenInventoryEvent(OpenInventoryEvent evt) //나중에 설정만들때 쓸듯?
        //{
        //    //진행중이라면 리턴
        //    if (_uiWindowStatus == UIWindowStatus.Closing || _uiWindowStatus == UIWindowStatus.Opening) return;

        //    if (_uiWindowStatus == UIWindowStatus.Opened) //열려있다면 닫기
        //    {
        //        _uiWindowStatus = UIWindowStatus.Closing;
        //        _playerInput.SetPlayerInput(true);
        //        SetWindow(false, () =>
        //        {
        //            _uiWindowStatus = UIWindowStatus.Closed;
        //            Time.timeScale = 1f;
        //            _currentUI?.Close();
        //        });
        //    }
        //    else if (_uiWindowStatus == UIWindowStatus.Closed) //닫혀있다면 열기
        //    {
        //        _uiWindowStatus = UIWindowStatus.Opening;
        //        Time.timeScale = 0;
        //        _playerInput.SetPlayerInput(false);
        //        SetWindow(true, () => _uiWindowStatus = UIWindowStatus.Opened);
        //        OpenPanel(evt.uiType); //지정된 패널 열기
        //    }
        //}
        #endregion
        public void SetWindow(bool isOpen, Action callback = null)
        {
            float alpha = isOpen ? 1f : 0f;
            _canvasGroup.DOFade(alpha, 0.3f).SetUpdate(true).OnComplete(() => callback?.Invoke());
            _canvasGroup.blocksRaycasts = isOpen;
            _canvasGroup.interactable = isOpen;
        }

    }
}