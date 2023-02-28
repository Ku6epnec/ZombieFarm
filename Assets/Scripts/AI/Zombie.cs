using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ZombieFarm.Views;

namespace ZombieFarm.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Zombie : MonoBehaviour
    { 
        [SerializeField] private Transform player;
        [SerializeField] private float distanceToPlayerForAttack;
        [SerializeField] private float distanceToPlayerForChase;
        [SerializeField] private GameObject walkingPointsParent;
        [SerializeField][Range(0, 100)] private float walkingProbability;

        private NavMeshAgent agent;
        private Transform[] walkingPoints;
        private const float accuracy = 0.001f;
        private Vector3? walkingTarget;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            walkingPoints = walkingPointsParent.GetComponentsInChildren<Transform>();
        }

        private void Update()
        {
            if (TryChase() == true)
            {
                TryAttack();
                return;
            }

            TryWalk();
        }

        private bool TryWalk()
        {
            //dirty
            if (walkingTarget != null)
            {
                if (Vector3.Distance(transform.position, walkingTarget.Value) > accuracy)
                {
                    return true;
                }
                else
                {
                    walkingTarget = null;
                    return false;
                }
            }

            if (Random.Range(0, 100) < walkingProbability)
            {
                int pointToMove = Random.Range(0, walkingPoints.Length);
                walkingTarget = walkingPoints[pointToMove].position;
                agent.SetDestination(walkingTarget.Value);
                return true;
            }

            return false;
        }

        private bool TryChase()
        {
            if (Vector3.Distance(this.transform.position, player.transform.position) < distanceToPlayerForChase)
            {
                agent.SetDestination(player.transform.position);
                return true;
            }

            return false;
        }

        private bool TryAttack()
        {
            if (Vector3.Distance(this.transform.position, player.transform.position) < distanceToPlayerForAttack)
            {
                //TODO: attack actions
                Debug.LogError("Attack!");
                return true;
            }

            return false;
        }
    }
}
