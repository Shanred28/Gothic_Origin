using TMPro;
using UnityEngine;

public class UI_PanelInfoItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _ammount;
    [SerializeField] private TMP_Text _info;
    [SerializeField] private TMP_Text _cost;

    public void SetInfo(string name, string ammount, string info, string cost)
    {
        _name.text = name;
        _ammount.text = ammount;
        _info.text = info;
        _cost.text = cost;
    }
}
