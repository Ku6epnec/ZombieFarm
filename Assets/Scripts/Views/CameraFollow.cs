
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
    }
}
