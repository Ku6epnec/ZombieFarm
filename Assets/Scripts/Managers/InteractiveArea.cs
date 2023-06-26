using UnityEngine;
using System;

namespace ZombieFarm.Views.Player
{
    public class InteractiveArea : MonoBehaviour
    {
        private ReceivedDamageObject receivedDamageObject;
        private LookAt lookAt;

        internal event Action OnInteractive = () => { };
        internal event Action OnDeInteractive = () => { };

        private GameObject InteractiveObject;

        private float timer;
        private float maxTimer = 1;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<ReceivedDamageObject>(out ReceivedDamageObject _receivedDamageObject))
            {
                InteractiveObject = other.gameObject;
                receivedDamageObject = _receivedDamageObject;
                receivedDamageObject.CleanInteractiveObject += Clean;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (InteractiveObject == null)
            {
                if (other.gameObject.TryGetComponent<ReceivedDamageObject>(out ReceivedDamageObject _receivedDamageObject))
                {
                    InteractiveObject = other.gameObject;
                    receivedDamageObject = _receivedDamageObject;
                    receivedDamageObject.CleanInteractiveObject += Clean;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == InteractiveObject)
            {
                Clean();
                receivedDamageObject.CleanInteractiveObject -= Clean;
            }
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            if (InteractiveObject != null && timer <= 0)
            {           
                timer = maxTimer;
                OnInteractive();
                lookAt.InitObject(InteractiveObject);
            }
            else if (InteractiveObject == null && timer <= 0)
            {           
                OnDeInteractive();              
                receivedDamageObject.CleanInteractiveObject -= Clean;
                lookAt.DeInitObject();
            }
        }

        private void Clean()
        {
            InteractiveObject = null;
        }
    }
}
