using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private BirdController followTarget;
    private Vector3 originalPosition;
    private Vector3 position;

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position,position, followTarget.BirdSpeed);
    }
    void Start()
    {
        followTarget = FindObjectOfType<BirdController>();
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        position = new Vector3(followTarget.transform.position.x, originalPosition.y, originalPosition.z);
    }
}
