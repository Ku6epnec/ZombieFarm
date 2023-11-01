using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IData
{
    string ObjectName { get; }
    Transform ObjectTransform { get; }
}
