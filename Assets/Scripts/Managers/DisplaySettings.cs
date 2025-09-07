using UnityEngine;

public class DisplaySettings : MonoBehaviour
{
    [Header("Display Settings")]
    [SerializeField] private int targetWidth = 960;
    [SerializeField] private int targetHeight = 720;
    [SerializeField] private int targetFrameRate = 30;
    [SerializeField] private bool startFullscreen = false;
    
    [Header("Aspect Ratio Control")]
    [SerializeField] private bool maintainAspectRatio = true;
    
    private Camera mainCamera;
    private float targetAspect;
    private int lastScreenWidth;
    private int lastScreenHeight;
    private bool lastFullscreenState;

    void Awake()
    {
        Application.targetFrameRate = targetFrameRate;
        
        mainCamera = GetComponent<Camera>();
        if (mainCamera == null)
            mainCamera = Camera.main;
            
        targetAspect = (float)targetWidth / (float)targetHeight;
        
        // Set frame rate
        Application.targetFrameRate = targetFrameRate;
        QualitySettings.vSyncCount = 0;
        
        // Set initial resolution and fullscreen state
        Screen.SetResolution(targetWidth, targetHeight, startFullscreen);
        
        // Store initial values
        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;
        lastFullscreenState = Screen.fullScreen;
    }

    void Start()
    {
        UpdateDisplay();
    }

    void Update()
    {
        // Check if screen properties changed
        if (Screen.width != lastScreenWidth || 
            Screen.height != lastScreenHeight || 
            Screen.fullScreen != lastFullscreenState)
        {
            UpdateDisplay();
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
            lastFullscreenState = Screen.fullScreen;
        }
        
        // Handle toggle fullscreen (F11 or Alt+Enter)
        if (Input.GetKeyDown(KeyCode.F11) || 
            (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Return)))
        {
            ToggleFullscreen();
        }
    }

    void UpdateDisplay()
    {
        if (mainCamera == null) return;

        // Always maintain the locked resolution
        if (Screen.width != targetWidth || Screen.height != targetHeight)
        {
            Screen.SetResolution(targetWidth, targetHeight, Screen.fullScreen);
            return;
        }

        // Apply black bars to maintain aspect ratio
        if (maintainAspectRatio)
        {
            ApplyLetterboxing();
        }
        else
        {
            // Reset to full viewport if not maintaining aspect ratio
            mainCamera.rect = new Rect(0, 0, 1, 1);
        }
    }

    void ApplyLetterboxing()
    {
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            // Letterbox - black bars on top and bottom
            Rect rect = new Rect(0, (1.0f - scaleHeight) / 2.0f, 1.0f, scaleHeight);
            mainCamera.rect = rect;
        }
        else
        {
            // Pillarbox - black bars on left and right
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = new Rect((1.0f - scaleWidth) / 2.0f, 0, scaleWidth, 1.0f);
            mainCamera.rect = rect;
        }
    }

    // Public methods for runtime control
    public void SetResolution(int width, int height)
    {
        targetWidth = width;
        targetHeight = height;
        targetAspect = (float)width / (float)height;
        Screen.SetResolution(width, height, Screen.fullScreen);
        UpdateDisplay();
    }

    public void SetFrameRate(int frameRate)
    {
        targetFrameRate = frameRate;
        Application.targetFrameRate = frameRate;
    }

    public void ToggleFullscreen()
    {
        Screen.SetResolution(targetWidth, targetHeight, !Screen.fullScreen);
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.SetResolution(targetWidth, targetHeight, fullscreen);
    }

    public void ToggleAspectRatioMaintenance()
    {
        maintainAspectRatio = !maintainAspectRatio;
        UpdateDisplay();
    }

    // Getters
    public Vector2 GetTargetResolution() => new Vector2(targetWidth, targetHeight);
    public int GetTargetFrameRate() => targetFrameRate;
    public bool IsFullscreen() => Screen.fullScreen;
    public bool IsMaintainingAspectRatio() => maintainAspectRatio;
}