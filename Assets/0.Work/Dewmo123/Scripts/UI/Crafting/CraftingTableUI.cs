using Agama.Scripts.Events;
using Scripts.EventChannel;
using Scripts.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.UI.Crafting
{
    public class CraftingTableUI : MonoBehaviour
    {
        [SerializeField] private List<CraftingRecipeSO> _recipes;
        private CraftInfoTxt _infoTxt;
        [SerializeField] private Transform _recipeParent;
        [SerializeField] private EventChannelSO _invenChannel;
        private RecipeUI _selectedRecipe;

        public void Awake()
        {
            _infoTxt = GetComponentInChildren<CraftInfoTxt>();
            var recipeUIs= _recipeParent.GetComponentsInChildren<RecipeUI>();
            Debug.Assert(recipeUIs.Length == _recipes.Count, "UICount does not equal RecipeCount");
            for (int i = 0; i < recipeUIs.Length; i++)
                recipeUIs[i].Init(_recipes[i], this);
        }
        public void SelectRecipe(RecipeUI recipeUI)
        {
            _selectedRecipe = recipeUI;
            _infoTxt.Select(recipeUI.recipe);
        }
        public void CraftItem()
        {
            var evt = InvenEvents.CraftItemEvent;
            evt.recipe = _selectedRecipe.recipe;
            if (evt.recipe == null) return;
            _invenChannel.InvokeEvent(evt);
        }
    }
}
