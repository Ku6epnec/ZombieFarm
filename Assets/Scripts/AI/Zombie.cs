
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ZombieFarm.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Zombie : MonoBehaviour
    {
        public event Action<ZombieState> OnChangeState = (newState) => { };
        public event Action<Zombie> OnDie = (Zombie) => { };

        [SerializeField] private float speedForWalking = 2f;
        [SerializeField] private float speedForChasing = 6f;
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

        private bool IsCurrentStateUpdatableInEveryFrame => currentState == ZombieState.Chase;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            walkingPoints = GetWalkingPoints();

            healthProgressBar.ProcessCompleted += Die;
            OnChangeState += UpdateAction;
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
            healthProgressBar.ResetProgress();
        }

        private void Attack()
        {
            agent.speed = 0;
            agent.isStopped = true;

            healthProgressBar.StartProgress();
        }

        private void Die()
        {
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
                default:
                    break;
            }
        }

        private ZombieState GetCurrentZombieState()
        {
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
