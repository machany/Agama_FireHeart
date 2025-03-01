﻿using Scripts.InvenSystem;
using SerializableDictionary.Scripts;
using System.Text;
using UnityEngine;

namespace Scripts.Items
{
    [CreateAssetMenu(menuName = "SO/Recipe", fileName = "CraftingRecipe")]
    public class CraftingRecipeSO : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<ItemDataSO, int> neededItem;//재료들이라고 해서 MaterialItem이 아닐수 있음!!!
        public ItemDataSO product;
        public Sprite icon => product.icon;
        public bool MakeItem(InvenData inven)
        {
            foreach (var KVP in neededItem.Dictionary)
            {
                var item = inven.GetAllItemStack(KVP.Key);
                if (item < KVP.Value)
                    return false;
            }
            foreach (var KVP in neededItem.Dictionary)
                inven.RemoveItem(KVP.Key, KVP.Value);
            return true;
        }
        public string GetNeededItemInfo()
        {
            StringBuilder message = new StringBuilder();
            foreach (var KVP in neededItem.Dictionary)
            {
                message.Append($"{KVP.Key.itemName} : {KVP.Value}\n");
            }
            return message.ToString();
        }
        public string GetDescription()
        {
            return product.GetDescription();
        }
    }
}
