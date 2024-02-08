using UnityEngine;

public class CharacterInputController : MonoBehaviour
{
    [SerializeField] private CharacterMovementHuman characterMovement;


    [Header("Camera setting")]
    [SerializeField] private ThirdPersonCamera _thirdPersonCamera;
    private void Update()
    {

        characterMovement.TargetDirectionControl = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        _thirdPersonCamera.RotationControl = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") );


        /*        if (characterMovement.TargetDirectionControl != Vector3.zero)
                {
                    _thirdPersonCamera.IsRotateTarget = true;
                }
                else 
                {
                    _thirdPersonCamera.IsRotateTarget = false;
                }*/

        if (Input.GetKeyDown(KeyCode.LeftShift) == true)
        {
            characterMovement.Sprint();
        }

        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            characterMovement.Jump();
        }
    }
}
