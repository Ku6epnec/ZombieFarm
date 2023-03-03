
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ZombieFarm.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Zombie : MonoBehaviour
    { 
        [SerializeField] private float distanceToPlayerForAttack;
        [SerializeField] private float distanceToPlayerForChase;
        [SerializeField] private ProgressBar healthProgressBar;
        [SerializeField] private Transform player;

        [Header("Walking")]
        [SerializeField] private GameObject walkingPointsParent;
        [SerializeField][Range(0, 1000)] private int walkingProbability;

        private NavMeshAgent agent;
        private List<Transform> walkingPoints;
        private ZombieState currentState;

        private event Action ChangeState = () => { };

        private bool IsCurrentStateUpdatableInEveryFrame => currentState == ZombieState.Chase;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            walkingPoints = GetWalkingPoints();
            currentState = GetCurrentZombieState();

            healthProgressBar.ProcessCompleted += Die;
            ChangeState += UpdateAction;
        }

        private void OnDestroy()
        {
            healthProgressBar.ProcessCompleted -= Die;
            ChangeState -= UpdateAction;
        }

        private void FixedUpdate()
        {
            RefreshCurrentState();

            if (IsCurrentStateUpdatableInEveryFrame == true)
            {
                UpdateAction();
            }
        }

        private void Walk()
        {
            int pointToMove = UnityEngine.Random.Range(0, walkingPoints.Count);
            agent.SetDestination(walkingPoints[pointToMove].position);
        }

        private void Chase()
        {
            agent.SetDestination(player.transform.position);
        }

        private void Attack()
        {
            healthProgressBar.StartProgress();
        }

        private void Die()
        {
            Destroy(this.gameObject);
        }

        private void RefreshCurrentState()
        {
            ZombieState lastZombieState = currentState;
            currentState = GetCurrentZombieState();

            if (currentState != lastZombieState)
            {
                ChangeState();
            }
        }

        private void UpdateAction()
        {
            healthProgressBar.gameObject.SetActive(currentState == ZombieState.Chase || currentState == ZombieState.Attack);

            switch (currentState)
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
            float distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);

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
