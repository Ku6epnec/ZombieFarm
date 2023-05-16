
using UnityEngine;
using UnityEngine.InputSystem;

namespace ZombieFarm.Views.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMover : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float movementSpeed = 1;
        [SerializeField] private float rotationSpeed = 1;
        [SerializeField] private float gravity = 9.8f;

        [SerializeField] private FloatingJoystick floatingJoystick;

        private CharacterController characterController;

        private bool isJoystickActive = false;

        internal float CurrentMotionSpeed => GetCurrentMoveCommand().magnitude;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();

            floatingJoystick.OnPointerStateChanged += OnPointerStateChanged;
        }

        private void OnPointerStateChanged(bool pointerDown)
        {
            isJoystickActive = pointerDown;
        }

        private Vector3 GetCurrentMoveCommand() => isJoystickActive == true ? new Vector3(-floatingJoystick.Horizontal, 0, -floatingJoystick.Vertical) : Vector3.zero;

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
            Vector3 motionJoystick = GetCurrentMoveCommand();
            motionJoystick *= movementSpeed * Time.deltaTime;
            motionJoystick.y -= gravity;

            characterController.Move(motionJoystick);
        }

        private void Rotate()
        {
            Vector3 target = GetCurrentMoveCommand();
            Quaternion targetRotation = Quaternion.LookRotation(target, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        }
    }
}
