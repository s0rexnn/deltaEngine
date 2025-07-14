using UnityEngine;
using TMPro;

public class DebugManager : MonoBehaviour
{
    public TMP_Text debugText;               
    public string[] debugLabels;            
    public float[] debugValues;        
    private Rigidbody2D rb; 



    void Start()
    {
        rb = GameObject.FindWithTag("Player")?.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (debugValues.Length >= 3)
        {
            debugValues[0] = rb.linearVelocity.magnitude;
            debugValues[1] = transform.position.x;
            debugValues[2] = transform.position.y;
        }
        int count = Mathf.Min(debugLabels.Length, debugValues.Length);
        string fullText = "";
        for (int i = 0; i < count; i++)
        {
            fullText += $"{debugLabels[i]}: {debugValues[i]:F2}\n";
        }

        debugText.text = fullText;
    }
}
