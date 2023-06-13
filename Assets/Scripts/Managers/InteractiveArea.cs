using UnityEngine;
using System;

namespace ZombieFarm.Views.Player
{
    public class InteractiveArea : MonoBehaviour
    {
        public GameObject InteractiveObject;
        private float timer;
        private float maxTimer = 1;
        internal event Action Interactive = () => { };
        internal event Action DeInteractive = () => { };
        public LookAt lookAt;
        private ReceivedDamageObject ReceivedDamageObject;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<ReceivedDamageObject>(out ReceivedDamageObject receivedDamageObject))
            {
                InteractiveObject = other.gameObject;
                ReceivedDamageObject = receivedDamageObject;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.TryGetComponent<ReceivedDamageObject>(out ReceivedDamageObject receivedDamageObject))
            {
                InteractiveObject = other.gameObject;
                ReceivedDamageObject = receivedDamageObject;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == InteractiveObject)
            {
                InteractiveObject = null;
            }
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            if (InteractiveObject != null && timer <= 0)
            {
                timer = maxTimer;
                Interactive();
                lookAt.InitObject(InteractiveObject);
            }
            else if (InteractiveObject == null && timer <= 0)
            {
                lookAt.DeInitObject();
                DeInteractive();
            }
        }
    }
}
