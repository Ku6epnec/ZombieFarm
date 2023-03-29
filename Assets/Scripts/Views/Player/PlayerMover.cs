
using UnityEngine;
using UnityEngine.InputSystem;

namespace ZombieFarm.Views.Player
{
    [RequireComponent(typeof(PlayerInput), typeof(CharacterController))]
    public class PlayerMover : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float movementSpeed = 1;
        [SerializeField] private float rotationSpeed = 1;
        [SerializeField] private float gravity = 9.8f;

        [SerializeField] private DynamicJoystick _joystick;

        private Vector2 moveCommand;
        private PlayerInput playerInput;
        private CharacterController characterController;
        private Rigidbody _rigidbody;

        internal float CurrentMotionSpeed => moveCommand.magnitude;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            characterController = GetComponent<CharacterController>();
            _rigidbody = GetComponent<Rigidbody>();

            playerInput.onActionTriggered += OnPlayerInputActionTriggered;
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

        private void FixedUpdate()
        {
            if (moveCommand == Vector2.zero)
            {
                return;
            }

            Move();
            Rotate();
        }

        private void Move()
        {
            Vector3 motion = new Vector3(-moveCommand.x, 0, -moveCommand.y);
            motion *= movementSpeed * Time.deltaTime;
            motion.y -= gravity;

            _rigidbody.velocity = new Vector3(_joystick.Horizontal * movementSpeed, _rigidbody.velocity.y, _joystick.Vertical * movementSpeed);

            characterController.Move(motion);
        }

        private void Rotate()
        {
            Vector3 target = new Vector3(-moveCommand.x, 0, -moveCommand.y);
            Quaternion targetRotation = Quaternion.LookRotation(target, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        }
    }
}
