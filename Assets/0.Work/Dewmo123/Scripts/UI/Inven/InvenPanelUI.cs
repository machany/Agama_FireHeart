using Agama.Scripts.Events;
using Scripts.Core;
using Scripts.EventChannel;
using Scripts.InvenSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.UI.Inven
{
    //여기서 인벤 관리중 플레이어에서 관리할거면 이벤트채널 받아서 리스트 수정한 후 여기에 보내주면 됨
    public class InvenPanelUI : MonoBehaviour
    {
        [SerializeField] protected EventChannelSO _invenSwapChannel;
        public List<InventoryItem> inventory;

        [SerializeField] protected Transform _slotParent;
        protected ItemSlotUI[] _itemSlots;

        protected virtual void Awake()
        {
            _itemSlots = _slotParent.GetComponentsInChildren<ItemSlotUI>();

            _invenSwapChannel.AddListener<InvenSwapEvent>(InvenSwapHandler);

            for(int i = 0; i < _itemSlots.Length; i++)
            {
                _itemSlots[i].slotIndex = i;
                inventory.Add(null);
            }
            UpdateSlotUI();
        }
        private void OnDestroy()
        {
            _invenSwapChannel.RemoveListener<InvenSwapEvent>(InvenSwapHandler);
        }
        private void InvenSwapHandler(InvenSwapEvent t)
        {
            if (t.isSame)
            {
                inventory[t.index2].AddStack(inventory[t.index1].stackSize); //꽉찼을때 판정 해줘야함
                inventory.Remove(inventory[t.index1]);
            }
            else
            {
                (inventory[t.index1], inventory[t.index2]) = (inventory[t.index2], inventory[t.index1]);
            }
            UpdateSlotUI();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
                UpdateSlotUI();
        }

        private void HandleDataRefresh(List<InventoryItem> evt)//얘한테 줄때 현재 슬롯카운트 크기의 리스트로 줌
        {
            inventory = evt;
            UpdateSlotUI();
        }

        public virtual void UpdateSlotUI()
        {
            for (int i = 0; i < _itemSlots.Length; i++)
            {
                _itemSlots[i].CleanUpSlot();
            }
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].data != null)
                    _itemSlots[i].UpdateSlot(inventory[i]);
            }
        }
    }
}
