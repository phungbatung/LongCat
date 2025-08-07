using UnityEngine;
using UnityEngine.UI;

public class TestCameraTexture : MonoBehaviour
{
    [Header("Option 1")]
    public Camera captureCamera;                  // Camera cần chụp
    public RenderTexture renderTexture;           // Đã gán vào camera
    public RawImage displayImage;                 // RawImage UI để hiển thị ảnh chụp
    
    [ContextMenu("Capture1")]
    public void CaptureAndDisplay()
    {
        Debug.Log("log1");
        RenderTexture.active = captureTarget;

        int w = Mathf.Min(captureTarget.width, captureTarget.height);
        int h = w;

        // Tính toán offset để capture ở center
        int offsetX = (captureTarget.width - w) / 2;
        int offsetY = (captureTarget.height - h) / 2;

        Texture2D tex = new Texture2D(w, h, TextureFormat.RGBA32, false, true);

        // Capture từ vị trí center thay vì (0,0)
        tex.ReadPixels(new Rect(offsetX, offsetY, w, h), 0, 0);
        tex.Apply();

        // Gán lên UI
        displayImage.texture = tex;
        RenderTexture.active = null;
    }

    [Space]
    [Header("Option2")]
    public Camera postProcessingCamera;
    public RenderTexture captureTarget;
    public RawImage rawImage;

    [ContextMenu("Capture2")]
    void CaptureAfterPostProcessing()
    {
        Debug.Log("log2");
        RenderTexture.active = captureTarget;

        Texture2D tex = new Texture2D(captureTarget.width, captureTarget.height, TextureFormat.RGBA32, false, true);
        tex.ReadPixels(new Rect(0, 0, captureTarget.width, captureTarget.height), 0, 0);
        tex.Apply();

        // Gán lên UI
        rawImage.texture = tex;

        RenderTexture.active = null;
    }
}
