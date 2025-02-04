using Agama.Scripts.Events;
using Scripts.EventChannel;
using Scripts.Items;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.UI.Crafting
{
    public class CraftInfoTxt : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _title, _description;

        public void Select(CraftingRecipeSO recipe)
        {
            _title.text = recipe.product.itemName;
            _description.text = recipe.GetNeededItemInfo();
        }

    }
}
