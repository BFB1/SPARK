using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Controls camera movement

    public Transform target;
    public Vector3 offset;
    public float smoothSpeed;
    


	
    private void FixedUpdate ()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        transform.position = smoothedPosition;
        transform.LookAt(target);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
    }
}