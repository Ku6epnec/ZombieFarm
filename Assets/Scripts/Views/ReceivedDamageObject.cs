using UnityEngine;
using System;

public class ReceivedDamageObject: MonoBehaviour
{
    internal virtual event Action CleanInteractiveObject = () => {};

    public virtual void Interaction(float interactiveValue)
    {

    }
}
