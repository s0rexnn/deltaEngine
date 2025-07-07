using UnityEngine;

public class ScreenScript : MonoBehaviour
{

    void Awake()
    {
        Screen.SetResolution(960, 720, false); 
        Application.targetFrameRate = 30; 
    }

    [SerializeField]
    private float targetAspect = 4f / 3f; 

    void Start()
    {
        UpdateViewport();
    }

    void Update()
    {
        
        UpdateViewport();
    }

    void UpdateViewport()
    {
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        Camera cam = GetComponent<Camera>();

        if (scaleHeight < 1.0f)
        {
            
            Rect rect = new Rect(0, (1.0f - scaleHeight) / 2.0f, 1.0f, scaleHeight);
            cam.rect = rect;
        }
        else
        {
            
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = new Rect((1.0f - scaleWidth) / 2.0f, 0, scaleWidth, 1.0f);
            cam.rect = rect;
        }
    }
}