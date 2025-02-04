using UnityEngine;

namespace Agama.Scripts.Animators
{
    [CreateAssetMenu(fileName = "AnimationParamitor", menuName = "SO/Animatiom/Paramitor", order = 0)]
    public class AnimationParamitorSO : ScriptableObject
    {
        public string paramitorName;
        public int hashCode;

        private void OnValidate()
        {
            hashCode = Animator.StringToHash(paramitorName);
        }
    }
}
