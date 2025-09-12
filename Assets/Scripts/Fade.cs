using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    Color color;

    void Awake()
    {
        color.a = 1f;
    }   

public IEnumerator FadeOut() 
{
    while (color.a > 0)
    {
        color.a -= 0.1f;
        color.a = Mathf.Clamp01(color.a); 
        spriteRenderer.materials[0].color = color;

        yield return new WaitForEndOfFrame();
    }
}

public IEnumerator FadeIn() 
{
    while (color.a < 1)
    {
        color.a += 0.1f;
        color.a = Mathf.Clamp01(color.a); 
        spriteRenderer.materials[0].color = color;

        yield return new WaitForEndOfFrame();
    }
}



}
