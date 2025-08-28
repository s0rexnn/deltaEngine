using UnityEngine;

public class PartyFollower : MonoBehaviour
{
    [Header("Adjustments")]
    [SerializeField] private PartyLeader leader;
    [SerializeField] private float speed = 0f;  
    public int stepsBehind = 10;                 
    private Vector2 currentVelocity;
    private Movement movement;

    void Awake()
    {
    if (leader != null)
        movement = leader.GetComponent<Movement>();
 }

    void Update()
    {
        if (leader != null && leader.positions.Count > stepsBehind)
        {
            speed = movement.runningSpeed;
            currentVelocity = Vector2.one * speed;
            Vector2 targetPos = leader.positions[stepsBehind];

            if (movement.isMoving)
            {
                transform.position = Vector2.MoveTowards(
               transform.position,
               targetPos,
               speed * Time.deltaTime
              );
            }
            else
            {
                currentVelocity = Vector2.zero;
            }
        }
    }
}  
