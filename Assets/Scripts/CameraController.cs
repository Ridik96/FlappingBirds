using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    private Vector3 originalPosition;
    private Vector3 position;

    public BirdController followTarget;

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position,position, followTarget.BirdSpeed);
    }
    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        position = new Vector3(followTarget.transform.position.x, originalPosition.y, originalPosition.z);
    }
}
