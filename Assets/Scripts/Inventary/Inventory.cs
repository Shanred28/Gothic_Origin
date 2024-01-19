using System;
using System.Collections.Generic;
using UnityEngine;

//����� ���������, ����������� ������. 
public class Inventory : MonoBehaviour
{
    public event Action<ItemScriptableObject> AddInventoryItem;
    public event Action<int,int> ChangeInventoryItemAmmount;
    public event Action<int> RemoveInventoryItem;

    [SerializeField] private List<Items> _items = new List<Items>();

    /*    private CompositeDisposable _disposables = new CompositeDisposable();
        public ReactiveCommand<int> ComandUseItem = new ReactiveCommand<int>();
        public ReactiveCommand<int> ComandRemoveItem = new ReactiveCommand<int>();*/

    /*    private void Start()
        {
            ComandUseItem.Subscribe(item =>
            {
                UseItem(item);
            }).AddTo(_disposables);

            ComandRemoveItem.Subscribe(item => 
            {
                RemoveItem(item);
            }).AddTo(_disposables);
        }*/

    /*    private void OnDestroy()
        {
            _disposables.Clear();
        }*/

    //��� ��������� ����� ����, �������� ���� �� ����� ��� ���� � ���������, ���� ��, �� �����������. ���� ���, �� ��������� ����� ����� ���� �
    //����������� � ������.
    public void AddItem(ItemScriptableObject itemScriptableObject)
    {
        bool isNew = false;
        foreach (var itemsdf in _items)
        {
            
            if (itemScriptableObject.IdItem == itemsdf.IdItem)
            {
                itemsdf.SetAmmount(itemScriptableObject.Ammount);
                ChangeInventoryItemAmmount?.Invoke(itemScriptableObject.IdItem, itemsdf.Amount);
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

    //�������� ������ ������ item �� ���� ����������� item
    private Items CreateItem(ItemScriptableObject itemScriptableObject)
    {
        switch (itemScriptableObject.TypeItem)
        {
            case ItemType.Herb:
                ItemsHerb herb = new ItemsHerb(itemScriptableObject as ItemScriptableObjectHerb);
                return herb;
        }

        Debug.Log("��� ������ ����");
        return null;
    }


    //������������� item � ���������. ���� ���� ����� item  �� ��������� ammount, ���� ammount ����� ������������� 0,
    //�������� ����� � ������� ������� �� UI � ��������� 
    public void UseItem(int id)
    {
        foreach (var it in _items)
        {
            if (id == it.IdItem)
            {
                if (it is IUseItem)
                {
                    
                    var itemUse = it as IUseItem;
                    itemUse.Use();

                    if (it.Amount > 0)
                    {
                        ChangeInventoryItemAmmount?.Invoke(it.IdItem, it.Amount);
                    }

                    else
                    {
                        RemoveInventoryItem?.Invoke(id);
                        RemoveItem(id);
                        break;
                    }
                }
                    
            }
        }
    }

    //������� item � ������� �����. 
    public bool RemoveItem(int id)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].IdItem == id)
            {
                _items.Remove(_items[i]);
                RemoveInventoryItem?.Invoke(id);
                return true;
            }
        }
        return false;
    }
}
