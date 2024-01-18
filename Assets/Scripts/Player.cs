using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    //TOD
    public InventoryManager inventoryManager;
    public Inventory inventory;

    private void Awake()
    {
        Instance = this;
    }
}
