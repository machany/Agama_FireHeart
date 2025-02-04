using Agama.Scripts.Events;
using Scripts.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.UI.Crafting
{
    //레시피 UI 관리하는 스크립트 만들어도 괜찮을듯 SO도 거기에 몰아넣으면 알아서 Init해주는거
    //그래야 뭐가 선택됐는지 잘 파악가능할것 같은데
    public class RecipeUI : MonoBehaviour, IPointerClickHandler
    {
        public CraftingRecipeSO recipe { get; private set; }
        [SerializeField] private Image _image;
        private CraftingTableUI _craftingTable;
        public void Init(CraftingRecipeSO recipe,CraftingTableUI table)
        {
            _craftingTable = table;
            this.recipe = recipe;
            _image.sprite = recipe.icon;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            _craftingTable.SelectRecipe(this);
        }
    }
}
