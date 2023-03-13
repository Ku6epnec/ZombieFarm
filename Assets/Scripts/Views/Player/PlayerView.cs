using System;
using UnityEngine;

namespace ZombieFarm.Views.Player
{ 
    public class PlayerView : MonoBehaviour
    {
        public event Action<PlayerState> OnChangeState = (newState) => { };
        
        private PlayerState currentPlayerState;
        private float deltaSpeed = 0.05f;

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

        private void Update()
        {
            if (Root.Player.CurrentMotionSpeed > deltaSpeed)
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
    }
}
