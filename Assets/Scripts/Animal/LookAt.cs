using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] GameObject _object;

    public void InitObject(GameObject Object)
    {
        _object = Object;
    }
    public void DeInitObject()
    {
        _object = null;
    }

    private void Update()
    {
        if (_object != null)
        {
            transform.rotation = Quaternion.LookRotation(-transform.position + _object.transform.position);
        }
    }
}
