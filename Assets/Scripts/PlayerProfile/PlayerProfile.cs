using System;
using UnityTools.Runtime.StatefulEvent;
using ZombieFarm.Interfaces;

public class PlayerProfile : IPlayerProfile
{
    public event Action OnDie = () => {};
    public IStatefulEvent<float> CurrentHealth => currentHealth;

    private PlayerConfig config;

    private StatefulEventInt<float> currentHealth = StatefulEventInt.Create(1f);

    internal void Init()
    {
        config = Root.ConfigManager.GameSettings.Player;
        currentHealth.Set(config.maxHealth);
    }

    public void RegisterDamage(float damageValue)
    {
        damageValue = damageValue - config.armor;

        if (damageValue > 0)
        {
            float newHealth = currentHealth.Value - damageValue;

            currentHealth.Set(newHealth);

            if (newHealth < 0)
            {
                OnDie();
            }
        }
    }
}
