using Agama.Scripts.Entities;
using Agama.Scripts.Events;
using Assets._0.Work.Dewmo123.Scripts.Structures;
using Scripts.EventChannel;
using System;
using UnityEngine;

namespace Scripts.Players
{
    public class PlayerTentUtile : MonoBehaviour, IEntityComponent
    {
        private Tent _currentTent;
        [SerializeField] private EventChannelSO _utileChannel;
        [SerializeField] private GameObject _arrow;
        private Entity _owner;
        public void Initialize(Entity owner)
        {
            _arrow.gameObject.SetActive(false);
            _owner = owner;
            _utileChannel.AddListener<SelectTent>(HandleTentSelect);
        }
        private void Update()
        {
            if (_currentTent == null) return;
            _arrow.transform.right = _currentTent.transform.position - _owner.transform.position;
        }
        private void HandleTentSelect(SelectTent evt)
        {
            if (_currentTent != null)
                _currentTent.OnDeadEvent.RemoveListener(HandleTentDestroyed);
            _arrow.gameObject.SetActive(true);
            _currentTent = evt.tent;
            _currentTent.OnDeadEvent.AddListener(HandleTentDestroyed);
        }

        private void HandleTentDestroyed()
        {
            _currentTent = null;
            _arrow.gameObject.SetActive(false);
        }
    }
}
