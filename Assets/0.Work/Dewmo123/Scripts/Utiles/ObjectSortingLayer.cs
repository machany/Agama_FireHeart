using UnityEngine;

namespace Scripts.Utiles
{
    public class ObjectSortingLayer : MonoBehaviour
    {
        [SerializeField] private int _offset;

        private void OnEnable()
        {
            transform.GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.parent.position.y * -100) + _offset;
        }
    }
}
