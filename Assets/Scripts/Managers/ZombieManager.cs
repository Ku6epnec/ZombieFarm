using System;
using System.Collections.Generic;
using UnityEngine;
using ZombieFarm.AI;
using ZombieFarm.Managers.Interfaces;

namespace ZombieFarm.Managers
{
    public class ZombieManager : MonoBehaviour, IEnemyManager
    {
        public event Action OnMonsterAttack = () => { };

        private List<Zombie> zombies;

        private void Awake()
        {
            Zombie[] _zombies = GetComponentsInChildren<Zombie>();

            foreach (Zombie zombie in _zombies)
            {
                zombies.Add(zombie);
                zombie.OnChangeState += OnChangeZombieState;
                zombie.OnDie += OnZombieDie;
            }
        }

        private void OnDestroy()
        {
            foreach (Zombie zombie in zombies)
            {
                zombie.OnChangeState -= OnChangeZombieState;
                zombie.OnDie -= OnZombieDie;
            }
        }

        private void OnZombieDie(Zombie zombie)
        {
            zombies.Remove(zombie);

            zombie.OnChangeState -= OnChangeZombieState;
            zombie.OnDie -= OnZombieDie;
        }

        private void OnChangeZombieState(ZombieState zombieState)
        {
            if (zombieState == ZombieState.Attack)
            {
                OnMonsterAttack();
            }
        }
    }
}
