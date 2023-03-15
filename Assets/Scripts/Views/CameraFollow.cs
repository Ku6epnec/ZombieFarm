
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
            
            RaycastHit[] allHits;
            allHits = Physics.RaycastAll(transform.position, target.position, 100);
            foreach (var hit in allHits)
            {
                if (!hit.transform.tag.Equals("Player"))
                {
                    Debug.Log("Мы пульнули лазером в объект: " + hit);
                }
            }
            Debug.DrawRay(transform.position, -target.position, Color.black, 100);
        }
        private void OnTriggerEnter(Collider collider)
        {
            GameObject obstacle = collider.gameObject;
            MeshRenderer mesh = obstacle.GetComponent<MeshRenderer>();

            foreach (Material material in mesh.materials)
            {
                Color _color = material.color;
                _color.a = 0.2f;
                material.color = _color;
            }

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
