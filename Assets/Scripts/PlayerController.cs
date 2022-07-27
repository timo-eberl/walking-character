using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField, Range(0f, 6f)] private float walkSpeed = 3f;
    [SerializeField, Range(0f, 6f)] private float sprintSpeed = 6f;
    [SerializeField, Min(0f)] private float acceleration = 8f;
    [SerializeField, Min(0f)] private float deceleration = 16f;
    [SerializeField, Range(0f, 1f)] private float rotationSpeed = 0.8f;

    private CharacterController characterController;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction sprintAction;

    private float currentVelocity = 0f;
    private float currentAngle = 0f;
    private float currentAngleVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        playerInput = new PlayerInput();
        playerInput.Player.Enable();
        moveAction = playerInput.asset.FindAction("Player/Move");
        sprintAction = playerInput.asset.FindAction("Player/Sprint");
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 moveInputDirection = moveAction.ReadValue<Vector2>();
        bool sprintInputPressed = sprintAction.IsPressed();
        bool moveInputPressed = moveAction.IsPressed();

        // rotation
        float targetAngle = currentAngle;
        if (moveInputPressed)
        {
            targetAngle = Mathf.Atan2(moveInputDirection.x, moveInputDirection.y) * Mathf.Rad2Deg;
        }
        currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, 1f - rotationSpeed) % 360f;
        playerTransform.rotation = Quaternion.Euler(0, currentAngle, 0);
    }
}
