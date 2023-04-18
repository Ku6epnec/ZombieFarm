using UnityEngine;

namespace ZombieFarm.Views
{ 
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Vector3 cameraOffset;
        [SerializeField] private Transform target;
        [SerializeField][Range(0, 1)] private float ivisibleForObstacles;

        private void LateUpdate()
        { 
            transform.position = target.position + cameraOffset;
        }
        private void OnTriggerEnter(Collider collider)
        {
            GameObject obstacle = collider.gameObject;
            MeshRenderer mesh = obstacle.GetComponent<MeshRenderer>();

            foreach (Material material in mesh.materials)
            {
                Color _color = material.color;
                _color.a = ivisibleForObstacles;
                material.color = _color;
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            GameObject obstacle = collider.gameObject;
            MeshRenderer mesh = obstacle.GetComponent<MeshRenderer>();
            foreach (Material material in mesh.materials)
            {
                Color _color = material.color;
                _color.a = 1.0f;
                material.color = _color;
            };
        }
    }
}
