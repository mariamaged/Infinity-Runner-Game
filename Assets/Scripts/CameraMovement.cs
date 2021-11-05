using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 1f;
    public float offset;

    void FixedUpdate()
    {
        Vector3 desiredPosition = new Vector3(transform.position.x, transform.position.y, target.position.z + offset);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
