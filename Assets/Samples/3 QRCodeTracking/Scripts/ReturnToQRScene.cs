using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class ReturnToQRScene : MonoBehaviour
{
    void Update()
    {
        // Detect X button on Meta Quest controller (usually Button.X on Left Controller)
        bool xButtonPressed = false;

        // Oculus-specific (works with Meta Quest controllers)
        InputDevice leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        if (leftHand.TryGetFeatureValue(CommonUsages.primaryButton, out bool pressed))
        {
            xButtonPressed = pressed;
        }

        if (xButtonPressed)
        {
            SceneManager.LoadScene("QRCodeTracking");
        }
    }
}