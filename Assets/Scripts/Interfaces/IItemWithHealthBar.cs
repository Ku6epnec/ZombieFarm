using System;

public interface IItemWithHealthBar
{
    public float MaxHealthBarValue { get; }

    public event Action<float> OnRefreshProgress;

    public event Action OnResetProgress;

    public event Action<bool> OnRefreshProgressBarState;
}
