using DG.Tweening;
using GGMPool;
using TMPro;
using UnityEngine;

namespace Scripts.Utiles
{
    public class ItemDropText : MonoBehaviour,IPoolable
    {
        [SerializeField] private TextMeshPro _text;
        [SerializeField] private PoolTypeSO myType;
        private Pool _myPool;
        public PoolTypeSO PoolType => myType;

        public GameObject GameObject => gameObject;

        public void Init(string txt,Vector2 pos)
        {
            transform.position = pos;
            _text.text = txt;
            transform.DOMoveY(transform.position.y+1, 3).OnComplete(() => _myPool.Push(this));
        }

        public void ResetItem()
        {
            _text.text = null;
        }

        public void SetUpPool(Pool pool)
        {
            _myPool = pool; 
        }
    }
}
