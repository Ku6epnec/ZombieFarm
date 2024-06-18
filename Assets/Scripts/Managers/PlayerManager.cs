using UnityEngine;
using ZombieFarm.Views.Player;
using ZombieFarm.Interfaces;
using UnityTools.Runtime.StatefulEvent;

namespace ZombieFarm.Managers.Interfaces
{
    public class PlayerManager : MonoBehaviour, IPlayerManager
    {
        public IItemWithHealthBar PlayerItemWithHealthBar => playerView;
        public IStatefulEvent<PlayerState> CurrentPlayerState => playerStateMachine.CurrentPlayerState;
        public Transform PlayerTransform => playerMover.gameObject.transform;
        public float CurrentMotionSpeed => playerMover.CurrentMotionSpeed;

        [SerializeField] private PlayerMover playerMover;
        [SerializeField] private PlayerView playerView;
        private IPlayerStateMachine playerStateMachine;
        private IPlayerProfile playerProfile;

        [Header("TempValues")]
        [SerializeField] private InteractiveArea interactiveArea;

        private void Awake()
        {
            playerStateMachine = new PlayerStateMachine(interactiveArea, playerMover);
            playerStateMachine.CurrentPlayerState.OnValueChanged += OnPlayerStateChanged;
        }

        private void Start()
        {
            PlayerProfile profile = new PlayerProfile();

            playerProfile = profile;

            profile.Init();
            playerView.Init(playerProfile, this);
        }

        private void FixedUpdate()
        {
            playerStateMachine.Update();   
        }

        public void RegisterDamage(float damageValue)
        {
            playerProfile.RegisterDamage(damageValue);
            playerView.RefreshHealthBar();
        }

        //TODO: make gameScenario script, replace this logic to new script
        //scenario must subscribe on playerState and when it is "Death" - start transition
        private void OnPlayerStateChanged(PlayerState playerState)
        {
            if(playerState == PlayerState.Death)
            {
                Root.TransitionManager.StartTransition();
            }
        }
    }
}
