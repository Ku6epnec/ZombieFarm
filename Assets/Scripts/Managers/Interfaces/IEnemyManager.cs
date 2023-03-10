using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieFarm.Managers.Interfaces
{
    public interface IEnemyManager
    {
        event Action OnMonsterAttack;
    }
}
