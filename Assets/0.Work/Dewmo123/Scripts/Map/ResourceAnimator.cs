using Agama.Scripts.Animators;
using Agama.Scripts.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Map
{
    public class ResourceAnimator : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        [SerializeField] private List<AnimationParamiterSO> _params;
        private AnimationParamiterSO _current;
        private Entity _entity;
        private EntityRenderer _renderer;
        private Dictionary<string, AnimationParamiterSO> _paramDic;

        public void Initialize(Entity owner)
        {
            _entity = owner;
            _paramDic = new Dictionary<string, AnimationParamiterSO>();
            _params.ForEach(item => _paramDic.Add(item.paramiterName, item));
            _entity.GetComp<EntityAnimatorTrigger>().OnAnimationEndEvent += HadleEndTrigger;
        }
        private void OnDestroy()
        {
            _entity.GetComp<EntityAnimatorTrigger>().OnAnimationEndEvent -= HadleEndTrigger;
        }
        public void AfterInitialize()
        {
            _renderer = _entity.GetComp<EntityRenderer>();
            PlayAnimation("Idle");
        }
        public void PlayAnimation(string paramName)
        {
            var param = _paramDic[paramName];
            if (_current != null)
                _renderer.SetParamiter(_current, false);
            _renderer.SetParamiter(param, true);
            _current = param;
        }
        private void HadleEndTrigger()
        {
            PlayAnimation("Idle");
        }


    }
}
