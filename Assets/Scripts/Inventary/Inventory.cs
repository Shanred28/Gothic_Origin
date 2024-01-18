using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action<ItemScriptableObject> AddInventoryItem;
    public event Action<ItemScriptableObject,int> ChangeInventoryItemAmmount;

    [SerializeField] private List<Items> _items = new List<Items>();

    private CompositeDisposable _disposables = new CompositeDisposable();
    public ReactiveCommand<int> ComandUseItem = new ReactiveCommand<int>();
    public ReactiveCommand<int> ComandRemoveItem = new ReactiveCommand<int>();

    private void Start()
    {
        ComandUseItem.Subscribe(item =>
        {
            UseItem(item);
        }).AddTo(_disposables);

        ComandRemoveItem.Subscribe(item => 
        {
            RemoveItem(item);
        }).AddTo(_disposables);
    }

    private void OnDestroy()
    {
        _disposables.Clear();
    }

    public void AddItem(ItemScriptableObject itemScriptableObject)
    {
        bool isNew = false;
        foreach (var itemsdf in _items)
        {
            
            if (itemScriptableObject.IdItem == itemsdf.IdItem)
            {
                itemsdf.SetAmmount(itemScriptableObject.Ammount);
                Debug.Log(itemsdf.Amount);
                ChangeInventoryItemAmmount?.Invoke(itemScriptableObject,itemsdf.Amount);
                isNew = true;
                continue;
            }
        }
        if (isNew == false)
        {
            var item = CreateItem(itemScriptableObject);
            _items.Add(item);
            AddInventoryItem?.Invoke(itemScriptableObject);
        }       
    }


    private Items CreateItem(ItemScriptableObject itemScriptableObject)
    {
        switch (itemScriptableObject.TypeItem)
        {
            case ItemType.Herb:
                Debug.Log("Create herb");
                ItemsHerb herb = new ItemsHerb(itemScriptableObject);
                return herb;
        }

        Debug.Log("Õ≈“ “¿ Œ√Œ “»œ¿");
        return null;
    }

    public bool UseItem(int id)
    {
        foreach (var it in _items)
        {
            if (id == it.IdItem)
            {
                if (it is IUseItem)
                {
                    var itemUse = it as IUseItem;
                    itemUse.Use();
                    return true;
                }
                    
            }
        }

        return false;
    }

    public bool RemoveItem(int id)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].IdItem == id)
            {
                _items.Remove(_items[i]);
                Debug.Log("”‰‡ÎËÎ:");
                return true;
            }
        }
        return false;
    }
}
