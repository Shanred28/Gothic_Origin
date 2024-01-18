using UnityEngine;

public  class CharacterMovementHuman : MonoBehaviour
{
    private CharacterController characterController;
    public Vector3 DirectionControl;
    private Vector3 movementDirections;

    private float distanceToGround;
    [SerializeField] private float _runSpeed;

    public float DistanceToGround => distanceToGround;
    public bool IsGrounded => distanceToGround < 0.09f;
    public float CurrentSpeed => GetCurrentSpeedByState();

    public Vector3 TargetDirectionControl;
    [SerializeField] private float accelerationRate;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        TargetControlMove();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (IsGrounded == true)
        {
            movementDirections = DirectionControl * GetCurrentSpeedByState();
/*            if (isJump == true)
            {
                movementDirections.y = jumpSpeed;
                isJump = false;
            }*/

            movementDirections = transform.TransformDirection(movementDirections);
        }
        movementDirections += Physics.gravity * Time.fixedDeltaTime;
/*        if (UpdatePosition == true)*/
            characterController.Move(movementDirections * Time.fixedDeltaTime);

/*        if (characterController.isGrounded == true && Mathf.Abs(movementDirections.y) > 2)
        {
            if (Land != null)
                Land.Invoke(movementDirections);
        }*/
    }

    private void TargetControlMove()
    {
/*        if (IsMoveAction == false)
        {*/
            DirectionControl = Vector3.MoveTowards(DirectionControl, TargetDirectionControl, Time.deltaTime * accelerationRate);
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

    public float GetCurrentSpeedByState()
    {


        return _runSpeed;
    }
}
