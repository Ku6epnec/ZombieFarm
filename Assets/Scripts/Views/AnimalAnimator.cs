
using UnityEngine;

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
            animalFollow.OnStartFollowing += OnStartFollowing;
            animator = GetComponent<Animator>();
        }

        private void OnDestroy()
        {
            animalFollow.OnStartFollowing -= OnStartFollowing;
        }

        private void OnStartFollowing()
        {
            animator.SetBool(animatorParameter_StateIndex_Bool_Id, true);
        }

    }
}



