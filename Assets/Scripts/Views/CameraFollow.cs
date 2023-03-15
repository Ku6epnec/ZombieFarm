
using UnityEngine;

namespace ZombieFarm.Views
{ 
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Vector3 cameraOffset;
        [SerializeField] private Transform target;

        private void LateUpdate()
        { 
            transform.position = target.position + cameraOffset;
        }
        private void OnTriggerEnter(Collider collider)
        {
            GameObject obstacle = collider.gameObject;
            MeshRenderer mesh = obstacle.GetComponent<MeshRenderer>();
            Color _color = mesh.material.color;
            _color.a = 0.1f;
            mesh.material.color = _color;
            Debug.Log("Обзор загораживает: " + obstacle);
        }

        private void OnTriggerExit(Collider collider)
        {
            GameObject obstacle = collider.gameObject;
            MeshRenderer mesh = obstacle.GetComponent<MeshRenderer>();
            Color _color = mesh.material.color;
            _color.a = 1.0f;
            mesh.material.color = _color;
            Debug.Log("Обзор перестало загораживать: " + obstacle);
        }
    }
}
