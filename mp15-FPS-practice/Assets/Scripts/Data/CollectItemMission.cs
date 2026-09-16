public class CollectItemMission : MissionBase
{
    public int ItemId;
    public CollectItemMission(int id, string name, string desc, int targetProgress, int itemId) : base(id, name, desc, targetProgress)
    {
        Type = MissionType.CollectItem;
        ItemId = itemId;
    }

    public void CompareTarget(IInteractable Item)
    {
        if (IsClear) return;

        ItemData itemData = Item.GameObject.GetComponent<ItemData>();
        if(itemData.Id == ItemId)
        {
            CurrentProgress++;
        }
    }
}
