using UnityEngine;
using System;

namespace ZombieFarm.Views.Player
{
    public class InteractiveArea : MonoBehaviour
    {
        [SerializeField] private LookAt lookAt;

        private ReceivedDamageObject receivedDamageObject;

        internal event Action OnInteractive = () => { };
        internal event Action OnDeInteractive = () => { };

        private GameObject interactiveObject;

        private float timer;
        private float maxTimer = 1;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<ReceivedDamageObject>(out ReceivedDamageObject _receivedDamageObject))
            {
                interactiveObject = other.gameObject;
                receivedDamageObject = _receivedDamageObject;
                receivedDamageObject.CleanInteractiveObject += Clean;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (interactiveObject == null)
            {
                if (other.gameObject.TryGetComponent<ReceivedDamageObject>(out ReceivedDamageObject _receivedDamageObject))
                {
                    interactiveObject = other.gameObject;
                    receivedDamageObject = _receivedDamageObject;
                    receivedDamageObject.CleanInteractiveObject += Clean;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == interactiveObject)
            {
                Clean();
            }
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            if (interactiveObject != null && timer <= 0)
            {           
                timer = maxTimer;
                OnInteractive();
                lookAt.InitObject(interactiveObject);
            }
            else if (interactiveObject == null && timer <= 0)
            {
                lookAt.DeInitObject();
                OnDeInteractive();              
            }
        }

        internal void Clean()
        {
            interactiveObject = null;
            if (receivedDamageObject != null)
            {
                receivedDamageObject.CleanInteractiveObject -= Clean;
            }
        }
    }
}
