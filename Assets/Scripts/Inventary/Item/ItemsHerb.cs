using UnityEngine;

public class ItemsHerb : Items, IUseItem, ISellItem
{
    private int _hillHp;
    public ItemsHerb(ItemScriptableObjectHerb scriptableObject)
    {
        _name = scriptableObject.Name;
        _idItem = scriptableObject.IdItem;
        _ammount = scriptableObject.Ammount;
        _scriptableObject  = scriptableObject;
        _hillHp = scriptableObject.powerHill;
    }

    public void Sell()
    {
        throw new System.NotImplementedException();
    }

    public void Use()
    {
        _ammount--;
        Player.Instance.playerDes.ApplyHill(_hillHp);
        Debug.Log(_ammount);
        Debug.Log("Использовал:");
    }
}
