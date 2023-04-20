using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] GameObject _object;

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(-transform.position + _object.transform.position);
    }
}
