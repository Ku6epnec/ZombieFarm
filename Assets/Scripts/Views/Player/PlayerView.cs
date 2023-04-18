using System;
using UnityEngine;

namespace ZombieFarm.Views.Player
{ 
    public class PlayerView : MonoBehaviour
    {
        public event Action<PlayerState> OnChangeState = (newState) => { };
        
        private PlayerState currentPlayerState;
        private float deltaSpeed = 0.05f;
        private bool destroyObjectState = true;
        private IRemovableObject removableObject;

        private void Start()
        {
            currentPlayerState = PlayerState.Idle;

            Root.ZombieManager.OnMonsterAttack += OnAttack;
        }

        private void OnDestroy()
        {
            Root.ZombieManager.OnMonsterAttack -= OnAttack;
        }

        private void OnAttack()
        {
            RefreshCurrentState(PlayerState.Attack);
        }

        public void DestroyObject(bool inProcess)
        {
            destroyObjectState = inProcess;

            if (inProcess == true)
            {
                OnAttack();
            }
            else
            {
                RefreshCurrentState(PlayerState.Idle);
                removableObject.OnDestroyProcess -= DestroyObject;
                removableObject = null;
            }
        }

        private void Update()
        {
            if (Root.Player.CurrentMotionSpeed > deltaSpeed && destroyObjectState == false)
            {
                RefreshCurrentState(PlayerState.Idle);
            }
        }

        private void RefreshCurrentState(PlayerState playerState)
        {
            PlayerState lastZombieState = currentPlayerState;
            currentPlayerState = playerState;

            if (currentPlayerState != lastZombieState)
            {
                OnChangeState(currentPlayerState);
            }
        }

        public void RegisterRemovableObject(IRemovableObject removableObject)
        {
            this.removableObject = removableObject;
            this.removableObject.OnDestroyProcess += DestroyObject;
        }
    }
}
