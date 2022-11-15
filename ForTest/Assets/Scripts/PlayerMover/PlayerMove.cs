using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private GameObject handle;
    [SerializeField] private float playerSpeed = 1f;

    private StickDirectional stickDirectional;

    private void Start()
    {
        stickDirectional = handle.GetComponent<StickDirectional>();
    }

    private void Update()
    {
        transform.Translate(stickDirectional.directionalVector * playerSpeed * Time.deltaTime);
    }
}
