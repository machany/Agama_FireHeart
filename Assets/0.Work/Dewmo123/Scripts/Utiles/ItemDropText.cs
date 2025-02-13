using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Scripts.Utiles
{
    public class ItemDropText : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _text;

        public void Init(string txt)
        {
            _text.text = txt;
            transform.DOMoveY(transform.position.y+1, 3).OnComplete(() => Destroy(gameObject));
        }
    }
}
