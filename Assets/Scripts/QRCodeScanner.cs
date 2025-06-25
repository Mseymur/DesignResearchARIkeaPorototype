using UnityEngine;
using ZXing;
using UnityEngine.UI; // For UI Text
using TMPro;

public class QRCodeScanner : MonoBehaviour
{
    private WebCamTexture camTexture;
    private IBarcodeReader barcodeReader;

    public RawImage rawImageDisplay; // Optional: show webcam
    public Renderer webcamPlaneRenderer; // Assign the plane's material

    public TMP_Text scanResultText;

    private string lastScannedText = "";

    void Start()
    {
        camTexture = new WebCamTexture();
        camTexture.requestedWidth = 640;
        camTexture.requestedHeight = 480;
        camTexture.Play();

        if (webcamPlaneRenderer != null)
        {
            webcamPlaneRenderer.material.mainTexture = camTexture;
        }

        barcodeReader = new BarcodeReader();
    }

    void Update()
    {
        if (camTexture.width > 100 && camTexture.isPlaying)
        {
            try
            {
                Color32[] pixels = camTexture.GetPixels32();
                var result = barcodeReader.Decode(pixels, camTexture.width, camTexture.height);

                if (result != null && result.Text != lastScannedText)
                {
                    Debug.Log("QR Code Text: " + result.Text);
                    lastScannedText = result.Text;
                    if (scanResultText != null)
                        scanResultText.text = "Scanned: " + result.Text;
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("QR decode error: " + ex.Message);
            }
        }
    }
}