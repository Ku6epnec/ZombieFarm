
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
        private bool changeController = false;

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
                    moveCommand = context.action.ReadValue<Vector2>();
                    break;

                default:
                    Debug.LogError("Wrong action name!");
                    break;
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            { 
                changeController = !changeController; 
            }

            if (changeController)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    MousePress = true;
                }
                else if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    MousePress = false;
                    moveCommand = Vector2.zero;
                }
            }
        }

        private void FixedUpdate()
        {
            if (moveCommand == Vector2.zero)
            {
                return;
            }

            if (changeController)
            {
                Move(-moveCommand.x, -moveCommand.y);
                Rotate(-moveCommand.x, -moveCommand.y);
            }
            else
            {
                Move(-_joystick.Horizontal, -_joystick.Vertical);
                Rotate(-_joystick.Horizontal, -_joystick.Vertical);
            }
        }

        private void Move(float moveHorizontal, float moveVertical)
        {
           Vector3 motion = new Vector3(moveHorizontal, 0, moveVertical);
            motion *= movementSpeed * Time.deltaTime;
            motion.y -= gravity;

            characterController.Move(motion);
        }

        private void Rotate(float moveHorizontal, float moveVertical)
        {
            Vector3 target = new Vector3(moveHorizontal, 0, moveVertical);
            Quaternion targetRotation = Quaternion.LookRotation(target, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        }
    }
}
