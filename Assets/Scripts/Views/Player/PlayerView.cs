using System;
using UnityEngine;
using ZombieFarm.Interfaces;

namespace ZombieFarm.Views.Player
{ 
    public class PlayerView : MonoBehaviour, IHealth, IDamage, IHaveArmor, ISpawn, IItemWithHealthBar
    {
        public event Action<PlayerState> OnChangeState = (newState) => { };
        public event Action<float> OnRefreshProgress = (lostProgress) => { };
        public event Action<bool> OnRefreshProgressBarState = (isActive) => { };
        public event Action OnResetProgress = () => { };

        private PlayerState currentPlayerState;
        private float deltaSpeed = 0.05f;
        private bool destroyObjectState = false;

        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public float Armor => _armor;
        public float Damage => _damage;

        public Vector3 SpawnPoint => _spawnPoint;

        public float MaxHealthBarValue => _maxHealth;

        private Vector3 _spawnPoint;
        private Quaternion _spawnRotation;

        [Header("HealthStats")]
        [SerializeField] private float _health = 20;
        [SerializeField] private float _maxHealth = 20;

        [Header("ArmorStats")]
        [SerializeField] private float _armor = 0;

        [Header("DamageStats")]
        [SerializeField] private float _damage = 1;

        [Header("InteractiveArea")]
        [SerializeField] private InteractiveArea interactiveArea;
        [SerializeField] private AttackTrigger attackTrigger;

        private void Start()
        {
            _spawnPoint = transform.localPosition;
            _spawnRotation = transform.localRotation;
            OnSpawn();
        }

        private void OnDestroy()
        {
            Root.ZombieManager.OnMonsterAttack -= OnAttack;
            interactiveArea.OnDeInteractive -= OnIdle;
        }

        public void OnIdle()
        {
            RefreshCurrentState(PlayerState.Idle);
        }

        public void OnAttack()
        {                    
            RefreshCurrentState(PlayerState.Attack);
        }

        public void ReceivedDamage(float damage)
        {
            damage = damage - _armor;
            if (damage > 0)
            {
                _health -= damage;
                OnRefreshProgress(_health);

                if (_health < 0)
                {
                    Die();
                }
            }
        }

        private void Die()
        {
            OnRefreshProgressBarState(false);
            OnResetProgress();
            Root.ZombieManager.OnMonsterAttack -= OnAttack;
            interactiveArea.OnDeInteractive -= OnIdle;
            OnSpawn();
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

        public void OnSpawn()
        {
            interactiveArea.OnInteractive += OnAttack;
            interactiveArea.OnDeInteractive += OnIdle;

            transform.localPosition = SpawnPoint;
            transform.localRotation = _spawnRotation;
            RefreshCurrentState(PlayerState.Idle);
            _health = MaxHealth;
            interactiveArea.Clean();
            OnRefreshProgressBarState(true);
        }
    }
}
