using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    //TOD
    //public InventoryManager inventoryManager;
    public Inventory inventory;
    public Destructible playerDes;

    private void Awake()
    {
        Instance = this;
    }
}
