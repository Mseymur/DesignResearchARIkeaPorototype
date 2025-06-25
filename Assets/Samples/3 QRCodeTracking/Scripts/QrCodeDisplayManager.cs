using System;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR;
using PassthroughCameraSamples;
using UnityEngine.SceneManagement; // <-- **Added this line**

public class QrCodeDisplayManager : MonoBehaviour
{
#if ZXING_ENABLED
    [SerializeField] private QrCodeScanner scanner;
    [SerializeField] private EnvironmentRaycastManager envRaycastManager;
    [SerializeField] private WebCamTextureManager passthroughCameraManager;

    private readonly Dictionary<string, MarkerController> _activeMarkers = new();
    private PassthroughCameraEye _passthroughCameraEye;

    private void Awake()
    {
        _passthroughCameraEye = passthroughCameraManager.Eye;
    }

    private void Update()
    {
        UpdateMarkers();
    }

    private async void UpdateMarkers()
    {
        var qrResults = await scanner.ScanFrameAsync() ?? Array.Empty<QrCodeResult>();

        foreach (var qrResult in qrResults)
        {
            if (string.IsNullOrEmpty(qrResult?.text) || qrResult.corners == null || qrResult.corners.Length < 4)
            {
                continue;
            }

            // This section updates the visual markers in the world. We will leave it as is.
            #region Existing Marker Update Logic
            var count = qrResult.corners.Length;
            var uvs = new Vector2[count];
            for (var i = 0; i < count; i++)
            {
                uvs[i] = new Vector2(qrResult.corners[i].x, qrResult.corners[i].y);
            }
            
            var centerUV = Vector2.zero;
            foreach (var uv in uvs)
            {
                centerUV += uv;
            }
            centerUV /= count;

            var intrinsics = PassthroughCameraUtils.GetCameraIntrinsics(_passthroughCameraEye);
            var centerPixel = new Vector2Int(
                Mathf.RoundToInt(centerUV.x * intrinsics.Resolution.x),
                Mathf.RoundToInt(centerUV.y * intrinsics.Resolution.y)
            );
            
            var centerRay = PassthroughCameraUtils.ScreenPointToRayInWorld(_passthroughCameraEye, centerPixel);
            if (!envRaycastManager || !envRaycastManager.Raycast(centerRay, out var hitInfo))
            {
                continue;
            }

            var center = hitInfo.point;
            var distance = Vector3.Distance(centerRay.origin, hitInfo.point);

            var tempCorners = new Vector3[count];
            for (var i = 0; i < count; i++)
            {
                var pixelCoord = new Vector2Int(
                    Mathf.RoundToInt(uvs[i].x * intrinsics.Resolution.x),
                    Mathf.RoundToInt(uvs[i].y * intrinsics.Resolution.y)
                );
                
                var r = PassthroughCameraUtils.ScreenPointToRayInWorld(_passthroughCameraEye, pixelCoord);
                tempCorners[i] = r.origin + r.direction * distance;
            }

            var up = (tempCorners[1] - tempCorners[0]).normalized;
            var right = (tempCorners[2] - tempCorners[1]).normalized;
            var normal = -Vector3.Cross(up, right).normalized;
            var qrPlane = new Plane(normal, center);
            var worldCorners = new Vector3[count];
            
            for (var i = 0; i < count; i++)
            {
                var pixelCoord = new Vector2Int(
                    Mathf.RoundToInt(uvs[i].x * intrinsics.Resolution.x),
                    Mathf.RoundToInt(uvs[i].y * intrinsics.Resolution.y)
                );
                
                var r = PassthroughCameraUtils.ScreenPointToRayInWorld(_passthroughCameraEye, pixelCoord);
                if (qrPlane.Raycast(r, out var enter))
                {
                    worldCorners[i] = r.GetPoint(enter);
                }
                else
                {
                    worldCorners[i] = tempCorners[i];
                }
            }

            center = Vector3.zero;
            foreach (var corner in worldCorners)
            {
                center += corner;
            }
            
            center /= count;
            up = (worldCorners[1] - worldCorners[0]).normalized;
            right = (worldCorners[2] - worldCorners[1]).normalized;
            normal = -Vector3.Cross(up, right).normalized;
            
            var poseRot = Quaternion.LookRotation(normal, up);
            var width = Vector3.Distance(worldCorners[0], worldCorners[1]);
            var height = Vector3.Distance(worldCorners[0], worldCorners[3]);
            var scaleFactor = 1.5f;
            var scale = new Vector3(width * scaleFactor, height * scaleFactor, 1f);

            if (_activeMarkers.TryGetValue(qrResult.text, out var marker))
            {
                marker.UpdateMarker(center, poseRot, scale, qrResult.text);
            }
            else
            {
                var markerGo = MarkerPool.Instance.GetMarker();
                if (!markerGo) continue;
                marker = markerGo.GetComponent<MarkerController>();
                if (!marker) continue;
                marker.UpdateMarker(center, poseRot, scale, qrResult.text);
                _activeMarkers[qrResult.text] = marker;
            }
            #endregion
            
            // ===================================================================
            // === NEW SECTION: Logic to load scenes based on QR Code text     ===
            // ===================================================================

            // Check if the DataManager exists to prevent errors
            if (DataManager.Instance != null)
            {
                // SCENARIO 1: Open the app to the default main menu
                if (qrResult.text == "open_ikea_app_now")
                {
                    // Set the instruction to null to ensure the default menu opens
                    DataManager.Instance.itemToShowOnLoad = null;
                    SceneManager.LoadScene("ikea_app");
                }
                // SCENARIO 2: Open the app to show Sofa 2
                else if (qrResult.text == "ikea_app_sofa_2_open")
                {
                    // Set the instruction to "sofa2"
                    DataManager.Instance.itemToShowOnLoad = "sofa2";
                    SceneManager.LoadScene("ikea_app");
                }
            }
            else
            {
                // This message will appear in the console if the _DataManager prefab is missing
                Debug.LogError("DataManager not found! Make sure the _DataManager prefab is in this scene.");
            }
        }

        var keysToRemove = new List<string>();
        foreach (var kvp in _activeMarkers)
        {
            if (!kvp.Value.gameObject.activeSelf)
                keysToRemove.Add(kvp.Key);
        }
        
        foreach (var key in keysToRemove)
        {
            _activeMarkers.Remove(key);
        }
    }
#endif
}