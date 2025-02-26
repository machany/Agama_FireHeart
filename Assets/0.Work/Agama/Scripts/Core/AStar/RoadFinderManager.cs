using UnityEngine;

namespace Agama.Scripts.Core.AStar
{
    public class RoadFinderManager : MonoBehaviour
    {
        [SerializeField] private RoadFinderSO roadFinder;

        private void Awake()
        {
            roadFinder.Initialize();
        }
    }
}