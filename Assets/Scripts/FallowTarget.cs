using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallowTarget : MonoBehaviour
{
    public Transform target;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void Update()
    {
        transform.position = target.position + offset;
    }
}
