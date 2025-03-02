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
        [SerializeField] private Transform _selectedImage;
        private RecipeUI _selectedRecipe;

        public void Awake()
        {
            _infoTxt = GetComponentInChildren<CraftInfoTxt>();
            var recipeUIs= _recipeParent.GetComponentsInChildren<RecipeUI>();
            Debug.Assert(recipeUIs.Length == _recipes.Count, "UICount does not equal RecipeCount");
            for (int i = 0; i < recipeUIs.Length; i++)
                recipeUIs[i].Init(_recipes[i], this);
            SelectRecipe(recipeUIs[1]);
        }
        public void SelectRecipe(RecipeUI recipeUI)
        {
            _selectedRecipe = recipeUI;
            _infoTxt.Select(recipeUI.recipe);
            _selectedImage.transform.position = recipeUI.transform.position;
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
