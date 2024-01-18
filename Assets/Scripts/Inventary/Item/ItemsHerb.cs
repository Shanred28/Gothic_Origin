using UnityEngine;

public class ItemsHerb : Items, IUseItem, ISellItem
{
    public ItemsHerb(ItemScriptableObject scriptableObject)
    {
        _name = scriptableObject.Name;
        _idItem = scriptableObject.IdItem;
        _ammount = scriptableObject.Ammount;
        _scriptableObject  = scriptableObject;
    }

    public void Sell()
    {
        throw new System.NotImplementedException();
    }

    public void Use()
    {
        _ammount--;
        Debug.Log("Использовал:");
    }
}
