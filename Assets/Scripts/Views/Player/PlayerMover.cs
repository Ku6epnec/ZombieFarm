
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

        [SerializeField] private FloatingJoystick _joystick;

        private Vector2 moveCommand;
        private PlayerInput playerInput;
        private CharacterController characterController;

        private bool MousePress = false;

        internal float CurrentMotionSpeed => moveCommand.magnitude;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            characterController = GetComponent<CharacterController>();

            playerInput.onActionTriggered += OnPlayerInputActionTriggered;
        }

        private void OnPlayerInputActionTriggered(InputAction.CallbackContext context)
        {
            switch (context.action.name)
            {
                case "Move":
                    if (MousePress)
                        moveCommand = context.action.ReadValue<Vector2>();
                    else moveCommand = Vector2.zero;
                    break;

                default:
                    Debug.LogError("Wrong action name!");
                    break;
            }
        }

        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))

            {
                MousePress = true;
                Debug.Log("Нажали на ЛКМ");
            }

            if (moveCommand == Vector2.zero)
            {
                return;
            }

            Move();
            Rotate();

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                MousePress = false;
                Debug.Log("Отпустили ЛКМ");
            }
        }

        private void Move()
        {
            Vector3 motion = new Vector3(-moveCommand.x, 0, -moveCommand.y);
            motion *= movementSpeed * Time.deltaTime;
            motion.y -= gravity;

            Vector3 motionJoystick = new Vector3(-_joystick.Horizontal, 0, -_joystick.Vertical);
            motionJoystick *= movementSpeed * Time.deltaTime;
            motionJoystick.y -= gravity;

            characterController.Move(motionJoystick);
        }

        private void Rotate()
        {
            Vector3 target = new Vector3(-_joystick.Horizontal, 0, -_joystick.Vertical);
            Quaternion targetRotation = Quaternion.LookRotation(target, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        }
    }
}
