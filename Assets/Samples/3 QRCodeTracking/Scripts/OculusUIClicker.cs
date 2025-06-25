using UnityEngine;
using UnityEngine.EventSystems; // Required for UI event data
using UnityEngine.UI; // Required for the Button component
 // Required for the XRRayInteractor

public class OculusUIClicker : MonoBehaviour
{
    [Tooltip("Assign the XR Ray Interactor from this same GameObject here.")]
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor rayInteractor;

    [Tooltip("The specific Oculus controller button to listen for.")]
    public OVRInput.Button buttonToListenFor = OVRInput.Button.PrimaryIndexTrigger;

    [Tooltip("The specific Oculus controller.")]
    public OVRInput.Controller controller = OVRInput.Controller.RTouch;

    // We must check the OVRInput state in Update()
    void Update()
    {
        // Check if the trigger was just pressed down this frame
        if (OVRInput.GetDown(buttonToListenFor, controller))
        {
            // Try to find what the ray is currently pointing at in the UI
            if (rayInteractor != null && rayInteractor.TryGetCurrentUIRaycastResult(out RaycastResult result))
            {
                // We hit something! Check if the thing we hit has a Button component.
                if (result.gameObject.TryGetComponent<Button>(out Button button))
                {
                    // It does! Manually invoke the button's OnClick event.
                    button.onClick.Invoke();
                    Debug.Log($"Clicked on UI Button: {button.name}");
                }
            }
        }
    }
}