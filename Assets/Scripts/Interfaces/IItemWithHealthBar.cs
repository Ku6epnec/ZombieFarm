using System;

public interface IItemWithHealthBar
{
    public event Action<float> OnRefreshProgress;
}
