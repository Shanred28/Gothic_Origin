using Cinemachine.Utility;
using UnityEngine;

public  class CharacterMovementHuman : MonoBehaviour
{
    private CharacterController characterController;
    public Vector3 DirectionControl;
    private Vector3 movementDirections;

    private float distanceToGround;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpSpeed;

    public float DistanceToGround => distanceToGround;
    public bool IsGrounded => characterController.isGrounded || distanceToGround < 0.09f;
    public float CurrentSpeed => GetCurrentSpeedByState();

    public Vector3 TargetDirectionControl;
    [SerializeField] private float accelerationRate;

    public bool IsSprint;
    public bool IsCrouch;
    public bool IsFight;
    public bool IsJump;

   [SerializeField] private bool isSliding;
    private Vector3 _slopeSlideVelocity;
    [SerializeField] private float ySpeed;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        SetSlopeSlide();
        UpdateDistanceToGround();
        TargetControlMove();
        
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (IsGrounded == true && isSliding == false)
        {
            movementDirections = DirectionControl * GetCurrentSpeedByState();
            if (IsJump == true)
            {
                movementDirections.y = _jumpSpeed;
                IsJump = false;
            }

            movementDirections = transform.TransformDirection(movementDirections);
            movementDirections += Physics.gravity * Time.fixedDeltaTime;
            /*        if (UpdatePosition == true)*/
            characterController.Move(movementDirections * Time.fixedDeltaTime);
        }

/*        if (IsGrounded == false)
        {
            //movementDirections = DirectionControl * GetCurrentSpeedByState();
*//*            movementDirections = transform.TransformDirection(movementDirections);
            movementDirections += Physics.gravity * Time.fixedDeltaTime;*//*
            characterController.Move(movementDirections * Time.fixedDeltaTime);
        }*/

        movementDirections += Physics.gravity * Time.fixedDeltaTime;
        characterController.Move(movementDirections * Time.fixedDeltaTime);


        /*        movementDirections += Physics.gravity * Time.fixedDeltaTime;
        *//*        if (UpdatePosition == true)*//*
                    characterController.Move(movementDirections * Time.fixedDeltaTime);*/

        /*        if (characterController.isGrounded == true && Mathf.Abs(movementDirections.y) > 2)
                {
                    if (Land != null)
                        Land.Invoke(movementDirections);
                }*/

        if (isSliding == true)
        {
            Vector3 velocity = _slopeSlideVelocity;
            velocity.y = ySpeed;

            characterController.Move(velocity * Time.deltaTime);
        }
    }

    private void TargetControlMove()
    {
/*        if (IsMoveAction == false)
        {*/
            DirectionControl = Vector3.MoveTowards(DirectionControl, TargetDirectionControl, Time.deltaTime * accelerationRate);

        if (_slopeSlideVelocity == Vector3.zero)
        {
            isSliding = false;
        }
        else
            isSliding = true;
        /*        }
                else
                {
                    var dist = Vector3.Distance(transform.position, targetMoveToInteractPoint);
                    transform.LookAt(targetMoveToInteractPoint);

                    if (dist > 1f)
                    {
                        DirectionControl = Vector3.MoveTowards(DirectionControl, targetMoveToInteractPoint.normalized, Time.deltaTime * accelerationRate);
                    }
                    else
                        IsMoveAction = false;*/
            /*            else
                        {
                            List<EntityContextAction> actionsList = targetActionCollector.GetActionList<EntityContextAction>();

                            for (int i = 0; i < actionsList.Count; i++)
                            {

                                actionsList[i].StartAction();
                            }

                            IsMoveAction = false;
                        }*/


    }
    public void Sprint()
    {
        if (IsGrounded == false) return;
        if (IsCrouch == true) return;

        if (IsSprint == true) IsSprint = false;
        else IsSprint = true;
    }
        /*
            public void UnSprint()
            {
                IsSprint = false;
            }*/

        public void Jump()
    {
        if (IsGrounded == false) return;
        if (IsFight == true || IsCrouch == true) return;

        IsJump = true;
    }

    public float GetCurrentSpeedByState()
    {
        if (IsSprint == true)
            return _runSpeed;
        else
            return _walkSpeed;
    }

    private void SetSlopeSlide()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, 1f))
        {
            float angle = Vector3.Angle(hitInfo.normal,Vector3.up);

            if (angle >= characterController.slopeLimit)
            {
                _slopeSlideVelocity = Vector3.ProjectOnPlane(new Vector3(0, ySpeed, 0), hitInfo.normal);
                return;
            }   
        }

        if (isSliding == true)
        { 
            _slopeSlideVelocity -= _slopeSlideVelocity * Time.deltaTime * 3;

            if (_slopeSlideVelocity.magnitude > 1)
            {
                return;
            }
        }

        _slopeSlideVelocity = Vector3.zero;
    }

    private void UpdateDistanceToGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 20) == true)
        {
            distanceToGround = Vector3.Distance(transform.position, hit.point);
        }
    }
}
