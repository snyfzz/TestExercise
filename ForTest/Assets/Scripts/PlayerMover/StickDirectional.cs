using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickDirectional : MonoBehaviour
{
    private Vector3 startPos;
    public Vector3 directionalVector;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        directionalVector = (transform.position - startPos).normalized;
    }
}
