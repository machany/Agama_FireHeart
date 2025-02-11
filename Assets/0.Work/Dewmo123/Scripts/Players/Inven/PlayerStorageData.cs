using Scripts.InvenSystem;
using Scripts.Items;

namespace Scripts.Players.Inven
{
    public class PlayerStorageData : InvenData
    {
        public override void AddItem(ItemDataSO itemData, int count = 1)
        {
        }

        public override bool CanAddItem(ItemDataSO itemData)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveItem(ItemDataSO itemData, int count)
        {
            throw new System.NotImplementedException();
        }
    }
}
