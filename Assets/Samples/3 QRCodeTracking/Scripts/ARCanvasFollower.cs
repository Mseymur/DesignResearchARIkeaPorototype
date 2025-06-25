using UnityEngine;

public class ARCanvasFollower : MonoBehaviour
{
    [Tooltip("Assign your main XR camera (usually CenterEyeAnchor) here.")]
    public Transform cameraTransform;

    [Tooltip("How far in front of the user the UI should float.")]
    public float distance = 2.0f;

    [Tooltip("The vertical offset from the center of view. 0 = centered, negative = lower, positive = higher.")]
    public float verticalOffset = 0f;

    [Tooltip("How quickly the UI follows the user's head. Higher value = more rigid.")]
    public float followSpeed = 8.0f;

    // We use LateUpdate to ensure the camera has finished its movement for the frame.
    // This results in smoother, jitter-free following.
    void LateUpdate()
    {
        // Do nothing if the camera isn't set
        if (cameraTransform == null)
        {
            return;
        }

        // 1. Calculate the target position
        // Start with the camera's position, move forward by the set distance, and then apply the vertical offset.
        Vector3 targetPosition = cameraTransform.position + (cameraTransform.forward * distance);
        targetPosition.y += verticalOffset;

        // 2. Smoothly move this canvas towards the target position
        // Vector3.Lerp creates a smooth, dampened movement instead of an instant teleport.
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);

        // 3. Calculate the target rotation to make the canvas face the user
        Quaternion targetRotation = Quaternion.LookRotation(transform.position - cameraTransform.position);
        
        // 4. Smoothly rotate this canvas to face the user
        // Quaternion.Slerp does the same smoothing for rotation.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * followSpeed);
    }
}