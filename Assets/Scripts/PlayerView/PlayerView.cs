using System;
using UnityEngine;
using ZombieFarm.Interfaces;
using ZombieFarm.Managers.Interfaces;

namespace ZombieFarm.Views.Player
{ 
    public class PlayerView : MonoBehaviour, IPlayerView, IItemWithHealthBar
    {
        public event Action<float> OnRefreshProgress = (lostProgress) => { };
        public event Action<bool> OnRefreshProgressBarState = (isActive) => { };
        public event Action OnResetProgress = () => { };
        public float MaxHealthBarValue => config.maxHealth;

        private PlayerConfig config;
        private IPlayerProfile playerProfile;
        private IPlayerManager playerManager;

        private void Start()
        {
            config = Root.ConfigManager.GameSettings.Player;

            OnRefreshProgressBarState(true);
        }

        private void Update()
        {
            if(playerManager == null)
            {
                return;
            }

            transform.position = playerManager.PlayerTransform.position;
            transform.rotation = playerManager.PlayerTransform.rotation;
        }

        private void OnDestroy()
        {
            if(playerProfile != null)
            {
                playerProfile.OnDie -= OnDie;
            }
        }

        public void Init(IPlayerProfile playerProfile, IPlayerManager playerManager)
        {
            this.playerProfile = playerProfile;
            this.playerManager = playerManager;

            playerProfile.OnDie += OnDie;
        }

        public void RefreshHealthBar()
        {
            OnRefreshProgress(playerProfile.CurrentHealth.Value);
        }

        private void OnDie()
        {
            OnRefreshProgressBarState(false);
            OnResetProgress();
        }
    }
}
