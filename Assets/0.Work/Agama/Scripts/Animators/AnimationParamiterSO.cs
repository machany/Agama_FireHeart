using UnityEngine;

namespace Agama.Scripts.Animators
{
    [CreateAssetMenu(fileName = "AnimationParamiter", menuName = "SO/Animatiom/Paramiter", order = 0)]
    public class AnimationParamiterSO : ScriptableObject
    {
        [Tooltip("<color=red>명명 규칙</color> : 영문 소문자를 기준으로 하며, 띄어쓰기시 언더바(_)를 사용한다.")]
        public string paramiterName;
        public int hashCode;

        private void OnValidate()
        {
            hashCode = Animator.StringToHash(paramiterName);
        }
    }
}
