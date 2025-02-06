using Agama.Scripts.Animators;
using Unity.VisualScripting;
using UnityEngine;

namespace Agama.Scripts.Entities
{
    public class EntityRenderer : MonoBehaviour, IEntityComponent
    {
        /// <summary>
        /// 1 = 오른쪽을 바라보고 있음, -1 = 왼쪽을 바라보고 있음
        /// </summary>
        [field: SerializeField] public sbyte FacingDirection { get; private set; } = 1;

        private Entity _owner;
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;

        public void Initialize(Entity owner)
        {
            _owner = owner;
            _spriteRenderer = transform.GetOrAddComponent<SpriteRenderer>();
            _animator = transform.GetComponent<Animator>();
            Debug.Assert(_animator != null, "could not find animator component");
        }

        public void SetParamitor(AnimationParamiterSO paramitor)
            => _animator.SetTrigger(paramitor.hashCode);
        public void SetParamitor(AnimationParamiterSO paramitor, bool value)
            => _animator.SetBool(paramitor.hashCode, value);
        public void SetParamitor(AnimationParamiterSO paramitor, int value)
            => _animator.SetInteger(paramitor.hashCode, value);
        public void SetParamitor(AnimationParamiterSO paramitor, float value)
            => _animator.SetFloat(paramitor.hashCode, value);

        public void SetColor(Color color)
            => _spriteRenderer.color = color;

        public void FlipEntity()
        {
            FacingDirection *= -1;
            _owner.transform.Rotate(0, 180f, 0);
        }

        public void Flip(float xVelocity)
        {
            float xMove = Mathf.Approximately(xVelocity, 0) ? 0 : Mathf.Sign(xVelocity);
            if (Mathf.Abs(xMove + FacingDirection) < 0.5f)
                FlipEntity();
        }
    }
}
