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
                if (particleSystem.duration > result)
                {
                    result = particleSystem.duration;
                }
            }

            return result;
        }
    }
}

