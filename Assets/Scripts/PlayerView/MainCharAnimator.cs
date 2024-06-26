
using UnityEngine;
using ZombieFarm.Managers.Interfaces;

namespace ZombieFarm.Views.Player
{
    [RequireComponent(typeof(Animator))]
    public class MainCharAnimator : MonoBehaviour
    {
        private IPlayerManager player;
        private Animator animator;
        private float motionSpeed = 0;
        private float stateTransitionStep = 0.02f;

        private const string animatorParameter_CurrentMotionSpeed_Float_Name = "CurrentMotionSpeed";
        private const string animatorParameter_StateIndex_Int_Name = "State";

        private static readonly int animatorParameter_CurrentMotionSpeed_Float_Id = Animator.StringToHash(animatorParameter_CurrentMotionSpeed_Float_Name);
        private static readonly int animatorParameter_StateIndex_Int_Id = Animator.StringToHash(animatorParameter_StateIndex_Int_Name);

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            player = Root.PlayerManager;
            player.CurrentPlayerState.OnValueChanged += OnChangeState;
        }

        private void OnDestroy()
        {
            player.CurrentPlayerState.OnValueChanged -= OnChangeState;
        }

        void Update()
        {
            RunTransitionSmoothly();
        }

        private void RunTransitionSmoothly()
        {
            if (Root.PlayerManager.CurrentMotionSpeed == motionSpeed)
            {
                return;
            }

            motionSpeed = Mathf.MoveTowards(motionSpeed, Root.PlayerManager.CurrentMotionSpeed, stateTransitionStep);
            animator.SetFloat(animatorParameter_CurrentMotionSpeed_Float_Id, motionSpeed);
        }

        private void OnChangeState(PlayerState newState)
        {
            animator.SetInteger(animatorParameter_StateIndex_Int_Id, (int)newState);
        }
    }
}
