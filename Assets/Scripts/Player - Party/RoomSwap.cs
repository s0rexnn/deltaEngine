using UnityEngine;
using Unity.Cinemachine;

public class RoomSwap : MonoBehaviour
{
    public DTCamera confiner;
    public GameObject newCameraBoundary;
    private Transform playerTransform;
    public Vector2 TargetPosition;

    private Movement playermovement;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playermovement = playerTransform.GetComponent<Movement>();

    }

    void Update()
    {
        if (GameStateManager.Instance.isRoomSwapped)
        {
            GameStateManager.Instance.CanPlayerMove = false;
            playermovement.runningSpeed = playermovement.walkingSpeed;

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameStateManager.Instance.isRoomSwapped = true;
            Invoke("TelportPlayer", 0.5f);
        }
    }

    void TelportPlayer()
    {
        playerTransform.position = TargetPosition;
        confiner.boundaryPolygon = newCameraBoundary.GetComponent<PolygonCollider2D>();
        GameStateManager.Instance.CanPlayerMove = false;
        GameStateManager.Instance.isRoomSwapped = false;
    }
}
