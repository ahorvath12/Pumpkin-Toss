using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 10, smoothTime = 0.1f;
    public Vector3 offset;
    public Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        //transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

        transform.LookAt(target);
    }
}
