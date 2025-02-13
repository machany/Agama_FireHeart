using Scripts.EventChannel;
using System.Collections;
using UnityEngine;

namespace Scripts.UI.Inven.ItemPanelUI
{
    public abstract class PanelUI : MonoBehaviour
    {
        [SerializeField] protected Transform _slotParent;
        protected abstract void HandleDataRefresh(DataEvent evt);
        protected abstract void UpdateSlotUI();
        
    }

}