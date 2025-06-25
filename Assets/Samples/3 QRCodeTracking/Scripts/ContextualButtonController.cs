using UnityEngine;

public class ContextualButtonController : MonoBehaviour
{
    [Tooltip("Assign your UIManager object here.")]
    public UIManager uiManager;

    // We check for input every frame
    void Update()
    {
        // Check if the 'B' button on the right controller was just pressed
        // OVRInput.Button.Two is the 'B' button.
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            Debug.Log("'B' Button Pressed! Checking current UI state...");

            // This is our State Machine. Check which panel is active and decide what to do next.

            // IF we are on the Main Menu... (we'll check if the Wish List is active)
            if (uiManager.wishListPanel.activeInHierarchy)
            {
                // This assumes the user "clicks" the first sofa.
                uiManager.ShowSofa1Details();
            }
            // ELSE IF we are on a Sofa Details page...
            else if (uiManager.sofa1Panel.activeInHierarchy || uiManager.sofa2Panel.activeInHierarchy)
            {
                // ...the next step is to go to the checkout.
                uiManager.ShowCheckout();
            }
            // ELSE IF we are on the Checkout page...
            else if (uiManager.checkOutPanel.activeInHierarchy)
            {
                // ...the next step is to pay.
                uiManager.ShowPayment();
            }
            // ELSE IF we are on the Payment page...
            else if (uiManager.paymentPanel.activeInHierarchy)
            {
                // ...the next step is to show the "Paid" confirmation.
                uiManager.ShowPaidConfirmation();
            }
            // Note: The "Paid" confirmation automatically moves to the next step via a timer.
            // You could add more states here if needed.
        }
    }
}