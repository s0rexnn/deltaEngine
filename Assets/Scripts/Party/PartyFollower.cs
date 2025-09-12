using UnityEngine;

public class PartyFollower : MonoBehaviour
{
    public enum AxisLock { None, Horizontal, Vertical }
    public AxisLock axisLock = AxisLock.None;
    
    [Header("Adjustments")]
    [SerializeField] private PartyLeader leader;
    [SerializeField] private float speed = 0f;
    public int stepsBehind = 10;

    [Header("Animator")]
    public Animator anim;

    private Vector2 currentVelocity;
    private Vector2 inputVector;
    private Movement movement;
    private bool isMoving;
    private Vector2 lastMoveDirection = Vector2.down;

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
            Vector2 targetPos = leader.positions[stepsBehind];

            inputVector = (targetPos - (Vector2)transform.position).normalized;

            if (movement.isMoving)
            {
                currentVelocity = Vector2.one * speed;
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
        else
        {
            inputVector = Vector2.zero;
            currentVelocity = Vector2.zero;
        }

        isMoving = currentVelocity != Vector2.zero;

        if (isMoving)
        {
            bool hasX = Mathf.Abs(inputVector.x) > 0.02f;
            bool hasY = Mathf.Abs(inputVector.y) > 0.02f;

            if (!hasX && !hasY)
            {
                axisLock = AxisLock.None;
            }
            else if (hasX && hasY)
            {
                if (axisLock == AxisLock.None)
                    axisLock = AxisLock.Horizontal;
            }
            else if (hasX)
            {
                axisLock = AxisLock.Horizontal;
            }
            else if (hasY)
            {
                axisLock = AxisLock.Vertical;
            }
        }
        else
        {
            axisLock = AxisLock.None;
        }

        anim.SetBool("isMoving", isMoving);

        if (isMoving && inputVector != Vector2.zero)
        {
            float xDir = 0f;
            float yDir = 0f;
            
            if (Mathf.Abs(inputVector.x) > 0.02f)
                xDir = Mathf.Sign(inputVector.x);
            if (Mathf.Abs(inputVector.y) > 0.02f)
                yDir = Mathf.Sign(inputVector.y);

            if (axisLock == AxisLock.Horizontal)
            {
                anim.SetFloat("moveX", xDir);
                anim.SetFloat("moveY", 0f);
                lastMoveDirection = new Vector2(xDir, 0f);
            }
            else if (axisLock == AxisLock.Vertical)
            {
                anim.SetFloat("moveX", 0f);
                anim.SetFloat("moveY", yDir);
                lastMoveDirection = new Vector2(0f, yDir);  
            }
        
        }
        else if (axisLock == AxisLock.None)
        {
            anim.SetFloat("moveX", lastMoveDirection.x);
            anim.SetFloat("moveY", lastMoveDirection.y);
        }
        
        if (speed > 4.5f)
            anim.speed = 1.8f;
        else if (speed > 3.5f)
            anim.speed = 1.4f;
        else if (speed == 3.0f)
            anim.speed = 1.0f;
    }
}