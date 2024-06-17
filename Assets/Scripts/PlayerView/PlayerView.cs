using System;
using UnityEngine;
using ZombieFarm.Interfaces;

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

        private void Start()
        {
            config = Root.ConfigManager.GameSettings.Player;

            OnRefreshProgressBarState(true);
        }

        private void OnDestroy()
        {
            if(playerProfile != null)
            {
                playerProfile.OnDie -= OnDie;
            }
        }

        public void Init(IPlayerProfile playerProfile)
        {
            this.playerProfile = playerProfile;
            
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
