using UnityTools.Runtime.StatefulEvent;
using ZombieFarm.Managers;
using ZombieFarm.Interfaces;

namespace ZombieFarm.Views.Player
{
    public class PlayerStateMachine: IPlayerStateMachine
    {
        public IStatefulEvent<PlayerState> CurrentPlayerState => currentPlayerState;
        private StatefulEventInt<PlayerState> currentPlayerState = StatefulEventInt.CreateEnum(PlayerState.Idle);

        private const float deltaSpeed = 0.05f;

        private readonly ZombieManager zombieManager;
        private readonly InteractiveArea interactiveArea;
        private readonly PlayerMover playerMover;

        internal PlayerStateMachine(InteractiveArea interactiveArea, PlayerMover playerMover)
        {
            this.interactiveArea = interactiveArea;
            this.playerMover = playerMover;

            Enter();
        }

        private void Enter()
        {
            interactiveArea.OnInteractive += SetAttack;
            interactiveArea.OnDeInteractive += SetIdle;
        }

        public void Update()
        {
            if (playerMover.CurrentMotionSpeed > deltaSpeed)
            {
                SetIdle();
            }
        }

        private void Exit()
        {
            interactiveArea.OnInteractive -= SetAttack;
            interactiveArea.OnDeInteractive -= SetIdle;
        }

        public void SetDeath() => RefreshCurrentState(PlayerState.Death);
        
        private void SetAttack() => RefreshCurrentState(PlayerState.Attack);
        
        private void SetIdle() => RefreshCurrentState(PlayerState.Idle);

        private void RefreshCurrentState(PlayerState playerState)
        {
            currentPlayerState.Set(playerState);

            if(playerState == PlayerState.Death)
            {
                Exit();
            }
        }
    }
}
