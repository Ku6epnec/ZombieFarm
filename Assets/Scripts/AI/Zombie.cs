using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ZombieFarm.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Zombie : ReceivedDamageObject, IHealth, IDamage
    {
        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public float Damage => _damage;

        public event Action<ZombieState> OnChangeState = (newState) => { };
        public event Action<Zombie> OnDie = (Zombie) => { };

        internal override event Action CleanInteractiveObject = () => { };

        private NavMeshAgent agent;
        private List<Transform> walkingPoints;
        private ZombieState currentState;

        [SerializeField] private CharacterController characterController;
        [SerializeField] private ZombieFarm.Views.Player.PlayerView playerView;

        [Header("References")]
        [SerializeField] private ProgressBar healthProgressBar;

        [Header("HealthStats")]
        [SerializeField] private float _health = 10;
        [SerializeField] private float _maxHealth = 10;

        [Header("DamageStats")]
        [SerializeField] private float _damage = 1;


        [Header("SpeedStats")]
        [SerializeField] private float speedForWalking = 2f;
        [SerializeField] private float speedForChasing = 6f;

        [Header("DistanceStats")]
        [SerializeField] private float attackDistance = 2f;
        [SerializeField] private float chaseDistance = 15f;

        [Header("Walking")]
        [SerializeField] private GameObject walkingPointsParent;
        [SerializeField][Range(0, 1000)] private int walkingProbability;

        private float timer;
        private float maxTimer = 2.0f;
        private float recievedDamageTimer;

        private bool IsCurrentStateUpdatableInEveryFrame => currentState == ZombieState.Chase;

        public override void Interaction(float damage)
        {
            ReceivedDamage(damage);
        }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            walkingPoints = GetWalkingPoints();

            healthProgressBar.ProcessCompleted += Die;
            OnChangeState += UpdateAction;

            healthProgressBar.InitSlider(MaxHealth);
        }

        private void Start()
        {
            currentState = ZombieState.Idle;
            playerView = Root.ViewManager.GetPlayerView();
        }

        private void OnDestroy()
        {
            healthProgressBar.ProcessCompleted -= Die;
            OnChangeState -= UpdateAction;
            CleanInteractiveObject();
        }

        private void FixedUpdate()
        {
            RefreshCurrentState();
            timer -= Time.deltaTime;
            recievedDamageTimer -= Time.deltaTime;
            if (currentState == ZombieState.Attack && timer <= 0)
            {
                Attack();
                timer = maxTimer;
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

            playerView.ReceivedDamage(Damage);
        }

        public void ReceivedDamage(float damage)
        {
            if (recievedDamageTimer <= 0)
            {
                _health -= damage;
                healthProgressBar.RefreshProgress(_health);
                recievedDamageTimer = 2;
            }
        }

        private void Die()
        {
            Destroy(characterController);
            CleanInteractiveObject();
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
                    if (timer <= 0)
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

            if (distanceToPlayer < attackDistance)
            { 
                return ZombieState.Attack;
            }

            if (distanceToPlayer < chaseDistance)
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
