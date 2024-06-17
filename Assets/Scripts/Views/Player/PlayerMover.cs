
using UnityEngine;
using UnityEngine.AI;

namespace ZombieFarm.Views.Player
{
    public class PlayerMover : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float movementSpeed = 1;
        [SerializeField] private float rotationSpeed = 1;
        [SerializeField] private float gravity = 9.8f;
        [SerializeField] private NavMeshAgent navMeshAgent;

        private bool isJoystickActive = false;

        private Vector3 target;
        private Camera mainCamera;

        internal float CurrentMotionSpeed => Root.UIManager.Joystick.GetCurrentMoveCommand().magnitude;


        private void Start()
        {
            this.mainCamera = Root.Camera;

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

            target = Root.UIManager.Joystick.GetCurrentMoveCommand();
            if (target != Vector3.zero)
            {
                Move();
                Rotate();
            }
        }

        private void Move()
        {
            Vector3 motionJoystick = target;
            motionJoystick *= movementSpeed * Time.deltaTime;
            motionJoystick.y -= gravity;

            navMeshAgent.Move(motionJoystick);
        }

        private void Rotate()
        {
            Quaternion targetRotation = Quaternion.LookRotation(target * Mathf.Cos(mainCamera.transform.rotation.y), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);         
        }
    }
}
