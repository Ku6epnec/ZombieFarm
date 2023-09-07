using System;
using UnityEngine;
using UnityEngine.AI;
using UnityTools.Runtime.StatefulEvent;
using ZombieFarm.LocationTransition;

namespace ZombieFarm.AI
{
    public class AnimalFollow : MonoBehaviour
    {
        public IStatefulEvent<bool> IsMoving => isMoving;

        [SerializeField] private float nearDistanceBetweenForStop = 7f;
        [SerializeField] private float farDistanceBetweenForStop = 20f;
        [SerializeField] private GameObject vfx;

        private readonly StatefulEventInt<bool> isMoving = StatefulEventInt.Create(false);

        private NavMeshAgent agent;
        private bool isFollowing = false;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();

            isMoving.OnValueChanged += Move;
            Move(false);

            Root.TransitionManager.OnSetNewTransitionIndex += ReachSafeZone;
        }

        private void OnDestroy()
        {
            Root.TransitionManager.OnSetNewTransitionIndex -= ReachSafeZone;
        }

        private void Update()
        {
            if (isFollowing == false)
            {
                return;
            }

            float distanceToPlayer = Vector3.Distance(this.transform.position, Root.Player.transform.position);
            bool canMove = distanceToPlayer > nearDistanceBetweenForStop && distanceToPlayer < farDistanceBetweenForStop;

            isMoving.Set(canMove);

            if (agent.isStopped == false)
            {
                agent.SetDestination(Root.Player.transform.position);
            }
        }

        internal void StartFollowing() => isFollowing = true;

        private void Move(bool active)
        {
            agent.isStopped = active == false;

            vfx.SetActive(active);
        }

        private void ReachSafeZone()
        {
            isFollowing = false;

            //TODO: Add animal to farm location due spawn manager
        }
    }
}

