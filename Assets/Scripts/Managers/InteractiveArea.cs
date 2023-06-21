using UnityEngine;
using System;

namespace ZombieFarm.Views.Player
{
    public class InteractiveArea : MonoBehaviour
    {
        private GameObject InteractiveObject;
        private float timer;
        private float maxTimer = 1;
        internal event Action Interactive = () => { };
        internal event Action DeInteractive = () => { };
        private LookAt lookAt;
        private ReceivedDamageObject ReceivedDamageObject;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<ReceivedDamageObject>(out ReceivedDamageObject receivedDamageObject))
            {
                InteractiveObject = other.gameObject;
                ReceivedDamageObject = receivedDamageObject;
                ReceivedDamageObject.CleanInteractiveObject += Cleaner;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (InteractiveObject == null)
            {
                if (other.gameObject.TryGetComponent<ReceivedDamageObject>(out ReceivedDamageObject receivedDamageObject))
                {
                    InteractiveObject = other.gameObject;
                    ReceivedDamageObject = receivedDamageObject;
                    ReceivedDamageObject.CleanInteractiveObject += Cleaner;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == InteractiveObject)
            {
                Cleaner();
                ReceivedDamageObject.CleanInteractiveObject -= Cleaner;
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
                
                ReceivedDamageObject.CleanInteractiveObject -= Cleaner;
                lookAt.DeInitObject();
            }
        }

        private void Cleaner()
        {
            InteractiveObject = null;
        }
    }
}
