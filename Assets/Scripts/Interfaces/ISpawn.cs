using UnityEngine;

namespace ZombieFarm.Interfaces
{
    public interface ISpawn
    {
        public Vector3 SpawnPoint { get; }
        public void OnSpawn();
    }
}
