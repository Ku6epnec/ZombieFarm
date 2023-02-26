using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class AnimalFollow : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform target;

    private CharacterController controller;
    [SerializeField] private bool isFollowing = false;


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
            controller.Move(velocity * Time.deltaTime);
        }
    }

    public void StartFollowing(GameObject targetObject = null)
    {
        isFollowing = true;

        if (targetObject != null) target = targetObject.transform;
    }
}
