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
        public Animator AnimatorComp {  get; private set; }

        public void Initialize(Entity owner)
        {
            _owner = owner;
            _spriteRenderer = transform.GetOrAddComponent<SpriteRenderer>();
            AnimatorComp = transform.GetComponent<Animator>();
            Debug.Assert(AnimatorComp != null, "could not find animator component");
        }

        public void SetParamiter(AnimationParamiterSO paramitor)
            => AnimatorComp.SetTrigger(paramitor.hashCode);
        public void SetParamiter(AnimationParamiterSO paramitor, bool value)
            => AnimatorComp.SetBool(paramitor.hashCode, value);
        public void SetParamiter(AnimationParamiterSO paramitor, int value)
            => AnimatorComp.SetInteger(paramitor.hashCode, value);
        public void SetParamiter(AnimationParamiterSO paramitor, float value)
            => AnimatorComp.SetFloat(paramitor.hashCode, value);

        public void SetColor(Color color)
            => _spriteRenderer.color = color;

        private void FlipEntity()
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
