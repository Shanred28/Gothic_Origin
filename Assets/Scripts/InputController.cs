using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryUI;
    //TODO
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        { 
            if(_inventoryUI.activeSelf == false)
                _inventoryUI.SetActive(true);
            else
                _inventoryUI.SetActive(false);
        }
    }
}
