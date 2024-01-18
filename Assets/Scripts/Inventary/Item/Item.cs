using UnityEngine;

public class Item : MonoBehaviour, IPickUp
{
    [SerializeField] private ItemScriptableObject ItemSO;
    [SerializeField] private ItemType type;

    public ItemType ItemType => type;

    public void PickUp()
    {
        Player.Instance.inventory.AddItem(ItemSO);
        Destroy(gameObject);
    }
}
