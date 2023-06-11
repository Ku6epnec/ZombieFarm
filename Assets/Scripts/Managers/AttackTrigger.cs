using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ZombieFarm.Views.Player
{
    public class AttackTrigger : MonoBehaviour
    {
        public GameObject InteractiveObject;
        internal event Action ApplyDamage = () => { };
        private PlayerView playerView;
        public ZombieFarm.AI.Zombie Enemy;
        public Cage cage;
        private void Awake()
        {
            playerView = FindObjectOfType<PlayerView>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Zombie")
            {
                InteractiveObject = other.gameObject;
                if (InteractiveObject.TryGetComponent<ZombieFarm.AI.Zombie>(out ZombieFarm.AI.Zombie zombie))
                {
                    Enemy = zombie;
                }
                playerView.enemy = Enemy;
                ApplyDamage();
            }
            else if(other.tag == "Interactible")
            {
                InteractiveObject = other.gameObject;
                if (InteractiveObject.TryGetComponent<Cage>(out Cage _cage))
                {
                    cage = _cage;
                }
                playerView.cage = cage;
                ApplyDamage();
            }
        }
    }
}
