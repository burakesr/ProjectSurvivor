using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControl : MonoBehaviour
{
    [Header("GENERAL")]
    [SerializeField] private CharacterConfigSO config;

    private PlayerInputControl input;
    private AnimatorController animController;
    private Rigidbody rb;

    private Vector3 movementVector;
    private float turnSmoothVelocity;
    private bool isGrounded;


    private const float moveSpeedConstant = 100f;

    private void Awake()
    {
        input = GetComponent<PlayerInputControl>();
        animController = GetComponent<AnimatorController>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        RaycastHit groundCheckResult = GroundCheckHandler();
        
        PlayerMoveHandler(groundCheckResult);
    }

    private void PlayerMoveHandler(RaycastHit groundCheckResult)
    {
        Vector2 inputDirection = new Vector2(input.move.x, input.move.y).normalized;

        movementVector = new Vector3(inputDirection.x, 0f, inputDirection.y).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movementVector.x, movementVector.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, config.turnSmoothness);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            animController.OnCharacterWalking();
        }
        else
        {
            animController.OnCharacterIdle();
        }

        if (isGrounded)
        {
            // project onto the current surface
            movementVector = Vector3.ProjectOnPlane(movementVector, groundCheckResult.normal);

            // If trying to move up to steep a slope
            if (movementVector.y > 0 && Vector3.Angle(Vector3.up, groundCheckResult.normal) > config.slopeLimit)
            {
                movementVector = Vector3.zero;
            }
        }
        else
        {
            movementVector.y = config.gravityForce * Time.fixedDeltaTime;
        }

        float currentSpeed = (moveSpeedConstant * config.moveSpeed);
        rb.velocity = movementVector * currentSpeed * Time.fixedDeltaTime;
    }

    private RaycastHit GroundCheckHandler()
    {
        RaycastHit hit;
        Vector3 startPos = rb.position + Vector3.up * config.height * 0.5f;
        
        float groundCheckRadius = config.radius + config.groundCheckRadiusBuffer;
        float groundCheckDistance = (config.height * 0.5f) - config.radius + config.groundCheckBuffer;

        if (Physics.SphereCast(startPos, groundCheckRadius, Vector3.down,
            out hit, groundCheckDistance, config.groundCheckLayer, QueryTriggerInteraction.Ignore))
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
