using System;
using UnityEngine;
using ZombieFarm.Interfaces;

namespace ZombieFarm.Views.Player
{ 
    public class PlayerView : MonoBehaviour, IHealth, IDamage, IHaveArmor, ISpawn
    {
        public event Action<PlayerState> OnChangeState = (newState) => { };
        
        private PlayerState currentPlayerState;
        private float deltaSpeed = 0.05f;
        private bool destroyObjectState = false;

        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public float Armor => _armor;
        public float Damage => _damage;

        public Vector3 SpawnPoint => _spawnPoint;

        private Vector3 _spawnPoint;
        private Quaternion _spawnRotation;

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
        }

        private void Start()
        {
            _spawnPoint = transform.localPosition;
            _spawnRotation = transform.localRotation;
            OnSpawn();

        }

        private void OnDestroy()
        {
            Root.ZombieManager.OnMonsterAttack -= OnAttack;
            healthProgressBar.ProcessCompleted -= Die;
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
                healthProgressBar.RefreshProgress(_health);
            }
        }

        private void Die()
        {
            healthProgressBar.gameObject.SetActive(false);
            Root.ZombieManager.OnMonsterAttack -= OnAttack;
            healthProgressBar.ProcessCompleted -= Die;
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
            healthProgressBar.ProcessCompleted += Die;
            interactiveArea.OnInteractive += OnAttack;
            interactiveArea.OnDeInteractive += OnIdle;

            //transform.localEulerAngles = new Vector3(0, 0, 0);
            transform.localPosition = SpawnPoint;
            transform.localRotation = _spawnRotation;
            Debug.Log("Local position " + transform.localPosition);
            RefreshCurrentState(PlayerState.Idle);
            _health = MaxHealth;
            interactiveArea.Clean();
            healthProgressBar.InitSlider(MaxHealth);
        }
    }
}
