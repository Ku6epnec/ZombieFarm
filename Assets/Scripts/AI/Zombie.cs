
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
        [SerializeField] private GameObject walkingPointsParent;
        [SerializeField][Range(0, 1000)] private int walkingProbability;

        private Transform player;
        private NavMeshAgent agent;
        private List<Transform> walkingPoints;

        private void Awake()
        {
            player = Root.Player;
            agent = GetComponent<NavMeshAgent>();
            walkingPoints = GetWalkingPoints();
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

        private void FixedUpdate()
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
            if (agent.remainingDistance > 0.6f)
            {
                return true;
            }

            if (Random.Range(0, 1000) < walkingProbability)
            {
                int pointToMove = Random.Range(0, walkingPoints.Count);
                agent.SetDestination(walkingPoints[pointToMove].position);
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
