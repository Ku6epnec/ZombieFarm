using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ZombieFarm.Views.Player
{
    public class InteractiveArea : MonoBehaviour
    {
        public GameObject InteractiveObject;
        public ZombieFarm.AI.Zombie Enemy;
        private float timer;
        private float MaxTimer = 1;
        internal event Action Interactive = () => { };
        internal event Action DeInteractive = () => { };
        public LookAt lookAt;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Zombie")
            {
                InteractiveObject = other.gameObject;
                if (InteractiveObject.TryGetComponent<ZombieFarm.AI.Zombie>(out ZombieFarm.AI.Zombie zombie))
                {
                    Enemy = zombie;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Zombie"))
            {
                InteractiveObject = null;
            }
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            if (InteractiveObject != null && timer <= 0)
            {
                timer = MaxTimer;
                Interactive();
                lookAt.InitObject(InteractiveObject);
            }
            else if (InteractiveObject == null)
            {
                lookAt.DeInitObject();
                DeInteractive();
            }
        }
    }
}
