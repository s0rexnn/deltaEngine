using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class DepthSorting : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

   
    void LateUpdate()

    // Lower Y position means higher depth
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100f);
    }
}
