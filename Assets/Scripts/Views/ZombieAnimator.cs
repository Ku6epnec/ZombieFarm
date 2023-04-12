using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieFarm.AI;

namespace ZombieFarm.Views
{
    [RequireComponent(typeof(Zombie), typeof(Animator))]
    public class ZombieAnimator : MonoBehaviour
    {
        [SerializeField] private float secondsBeforeDisappearance = 2.9f;
        [SerializeField] private ZombieVfx disappearVFX;
        [SerializeField] private Transform zombieModel;

        private Zombie zombie;
        private Animator animator;

        private const string animatorParameter_StateIndex_Int_Name = "State";
        private const string animatorParameter_Death_Trigger_Name = "Death";

        private static readonly int animatorParameter_StateIndex_Int_Id =
            Animator.StringToHash(animatorParameter_StateIndex_Int_Name);

        private static readonly int animatorParameter_Death_Trigger_Id =
            Animator.StringToHash(animatorParameter_Death_Trigger_Name);

        private void Awake()
        {
            zombie = GetComponent<Zombie>();
            animator = GetComponent<Animator>();

            zombie.OnChangeState += ChangeState;
            zombie.OnDie += Die;
        }

        private void OnDestroy()
        {
            zombie.OnChangeState -= ChangeState;
            zombie.OnDie -= Die;
        }

        private void ChangeState(ZombieState newState)
        {
            animator.SetInteger(animatorParameter_StateIndex_Int_Id, (int)newState);
        }

        private void Die(Zombie _)
        {
            animator.SetTrigger(animatorParameter_Death_Trigger_Id);

            StartCoroutine(WaitingForAction(secondsBeforeDisappearance, () => StartVFX()));
        }

        private void StartVFX()
        {
            zombieModel.gameObject.SetActive(false);
            disappearVFX.gameObject.SetActive(true);

            StartCoroutine(WaitingForAction(disappearVFX.GetMaxParticleDuration(), () => Destroy(gameObject)));
        }
        
        private IEnumerator WaitingForAction(float time, Action action)
        {
            yield return new WaitForSeconds(time);

            action();
        }

    }
}
