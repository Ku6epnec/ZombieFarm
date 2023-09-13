
using UnityEngine;
using ZombieFarm.AI;

namespace ZombieFarm.Views
{
    [RequireComponent(typeof(Animator))]
    public class AnimalAnimator : MonoBehaviour
    {
        [SerializeField] private AnimalFollow animalFollow;

        private Animator animator;

        private const string animatorParameter_Run_Bool_Name = "Run";

        private static readonly int animatorParameter_StateIndex_Bool_Id = Animator.StringToHash(animatorParameter_Run_Bool_Name);

        private void Awake()
        {
            animalFollow.IsMoving.OnValueChanged += OnMovingValueChanged;
            animator = GetComponent<Animator>();
        }

        private void OnDestroy()
        {
            animalFollow.IsMoving.OnValueChanged -= OnMovingValueChanged;
        }

        private void OnMovingValueChanged(bool value)
        {
            animator.SetBool(animatorParameter_StateIndex_Bool_Id, value);
        }

    }
}



