using UnityEngine;

public class DRCamera : MonoBehaviour
{
    [Header("Camera Following")]
    [SerializeField] private Transform followTarget;
    [SerializeField] private Vector2 offset = Vector2.zero;

    [Header("Polygon Boundaries")]
    [SerializeField] public PolygonCollider2D boundaryPolygon;

    private Camera cam;
    
    void Start()
    {
        cam = GetComponent<Camera>();
        if (!cam) cam = Camera.main;
    }

    void LateUpdate()
    {
        if (!followTarget) return;

        Vector3 targetPos = followTarget.position + (Vector3)offset;
        targetPos.z = transform.position.z;

        if (boundaryPolygon) targetPos = ConstrainToBounds(targetPos);

        transform.position = targetPos;
    }

    private Vector3 ConstrainToBounds(Vector3 pos)
    {
        float camH = cam.orthographicSize * 2f;
        float camW = camH * cam.aspect;
        float halfW = camW * 0.5f;
        float halfH = camH * 0.5f;

        Bounds b = boundaryPolygon.bounds;

        float minX = b.min.x + halfW;
        float maxX = b.max.x - halfW;
        float minY = b.min.y + halfH;
        float maxY = b.max.y - halfH;

        float x = (minX <= maxX) ? Mathf.Clamp(pos.x, minX, maxX) : b.center.x;
        float y = (minY <= maxY) ? Mathf.Clamp(pos.y, minY, maxY) : b.center.y;

        return new Vector3(x, y, pos.z);
    }

    public void SetFollowTarget(Transform target) => followTarget = target;
    public void SetBoundaryPolygon(PolygonCollider2D polygon) => boundaryPolygon = polygon;
}
