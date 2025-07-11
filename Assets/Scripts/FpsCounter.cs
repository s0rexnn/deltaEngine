using UnityEngine;
using TMPro;

public class FpsCounter : MonoBehaviour
{
    public TextMeshProUGUI fpsText;

    private float timer;
    private float refreshRate = 0.5f; // Update every second

    void Update()
    {
        timer += Time.unscaledDeltaTime;

        if (timer >= refreshRate)
        {
            float fps = 1f / Time.unscaledDeltaTime;
            fpsText.text = "FPS: " + Mathf.RoundToInt(fps).ToString();
            timer = 0f;
        }


    }
}
