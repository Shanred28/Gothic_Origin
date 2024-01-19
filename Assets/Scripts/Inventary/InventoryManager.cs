using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemInInventory
{
    public string Name;
    public int IdItem;
    public int Ammount;
    public int CountGold;

    public ItemInInventory(string name, int idItem, int ammount, int countGold)
    {
        Name = name;
        IdItem = idItem;
        Ammount = ammount;
        CountGold = countGold;
    }

    public void SetAmmount(int ammount)
    {
        Ammount = ammount;
    }
}

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    [SerializeField] private Dictionary<int, UI_InventoryCell> _items;
    [SerializeField] private List<ItemInInventory> _itemsInInventory = new List<ItemInInventory>();
    [SerializeField] private List<UI_InventoryCell> _inventoryCells = new List<UI_InventoryCell>();

    [SerializeField] private Transform _itemContent;
    [SerializeField] private Transform _perentInfoPanel;

    [SerializeField] private UI_InventoryCell inventoryCellPref;

    private void Start()
    {
        _inventory.AddInventoryItem += AddCell;
        _inventory.ChangeInventoryItemAmmount += OnChangeInventoryItemAmmount;
        _inventory.RemoveInventoryItem += OnRemoveInventoryItem;
    }

    private void OnDestroy()
    {
        _inventory.AddInventoryItem -= AddCell;
        _inventory.ChangeInventoryItemAmmount -= OnChangeInventoryItemAmmount;
        _inventory.RemoveInventoryItem -= OnRemoveInventoryItem;
    }

    private void OnChangeInventoryItemAmmount(int itemId,int obj)
    {
        if (SearchItem(itemId) == true)
        {
            ChangeAmmountCells(itemId, obj);
        }
    }

    public void AddCell(ItemScriptableObject itemSO)
    {
        ItemInInventory inventory = new ItemInInventory(itemSO.Name, itemSO.IdItem, itemSO.Ammount, itemSO.CountGold);
        _itemsInInventory.Add(inventory);
        AddItemCellInInventory(itemSO);

    }

    private void AddItemCellInInventory(ItemScriptableObject item)
    {
        UI_InventoryCell cell = Instantiate(inventoryCellPref, _itemContent);
        cell.SetInfo(item.Name, item.IdItem, item.IconeItem, item.Ammount, item.CountGold, _perentInfoPanel, item.ItemInfo);
        _inventoryCells.Add(cell);
    }
 
    private bool SearchItem(int itemId)
    {
        foreach (ItemInInventory item in _itemsInInventory)
        {
            if (itemId == item.IdItem)
            {
                return true;
            }
        }
        return false;
    }

    private void ChangeAmmountCells(int itemId, int ammount)
    {
        foreach (var cell in _itemsInInventory)
        {
            if (itemId == cell.IdItem)
            {
                cell.SetAmmount(ammount);
            }
        }
        foreach (var cell in _inventoryCells)
        {
            if (itemId == cell.IdItem)
            {
                cell.SetAmmount(ammount);
            }
        }
    }

    //Удаление из списков. Дестрой _inventoryCells. Последовательность циклов важна.
    private void OnRemoveInventoryItem(int itemId)
    {
        for (int i = 0; i < _itemsInInventory.Count; i++)
        {
            if (itemId == _itemsInInventory[i].IdItem)
            {
                _itemsInInventory.Remove(_itemsInInventory[i]);
            }
        }

        for (int i = 0; i < _inventoryCells.Count; i++)
        {
            if (itemId == _inventoryCells[i].IdItem)
            {
                Destroy(_inventoryCells[i].gameObject);
                _inventoryCells.Remove(_inventoryCells[i]);
            }
        }
    }
}
