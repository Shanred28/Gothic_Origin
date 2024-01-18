using UnityEngine;
using UnityEngine.Events;

public class UI_PanelInteractItemInv : MonoBehaviour
{
    public  UnityEvent OnUseItem;
    public  UnityEvent OnRemoveItem;

    public void UseItem()
    {
        OnUseItem?.Invoke();
    }

    public void RemoveItem()
    {
        OnRemoveItem?.Invoke();       
    }
}
