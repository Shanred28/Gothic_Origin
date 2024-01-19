using UnityEngine;

public enum ItemType
{
    Herb,
    Potion,
    Weapon,
    Armor,
    Ring
}

[CreateAssetMenu(fileName ="New Item", menuName ="Item/Create")]
public class ItemScriptableObject : ScriptableObject
{
    public string Name;
    public int IdItem;
    public ItemType TypeItem;
    public int Ammount;
    public int CountGold;
    public GameObject ModelPref;
    public Sprite IconeItem;
    public string ItemInfo;
}
