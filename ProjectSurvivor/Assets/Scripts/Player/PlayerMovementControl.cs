using UnityEngine;

public class PlayerMovementControl : MonoBehaviour
{
    private PlayerInputControl _input;
    private AnimatorController _animController;
    private Rigidbody _rb;
    private Player _player;

    private Vector3 movementVector;
    private float turnSmoothVelocity;
    private bool isGrounded;

    private const float moveSpeedConstant = 100f;

    private void Awake()
    {
        _input = GetComponent<PlayerInputControl>();
        _animController = GetComponent<AnimatorController>();
        _rb = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        RaycastHit groundCheckResult = GroundCheckHandler();
        
        PlayerMoveHandler(groundCheckResult);
    }

    private void PlayerMoveHandler(RaycastHit groundCheckResult)
    {
        Vector2 inputDirection = new Vector2(_input.move.x, _input.move.y).normalized;

        movementVector = new Vector3(inputDirection.x, 0f, inputDirection.y).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movementVector.x, movementVector.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, _player.CharacterConfig.turnSmoothness);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            _animController.OnCharacterWalking();
        }
        else
        {
            _animController.OnCharacterIdle();
        }

        if (isGrounded)
        {
            // project onto the current surface
            movementVector = Vector3.ProjectOnPlane(movementVector, groundCheckResult.normal);

            // If trying to move up to steep a slope
            if (movementVector.y > 0 && Vector3.Angle(Vector3.up, groundCheckResult.normal) > _player.CharacterConfig.slopeLimit)
            {
                movementVector = Vector3.zero;
            }
        }
        else
        {
            movementVector.y = _player.CharacterConfig.gravityForce * Time.fixedDeltaTime;
        }

        float currentSpeed = (moveSpeedConstant * _player.CharacterConfig.moveSpeed);
        _rb.velocity = movementVector * currentSpeed * Time.fixedDeltaTime;
    }

    private RaycastHit GroundCheckHandler()
    {
        RaycastHit hit;
        Vector3 startPos = _rb.position + Vector3.up * _player.CharacterConfig.height * 0.5f;
        
        float groundCheckRadius = _player.CharacterConfig.radius + _player.CharacterConfig.groundCheckRadiusBuffer;
        float groundCheckDistance = (_player.CharacterConfig.height * 0.5f) - _player.CharacterConfig.radius + _player.CharacterConfig.groundCheckBuffer;

        if (Physics.SphereCast(startPos, groundCheckRadius, Vector3.down,
            out hit, groundCheckDistance, _player.CharacterConfig.groundCheckLayer, QueryTriggerInteraction.Ignore))
        {
            isGrounded = true;

            // Add auto parenting here

            return hit;
        }
        else
        {
            isGrounded = false;
        }
        
        return hit;
    }
}
