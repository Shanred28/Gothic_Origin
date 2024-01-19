using UnityEngine;
using UnityEngine.EventSystems;

public class UI_SelectableButtonCellInventory : UI_SelectableButton
{
    [SerializeField] private UI_InventoryCell inventoryCell;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        inventoryCell.PopupInfoItem(eventData.position);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        inventoryCell.RemoveInfoItem();
    }
}
