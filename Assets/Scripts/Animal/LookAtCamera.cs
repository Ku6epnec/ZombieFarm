using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    GameObject _object;

    private void Start()
    {
        _object = Camera.main.gameObject;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(-transform.position + _object.transform.position);
    }
}
