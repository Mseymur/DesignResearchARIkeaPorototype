using UnityEngine;
using System.Collections; // Required for using Coroutines

public class ARUIFollower : MonoBehaviour
{
    [Tooltip("How far in front of the user the UI should be placed.")]
    public float distance = 2.0f;

    [Tooltip("How far below the user's direct line of sight the UI should be placed.")]
    public float verticalOffset = -0.3f;

    [Tooltip("Delay in seconds to wait for the XR rig to initialize before positioning the UI.")]
    public float initializationDelay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine to position the UI after a delay
        StartCoroutine(PositionCanvasAfterDelay());
    }

    private IEnumerator PositionCanvasAfterDelay()
    {
        // Wait for a short moment to ensure the camera is initialized
        yield return new WaitForSeconds(initializationDelay);

        if (Camera.main != null)
        {
            Transform cam = Camera.main.transform;

            // Calculate the target position
            Vector3 targetPosition = cam.position + (cam.forward * distance);
            targetPosition.y += verticalOffset;

            transform.position = targetPosition;

            // Make the canvas look back at the camera
            transform.LookAt(new Vector3(cam.position.x, transform.position.y, cam.position.z));
            transform.forward *= -1;
        }
        else
        {
            Debug.LogError("ARUIFollower: Camera.main is null! Cannot position canvas.");
        }
    }
}