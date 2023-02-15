
using UnityEngine;
using UnityEngine.InputSystem;

namespace ZombieFarm.Views
{
    [RequireComponent(typeof(PlayerInput), typeof(CharacterController))]
    public class PlayerMover : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float movementSpeed = 1;
        [SerializeField] private float gravity = 9.8f;

        private Vector2 moveCommand;
        private PlayerInput playerInput;
        private CharacterController characterController;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            characterController = GetComponent<CharacterController>();

            playerInput.onActionTriggered += OnPlayerInputActionTriggered;
        }

        private void FixedUpdate()
        {
            StartMoving();
        }

        private void OnPlayerInputActionTriggered(InputAction.CallbackContext context)
        {
            switch (context.action.name)
            {
                case "Move":
                    moveCommand = context.action.ReadValue<Vector2>();
                    break;

                default:
                    Debug.LogError("Wrong action name!");
                    break;
            }
        }

        private void StartMoving()
        {
            Vector3 motion = new Vector3(moveCommand.x, 0, moveCommand.y);
            motion *= movementSpeed * movementSpeed * Time.deltaTime;
            motion.y -= gravity;

            characterController.Move(transform.rotation * motion);
        }
    }
}
