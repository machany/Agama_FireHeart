using UnityEngine;

namespace Agama.Scripts.Animators
{
    [CreateAssetMenu(fileName = "AnimationParamitor", menuName = "SO/Animatiom/Paramitor", order = 0)]
    public class AnimationParamiterSO : ScriptableObject
    {
        public string paramiterName;
        public int hashCode;

        private void OnValidate()
        {
            hashCode = Animator.StringToHash(paramiterName);
        }
    }
}
