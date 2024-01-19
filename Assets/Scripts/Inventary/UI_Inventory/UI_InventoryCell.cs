using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventoryCell : MonoBehaviour
{
    private const string NAMEITEM = "Название: ";
    private const string AMMOUNTITEM = "Количество: ";
    private const string COSTGOLDITEM = "Стоимость: ";

    [SerializeField] private TMP_Text _nameItem;

    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _amountText;

    [SerializeField] private GameObject _popupPref;
    [SerializeField] private GameObject _popupPanelInteractItem;
    private GameObject _popupPanel;

    private string _infoItem = "Тут будет классное описание";
    private int _costGold;
    public int IdItem;
    private UI_PanelInfoItem _panelInfo;
    private Transform _parentInfoPanel;
    private UI_PanelInteractItemInv panelInteractItemInv;

    private void OnEnable()
    {
        if (_panelInfo != null)
        {
            RemoveInfoItem();
        }      
    }

    public void SetInfo(string name, int id, Sprite image, int amount, int costGold, Transform parent, string info)
    {
        _nameItem.text = name;
        IdItem = id;
        _costGold = costGold;
        _image.sprite = image;
        _amountText.text = amount.ToString();
        _infoItem = info;
       _parentInfoPanel = parent;
    }

    public void SetAmmount(int amount)
    {
        _amountText.text = amount.ToString();
    }

    public void PopupInfoItem(Vector3 pos)
    {
        if (_panelInfo == null)
        {
             var obj = Instantiate(_popupPref.gameObject, _parentInfoPanel);
            _panelInfo = obj.GetComponent<UI_PanelInfoItem>();
            _panelInfo.SetInfo(NAMEITEM + _nameItem.text, AMMOUNTITEM + _amountText.text, _infoItem, COSTGOLDITEM+ _costGold.ToString());
        }
        
        var sdfd = _panelInfo.GetComponent<RectTransform>();
        _panelInfo.transform.position = new Vector3(pos.x + 150, pos.y - 400,0);
    }

    public void RemoveInfoItem()
    {
        if(_panelInfo != null)
         Destroy(_panelInfo.gameObject);
        if (_popupPanel != null)
        {
            panelInteractItemInv.OnUseItem.RemoveListener(OnUseItem);
            panelInteractItemInv.OnRemoveItem.RemoveListener(OnRemoveItem);
            Destroy(_popupPanel.gameObject);
        }         
    }

    public void OnClickCell()
    {
        _popupPanel = Instantiate(_popupPanelInteractItem, transform);

        panelInteractItemInv = _popupPanel.GetComponent<UI_PanelInteractItemInv>();
        panelInteractItemInv.OnUseItem.AddListener(OnUseItem);
        panelInteractItemInv.OnRemoveItem.AddListener(OnRemoveItem);

        if (_panelInfo != null)
            Destroy(_panelInfo.gameObject);
    }

    private void OnUseItem()
    {
        Player.Instance.inventory.UseItem(IdItem);
    }

    public void OnRemoveItem()
    {
        Player.Instance.inventory.RemoveItem(IdItem);
    }
}
