using System;

[Serializable]
public class Items
{
    protected string _name;
    protected int _idItem;
    public int IdItem => _idItem;
    protected int _ammount;
    public int Amount => _ammount;
    protected ItemScriptableObject _scriptableObject;

    public void SetAmmount(int ammount)
    {
        _ammount += ammount;
    }

}
