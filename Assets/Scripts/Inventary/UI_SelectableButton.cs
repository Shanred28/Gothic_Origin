using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_SelectableButton : UI_Button
{
    [SerializeField] private Image _selectImage;

    public UnityEvent OnSelect;
    public UnityEvent OnUnSelect;
    //public UnityEvent OnDestroyButton;
   // public UnityEvent<Action> OnDestroyButton;
    public Action<UI_SelectableButton> OnDestroyButton;

    public void OnDestroy()
    {
        OnDestroyButton?.Invoke(this);
    }
    public override void SetFocuse()
    {
        base.SetFocuse();

        _selectImage.enabled = true;
        OnSelect?.Invoke();
    }

    public override void SetUnFocuse()
    {
        base.SetUnFocuse();

        if(_selectImage != null)
            _selectImage.enabled = false;
        OnUnSelect?.Invoke();
    }
}
