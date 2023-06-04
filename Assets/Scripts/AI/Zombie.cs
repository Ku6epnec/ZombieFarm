
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ZombieFarm.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Zombie : MonoBehaviour, IHealth, IDamage
    {
        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public float Damage => _damage;

        [Header("HealthStats")]
        public float _health = 10;
        public float _maxHealth = 10;

        [Header("DamageStats")]
        public float _damage = 1;

        public event Action<ZombieState> OnChangeState = (newState) => { };
        public event Action<Zombie> OnDie = (Zombie) => { };

        [Header("SpeedStats")]
        [SerializeField] private float speedForWalking = 2f;
        [SerializeField] private float speedForChasing = 6f;

        [Header("DistanceStats")]
        [SerializeField] private float distanceToPlayerForAttack = 2f;
        [SerializeField] private float distanceToPlayerForChase = 15f;

        [Header("References")]
        [SerializeField] private ProgressBar healthProgressBar;

        [Header("Walking")]
        [SerializeField] private GameObject walkingPointsParent;
        [SerializeField][Range(0, 1000)] private int walkingProbability;

        private NavMeshAgent agent;
        private List<Transform> walkingPoints;
        private ZombieState currentState;

        private float timer;
        private float Maxtimer = 1.0f;

        [SerializeField] private ZombieFarm.Views.Player.PlayerView playerView;
        [SerializeField] private ZombieFarm.Views.Player.InteractiveArea interactiveArea;

        private bool IsCurrentStateUpdatableInEveryFrame => currentState == ZombieState.Chase;

        private void Awake()
        {
            playerView = FindObjectOfType<ZombieFarm.Views.Player.PlayerView>();
            interactiveArea = FindObjectOfType<ZombieFarm.Views.Player.InteractiveArea>();
            agent = GetComponent<NavMeshAgent>();
            walkingPoints = GetWalkingPoints();

            healthProgressBar.ProcessCompleted += Die;
            OnChangeState += UpdateAction;

            healthProgressBar.InitSlider(MaxHealth);
        }

        private void Start()
        {
            currentState = ZombieState.Idle;
        }

        private void OnDestroy()
        {
            
            healthProgressBar.ProcessCompleted -= Die;
            OnChangeState -= UpdateAction;
        }

        private void FixedUpdate()
        {
            RefreshCurrentState();
            timer -= Time.deltaTime;
            if (currentState == ZombieState.Attack && timer <= 0)
            {
                Attack();
                timer = Maxtimer;
            }
            if (IsCurrentStateUpdatableInEveryFrame == true)
            {
                UpdateAction(currentState);
            }
        }

        private void Walk()
        {
            agent.speed = speedForWalking;

            int pointToMove = UnityEngine.Random.Range(0, walkingPoints.Count);
            agent.SetDestination(walkingPoints[pointToMove].position);
        }

        private void Chase()
        {
            agent.speed = speedForChasing;
            agent.isStopped = false;

            agent.SetDestination(Root.Player.transform.position);
        }

        private void Attack()
        {
            agent.speed = 0;
            agent.isStopped = true;

            playerView.RecievedDamage(Damage);
        }

        public void RecievedDamage(float damage)
        {
            _health -= damage;
            healthProgressBar.StartProgress(_health);
        }

        private void Die()
        {
            interactiveArea.InteractiveObject = null;
            currentState = ZombieState.Die;
            healthProgressBar.gameObject.SetActive(false);
            OnDie(this);
        }

        private void RefreshCurrentState()
        {
            ZombieState lastZombieState = currentState;
            currentState = GetCurrentZombieState();

            if (currentState != lastZombieState)
            {
                OnChangeState(currentState);
            }
        }

        private void UpdateAction(ZombieState newState)
        {
            healthProgressBar.gameObject.SetActive(newState == ZombieState.Chase || newState == ZombieState.Attack);

            switch (newState)
            {
                case ZombieState.Attack:
                    Attack();
                    break;

                case ZombieState.Chase:
                    Chase();
                    break;

                case ZombieState.Walking:
                    Walk();
                    break;

                case ZombieState.Idle:
                case ZombieState.Die:
                default:
                    break;
            }
        }

        private ZombieState GetCurrentZombieState()
        {
            if (currentState == ZombieState.Die)
            {
                return ZombieState.Die;
            }

            float distanceToPlayer = Vector3.Distance(this.transform.position, Root.Player.transform.position);

            if (distanceToPlayer < distanceToPlayerForAttack)
            { 
                return ZombieState.Attack;
            }

            if (distanceToPlayer < distanceToPlayerForChase)
            { 
                return ZombieState.Chase;
            }

            if (agent.hasPath && agent.remainingDistance > 0.6f || UnityEngine.Random.Range(0, 1000) < walkingProbability)
            { 
                return ZombieState.Walking;
            } 

            return ZombieState.Idle;
        }

        private List<Transform> GetWalkingPoints()
        {
            WalkingPoint[] points = walkingPointsParent.GetComponentsInChildren<WalkingPoint>();
            List<Transform> transformPoints = new List<Transform>();

            foreach (WalkingPoint point in points)
            {
                transformPoints.Add(point.transform);    
            }

            return transformPoints;
        }
    }
}
