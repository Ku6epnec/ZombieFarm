using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieFarm.Views
{
    public class ZombieVfx : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> particles;

        public float GetMaxParticleDuration()
        {
            float result = 0;
            
            foreach (ParticleSystem particleSystem in particles)
            {
                if (particleSystem.main.duration > result)
                {
                    result = particleSystem.main.duration;
                }
            }

            return result;
        }
    }
}

