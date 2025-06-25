using UnityEngine;
using System.Collections; // Needed for Coroutines like timers

public class UIManager : MonoBehaviour
{
    // === ASSIGN IN INSPECTOR ===
    // Drag all your panel GameObjects from the Hierarchy into these slots.

    [Header("Main Menu Panels")]
    public GameObject ikeaSearchPanel;
    public GameObject welcomePanel;
    public GameObject wishListPanel;
    public GameObject recommendedPanel;
    public GameObject tabBar;
    public GameObject musicBar;
    // public GameObject Button_Love;

    [Header("Item Detail Panels")]
    public GameObject sofa1Panel;
    public GameObject NavigationPanel;
    public GameObject NavigationPanel2;// The panel for sofa1
    public GameObject sofa2Panel;

    [Header("Checkout Flow Panels")]
    public GameObject checkOutPanel;
    public GameObject paymentPanel; // The Apple Pay screen
    public GameObject paidPanel;    // The "Done" screen
    public GameObject pickupDetailsPanel;

    [Header("Post-Purchase Panels")]
    public GameObject wishList2Panel;
    public GameObject recommended2Panel;

    // This runs when the scene first loads
    void Start()
    {
        // Check if the DataManager has told us to show a specific item
        if (DataManager.Instance != null && !string.IsNullOrEmpty(DataManager.Instance.itemToShowOnLoad))
        {
            string item = DataManager.Instance.itemToShowOnLoad;
            // IMPORTANT: Clear the instruction so it doesn't run again next time
            DataManager.Instance.itemToShowOnLoad = null;

            if (item == "sofa2")
            {
                ShowSofa2Details();
            }
            // Add more "else if" conditions here for other QR codes
        }
        else
        {
            // If no specific item is requested, just show the default main menu
            ShowMainMenu();
        }
    }

    // --- Public Functions for your Buttons to Call ---

    public void ShowMainMenu()
    {
        HideAllPanels(); // Helper function to start clean
        ikeaSearchPanel.SetActive(true);
        wishListPanel.SetActive(true);
        recommendedPanel.SetActive(true);
        tabBar.SetActive(true);
        musicBar.SetActive(true);
        //Button_Love.SetActive(true);
    }

    public void ShowSofa1Details()
    {
        HideAllPanels();
        // Keep some base elements visible
        ikeaSearchPanel.SetActive(true);
        NavigationPanel.SetActive(true);
        NavigationPanel2.SetActive(true);
        tabBar.SetActive(true);
        musicBar.SetActive(true);
        // Show the specific item
        sofa1Panel.SetActive(true);
    }

    public void ShowSofa2Details()
    {
        HideAllPanels();
        ikeaSearchPanel.SetActive(true);
        NavigationPanel.SetActive(true);
        NavigationPanel2.SetActive(true);
        tabBar.SetActive(true);
        musicBar.SetActive(true);
        sofa2Panel.SetActive(true);
    }

    public void ShowCheckout()
    {
        HideAllPanels();
        ikeaSearchPanel.SetActive(true);
        NavigationPanel.SetActive(true);
        NavigationPanel2.SetActive(true);
        tabBar.SetActive(true);
        musicBar.SetActive(true);
        checkOutPanel.SetActive(true);
    }
    
    public void ShowPayment()
    {
        HideAllPanels();
        paymentPanel.SetActive(true);
    }

    // This function will automatically go to the next step after a delay
    public void ShowPaidConfirmation()
    {
        HideAllPanels();
        paidPanel.SetActive(true);
        // Wait for 2 seconds, then show the final pickup details
        StartCoroutine(ShowPurchaseDetailsAfterDelay(2.0f));
    }
    
    IEnumerator ShowPurchaseDetailsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowPurchaseDetails();
    }

    public void ShowPurchaseDetails()
    {
        HideAllPanels();
        ikeaSearchPanel.SetActive(true);
        NavigationPanel.SetActive(true);
        NavigationPanel2.SetActive(true);
        tabBar.SetActive(true);
        musicBar.SetActive(true);
        pickupDetailsPanel.SetActive(true);
        wishList2Panel.SetActive(true);
        recommended2Panel.SetActive(true);
    }

    // A helper function to hide everything, making state changes easier
    void HideAllPanels()
    {
        // Deactivate all panels
        ikeaSearchPanel.SetActive(false);
        welcomePanel.SetActive(false);
        wishListPanel.SetActive(false);
        recommendedPanel.SetActive(false);
        sofa1Panel.SetActive(false);
        sofa2Panel.SetActive(false);
        checkOutPanel.SetActive(false);
        paymentPanel.SetActive(false);
        paidPanel.SetActive(false);
        pickupDetailsPanel.SetActive(false);
        wishList2Panel.SetActive(false);
        recommended2Panel.SetActive(false);
        NavigationPanel.SetActive(false);
        NavigationPanel2.SetActive(false);
        // Keep these on for now as they are always there
        tabBar.SetActive(true);
        musicBar.SetActive(true);
    }
}   