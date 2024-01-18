using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform _targetCameraFollowPoint;
    [SerializeField] private Transform _playerModel;

    [SerializeField] private float _rotationSpeed;

    [HideInInspector] public Vector2 RotationControl;

    [SerializeField] private float _minAngle ,_maxAngle;

/*    [HideInInspector] public bool IsRotateTarget;*/

    private void Update()
    {
        //Horizontal
        _targetCameraFollowPoint.rotation *= Quaternion.AngleAxis(RotationControl.x * _rotationSpeed, Vector3.up);

        //Vertical
        _targetCameraFollowPoint.rotation *= Quaternion.AngleAxis(-RotationControl.y * _rotationSpeed, Vector3.right);

        AngleCameraRotation();


    }

    private void AngleCameraRotation()
    {
        Vector3 angles = _targetCameraFollowPoint.localEulerAngles;
        angles.z = 0;

        if (angles.x > 180 && angles.x < _maxAngle)
        {
            angles.x = _maxAngle;
        }
        else if (angles.x < 180 && angles.x > _minAngle)
        {
            angles.x = _minAngle;
        }

        _targetCameraFollowPoint.localEulerAngles = angles;

        _playerModel.rotation = Quaternion.Euler(0, _targetCameraFollowPoint.eulerAngles.y, 0);
        _targetCameraFollowPoint.localEulerAngles = new Vector3(angles.x, 0, 0);

    }
}
