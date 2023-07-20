using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AnimalFollow : MonoBehaviour
{
    public event Action OnStartFollowing = () => { }; 

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float stopFollowingDistance;
    [SerializeField] private GameObject vfx;

    private NavMeshAgent agent;
    private bool isFollowing = false;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (isFollowing == true)
        {
            Vector3 playerPosition = Root.Player.transform.position;
            Vector3 distanceBetween = playerPosition - transform.position;
            if (distanceBetween.sqrMagnitude < stopFollowingDistance * stopFollowingDistance)
            {
                agent.isStopped = false;
                agent.SetDestination(playerPosition);
            }
            else
            {
                agent.isStopped = true;
            }
        }
    }

    public void StartFollowing()
    {
        isFollowing = true;

        agent.speed = moveSpeed;

        vfx.SetActive(true);
        
        OnStartFollowing();
    }
}
