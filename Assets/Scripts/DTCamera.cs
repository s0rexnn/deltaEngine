using UnityEngine;

public class DTCamera : MonoBehaviour
{
    public static DTCamera Instance { get; private set; }

    [Header("Camera Following")]
    [SerializeField] private Transform followTarget;
    [SerializeField] private Vector2 offset = Vector2.zero;

    [Header("Polygon Boundaries")]
    [SerializeField] public PolygonCollider2D boundaryPolygon;

    [Header("Pixel-Perfect")]
    [SerializeField] private bool pixelPerfect = true;
    [SerializeField] private int pixelsPerUnit = 32; // match your sprites’ PPU

    private Camera cam;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        cam = GetComponent<Camera>();
        if (!cam) cam = Camera.main;
    }

    void LateUpdate()
    {
        if (!followTarget) return;

        // Target position = player + offset
        Vector3 targetPos = followTarget.position + (Vector3)offset;
        targetPos.z = transform.position.z;

        // Clamp inside room
        if (boundaryPolygon) targetPos = ConstrainToBounds(targetPos);

        // Pixel-perfect rounding (optional)
        transform.position = PixelRound(targetPos);
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

    private Vector3 PixelRound(Vector3 worldPos)
    {
        if (!pixelPerfect || pixelsPerUnit <= 0) return worldPos;
        float x = Mathf.Round(worldPos.x * pixelsPerUnit) / pixelsPerUnit;
        float y = Mathf.Round(worldPos.y * pixelsPerUnit) / pixelsPerUnit;
        return new Vector3(x, y, worldPos.z);
    }

    public void SetFollowTarget(Transform target) => followTarget = target;
    public void SetBoundaryPolygon(PolygonCollider2D polygon) => boundaryPolygon = polygon;
}
