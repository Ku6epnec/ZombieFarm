using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRemovableObject
{
    event Action<bool> OnDestroyProcess;
}
