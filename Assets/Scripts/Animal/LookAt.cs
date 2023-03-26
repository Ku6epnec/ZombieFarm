using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [Tooltip("Main camera by default")]
    [SerializeField] GameObject _object;

    private void Start()
    {
        if (_object == null)
            _object = Camera.main.gameObject;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(-transform.position + _object.transform.position);
    }
}
