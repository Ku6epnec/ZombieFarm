using System;
using UnityEngine;
using ZombieFarm.Interfaces;

namespace ZombieFarm.Views.Player
{ 
    public class PlayerView : MonoBehaviour, IHealth, IDamage
    {
        public event Action<PlayerState> OnChangeState = (newState) => { };
        
        private PlayerState currentPlayerState;
        private float deltaSpeed = 0.05f;
        private bool destroyObjectState = false;
        private IRemovableObject removableObject;

        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public float Damage => _damage;

        [Header("HealthStats")]
        public float _health = 20;
        public float _maxHealth = 20;

        [Header("DamageStats")]
        public float _damage = 1;

        [Header("References")]
        [SerializeField] private ProgressBar healthProgressBar;

        private void Awake()
        {
            currentPlayerState = PlayerState.Idle;
            healthProgressBar.InitSlider(MaxHealth);
            healthProgressBar.ProcessCompleted += Die;

            Root.ZombieManager.OnMonsterAttack += OnAttack;
        }

        private void OnDestroy()
        {
            Root.ZombieManager.OnMonsterAttack -= OnAttack;
            healthProgressBar.ProcessCompleted -= Die;
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

        public void RecievedDamage(float damage)
        {
            _health -= damage;
            healthProgressBar.StartProgress(_health);
        }

        private void Die()
        {
            //currentState = ZombieState.Die;
            healthProgressBar.gameObject.SetActive(false);
            Destroy(gameObject);
            //OnDie(this);
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
