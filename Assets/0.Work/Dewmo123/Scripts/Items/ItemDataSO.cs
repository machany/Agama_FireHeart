using System.Text;
using UnityEngine;
using Agama.Scripts.Combats;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Scripts.Items
{
    public abstract class ItemDataSO : ScriptableObject
    {
        public const int DEFAULT_DAMAGE = 10;

        public string itemName;
        public Sprite icon;
        public string itemID;
        public int maxStack;
        [SerializeField] protected DamageMethodType _damageType;
        [HideInInspector] public sbyte damageType;
        [HideInInspector] public float attackDamage = DEFAULT_DAMAGE;
        protected StringBuilder _stringBuilder = new StringBuilder();

        public virtual string GetDescription()
        {
            return string.Empty;
        }

        protected virtual void OnEnable()
        {
            damageType = (sbyte)_damageType;
        }

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            string path = AssetDatabase.GetAssetPath(this);
            itemID = AssetDatabase.AssetPathToGUID(path);
        }
#endif
    }

}
