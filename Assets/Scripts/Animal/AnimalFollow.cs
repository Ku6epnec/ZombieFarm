using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class AnimalFollow : MonoBehaviour
{
    public event Action OnStartFollowing = () => { }; 

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject vfx;

    private CharacterController controller;
    private float distanceBetween = 5f;
    private bool isFollowing = false;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (isFollowing)
        {
            Vector3 direction = target.position - transform.position;
            direction = direction.normalized;
            Vector3 velocity = direction * moveSpeed;
            if (Vector3.Distance(target.position, transform.position) > distanceBetween)
                controller.Move(velocity * Time.deltaTime);
        }
    }

    public void StartFollowing(GameObject targetObject = null)
    {
        isFollowing = true;

        if (targetObject != null)
        { 
            target = targetObject.transform;
        } 

        vfx.SetActive(true);
        OnStartFollowing();
    }
}
