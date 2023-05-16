
using UnityEngine;

namespace ZombieFarm.Views.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMover : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float movementSpeed = 1;
        [SerializeField] private float rotationSpeed = 1;
        [SerializeField] private float gravity = 9.8f;

        private CharacterController characterController;

        private bool isJoystickActive = false;

        internal float CurrentMotionSpeed => Root.UIManager.Joystick.GetCurrentMoveCommand().magnitude;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            Root.UIManager.Joystick.OnPointerStateChanged += OnPointerStateChanged;
        }

        private void OnDestroy()
        {
            Root.UIManager.Joystick.OnPointerStateChanged -= OnPointerStateChanged;
        }

        private void OnPointerStateChanged(bool pointerDown)
        {
            isJoystickActive = pointerDown;
        }

        private void FixedUpdate()
        {
            if (isJoystickActive == false)
            {
                return;
            }

            Move();
            Rotate();
        }

        private void Move()
        {
            Vector3 motionJoystick = Root.UIManager.Joystick.GetCurrentMoveCommand();
            motionJoystick *= movementSpeed * Time.deltaTime;
            motionJoystick.y -= gravity;

            characterController.Move(motionJoystick);
        }

        private void Rotate()
        {
            Vector3 target = Root.UIManager.Joystick.GetCurrentMoveCommand();
            Quaternion targetRotation = Quaternion.LookRotation(target, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        }
    }
}
