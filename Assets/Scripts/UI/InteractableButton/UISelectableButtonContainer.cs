using System.Collections.Generic;
using UnityEngine;

public class UISelectableButtonContainer : MonoBehaviour
{
    [SerializeField] private Transform _buttonsContainer;

    public bool Interactable = true;
    public void SetInteractable(bool interactable) => Interactable = interactable;

    private List<UI_SelectableButton> _buttons = new List<UI_SelectableButton>();
    private int _selectButtonIndex;

    private void OnEnable()
    {
        foreach (var button in _buttonsContainer)
        {
            if (TryGetComponent(out UI_SelectableButton component))
            {
                _buttons.Add(component);
            }
        }
       // _buttons = _buttonsContainer.GetComponentsInChildren<UI_SelectableButton>();

        if (_buttons == null)
            Debug.LogError("Button list is empty!");

        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].eventPointerEnter += OnPointerEnter;
            _buttons[i].OnDestroyButton += RemoveButton;
        }

        if (Interactable == false) return;

        if (_selectButtonIndex == 0) return;

        _buttons[_selectButtonIndex].SetFocuse();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].eventPointerEnter -= OnPointerEnter;
        }
    }

    private void OnPointerEnter(UI_Button button)
    {
        SelectButton(button);
    }

    private void SelectButton(UI_Button button)
    {
        if (Interactable == false) return;

        _buttons[_selectButtonIndex].SetUnFocuse();

        for (int i = 0; i < _buttons.Count; i++)
        {
            if (button == _buttons[i])
            {
                _selectButtonIndex = i;
                button.SetFocuse();
                break;
            }
        }
    }

    private void RemoveButton(UI_SelectableButton selectableButton)
    {
        foreach (var item in _buttons)
        {
            if (selectableButton == item)
            {
                item.OnDestroyButton -= RemoveButton;
                _buttons.Remove(item);
            }
               
        }
    }
}

