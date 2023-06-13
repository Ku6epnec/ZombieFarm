using System;
using UnityEngine;
using ZombieFarm.Interfaces;

namespace ZombieFarm.Views.Player
{ 
    public class PlayerView : MonoBehaviour, IHealth, IDamage, IHaveArmor
    {
        public event Action<PlayerState> OnChangeState = (newState) => { };
        
        private PlayerState currentPlayerState;
        private float deltaSpeed = 0.05f;
        private bool destroyObjectState = false;
        private IRemovableObject removableObject;

        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public float Armor => _armor;
        public float Damage => _damage;

        [Header("HealthStats")]
        [SerializeField] private float _health = 20;
        [SerializeField] private float _maxHealth = 20;

        [Header("ArmorStats")]
        [SerializeField] private float _armor = 0;

        [Header("DamageStats")]
        [SerializeField] private float _damage = 1;

        [Header("References")]
        [SerializeField] private ProgressBar healthProgressBar;

        [Header("InteractiveArea")]
        [SerializeField] private InteractiveArea interactiveArea;
        [SerializeField] private AttackTrigger attackTrigger;

        private void Awake()
        {
            currentPlayerState = PlayerState.Idle;
            healthProgressBar.InitSlider(MaxHealth);
            healthProgressBar.ProcessCompleted += Die;

            interactiveArea.Interactive += OnAttack;
            interactiveArea.DeInteractive += OnIdle;
        }

        private void OnDestroy()
        {
            Root.ZombieManager.OnMonsterAttack -= OnAttack;
            healthProgressBar.ProcessCompleted -= Die;
        }

        public void OnIdle()
        {
            RefreshCurrentState(PlayerState.Idle);
        }

        public void OnAttack()
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

        public void ReceivedDamage(float damage)
        {
            damage = damage - _armor;
            if (damage > 0)
            {
                _health -= damage;
                healthProgressBar.StartProgress(_health);
            }
        }

        private void Die()
        {
            healthProgressBar.gameObject.SetActive(false);
            Destroy(gameObject);
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
