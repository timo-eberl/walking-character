using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0f, 6f)] private float walkSpeed = 3f;
    [SerializeField, Range(0f, 6f)] private float sprintSpeed = 6f;
    [SerializeField, Min(0f)] private float acceleration = 8f;
    [SerializeField, Min(0f)] private float deceleration = 16f;

    private CharacterController characterController;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction sprintAction;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        if (playerInput == null)
        {
            playerInput = new PlayerInput();
        }
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
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        bool sprintInput = sprintAction.IsPressed();

        Debug.Log("moveInput: " + moveInput);
    }
}
