
using UnityEngine;

namespace ZombieFarm.Views.Player
{
    [RequireComponent(typeof(PlayerMover), typeof(Animator))]
    public class MainCharAnimator : MonoBehaviour
    {
        private PlayerMover playerMover;
        private Animator animator;
        private float motionSpeed = 0;
        private float stateTransitionStep = 0.02f;

        private const string animatorParameter_CurrentMotionSpeed_Float_Name = "CurrentMotionSpeed";
        private static readonly int animatorParameter_CurrentMotionSpeed_Float_Id = Animator.StringToHash(animatorParameter_CurrentMotionSpeed_Float_Name);

        private void Awake()
        {
            playerMover = GetComponent<PlayerMover>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            RunTransitionSmoothly();
        }

        private void RunTransitionSmoothly()
        {
            if (playerMover.CurrentMotionSpeed == motionSpeed)
            {
                return;
            }

            motionSpeed = Mathf.MoveTowards(motionSpeed, playerMover.CurrentMotionSpeed, stateTransitionStep);
            animator.SetFloat(animatorParameter_CurrentMotionSpeed_Float_Id, motionSpeed);
        }
    }
}
