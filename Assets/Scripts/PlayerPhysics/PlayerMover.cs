
using UnityEngine;
using UnityEngine.AI;
using ZombieFarm.Interfaces;

namespace ZombieFarm.Views.Player
{
    public class PlayerMover : MonoBehaviour, IPlayerMover
    {
        public float CurrentMotionSpeed => motionVector.magnitude;
        [SerializeField] private NavMeshAgent navMeshAgent;

        private PlayerConfig config;
        private IJoystick joystick;
        private Camera mainCamera;
        private Vector3 motionVector;

        private void Start()
        {
            config = Root.ConfigManager.GameSettings.Player;
            joystick = Root.UIManager.Joystick;
            mainCamera = Root.Camera;
        }

        private void FixedUpdate()
        {
            if (joystick.IsActive.Value == false)
            {
                motionVector = Vector3.zero;
                return;
            }

            motionVector = joystick.GetCurrentMoveCommand();
            if (motionVector != Vector3.zero)
            {
                Move();
                Rotate();
            }
        }

        private void Move()
        {
            Vector3 moveVector = motionVector * config.movementSpeed * Time.deltaTime;

            navMeshAgent.Move(moveVector);
        }

        private void Rotate()
        {
            Quaternion targetRotation = Quaternion.LookRotation(motionVector * Mathf.Cos(mainCamera.transform.rotation.y), Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, config.rotationSpeed);         
        }
    }
}
