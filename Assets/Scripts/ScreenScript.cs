using UnityEngine;

public class ScreenScript : MonoBehaviour
{

    void Awake()
    {
        Screen.SetResolution(960, 720, false); // Set the resolution to 640x480 without fullscreen
        Application.targetFrameRate = 30; // Set the target frame rate to 30 FPS
    }

    [SerializeField]
    private float targetAspect = 4f / 3f; // 4:3 aspect ratio like Deltarune

    void Start()
    {
        UpdateViewport();
    }

    void Update()
    {
        // Optional: re-check every frame in case the window res changes
        UpdateViewport();
    }

    void UpdateViewport()
    {
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        Camera cam = GetComponent<Camera>();

        if (scaleHeight < 1.0f)
        {
            // Add letterbox (top and bottom bars)
            Rect rect = new Rect(0, (1.0f - scaleHeight) / 2.0f, 1.0f, scaleHeight);
            cam.rect = rect;
        }
        else
        {
            // Add pillarbox (side bars)
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = new Rect((1.0f - scaleWidth) / 2.0f, 0, scaleWidth, 1.0f);
            cam.rect = rect;
        }
    }
}