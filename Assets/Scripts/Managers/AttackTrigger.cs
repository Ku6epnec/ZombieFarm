using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ZombieFarm.Views.Player
{
    public class AttackTrigger : MonoBehaviour
    {
        private GameObject InteractiveObject;
        private PlayerView playerView;
        private ZombieFarm.AI.Zombie Enemy;
        private Cage cage;

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
        private void ApplyDamage()
        {
            if (Enemy != null)
            {
                Enemy.ReceivedDamage(playerView.Damage);
            }
            else if (cage != null)
            {
                cage.ReceivedDamage(playerView.Damage);
            }
        }
    }
}
