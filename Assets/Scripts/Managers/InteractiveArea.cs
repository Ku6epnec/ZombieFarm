using UnityEngine;
using System;

namespace ZombieFarm.Views.Player
{
    public class InteractiveArea : MonoBehaviour
    {
        private ReceivedDamageObject receivedDamageObject;
        private LookAt lookAt;

        internal event Action Interactive = () => { };
        internal event Action DeInteractive = () => { };

        private GameObject InteractiveObject;

        private float timer;
        private float maxTimer = 1;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<ReceivedDamageObject>(out ReceivedDamageObject _receivedDamageObject))
            {
                InteractiveObject = other.gameObject;
                receivedDamageObject = _receivedDamageObject;
                receivedDamageObject.CleanInteractiveObject += Cleaner;
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
                    receivedDamageObject.CleanInteractiveObject += Cleaner;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == InteractiveObject)
            {
                Cleaner();
                receivedDamageObject.CleanInteractiveObject -= Cleaner;
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
                DeInteractive();              
                receivedDamageObject.CleanInteractiveObject -= Cleaner;
                lookAt.DeInitObject();
            }
        }

        private void Cleaner()
        {
            InteractiveObject = null;
        }
    }
}
