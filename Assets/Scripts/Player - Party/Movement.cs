using NUnit.Framework.Internal.Filters;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    private enum AxisLock { None, Horizontal, Vertical }
    private AxisLock axisLock = AxisLock.None;

    [Header("Movement Settings")]
    public float walkingSpeed = 0f;
    public float runningSpeed = 0f;
    public float maxSpeed = 0f;
    public float acceleration = 0.0001f;
    public float deceleration = 0.0001f;

    public bool isMoving = false;
    public bool canMove = true;

    public Vector2 LastDirection => lastMoveDirection;
    private Vector2 lastMoveDirection = Vector2.down;
    private Vector2 input;

    public Animator anim;
    private Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameStateManager.Instance.CanPlayerMove)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            input = new Vector2(horizontal, vertical);
            bool diagonal = input.x != 0f && input.y != 0f;
            const float diagBoost = 1.10f;
            input = diagonal ? input.normalized * diagBoost : input.normalized;

            isMoving = input != Vector2.zero;

            bool hasX = Mathf.Abs(input.x) > 0f;
            bool hasY = Mathf.Abs(input.y) > 0f;

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
            else
            {
                axisLock = AxisLock.Vertical;
            }

            float currentSpeed = walkingSpeed;

            if ((Input.GetKey(KeyCode.LeftShift) && isMoving) || !canMove)
            {
                runningSpeed += acceleration * Time.deltaTime;
                runningSpeed = Mathf.Clamp(runningSpeed, walkingSpeed, maxSpeed);
                currentSpeed = runningSpeed;
            }
            else
            {
                runningSpeed -= deceleration * Time.deltaTime;
                runningSpeed = Mathf.Clamp(runningSpeed, walkingSpeed, maxSpeed);
                currentSpeed = runningSpeed;
            }

            if (currentSpeed > 4.5f)
                anim.speed = 1.8f;
            else if (currentSpeed > 3.5f)
                anim.speed = 1.4f;
            else if (currentSpeed == 3.0f)
                anim.speed = 1.0f;

            if (isMoving)
                {
                    if (axisLock == AxisLock.Horizontal)
                        lastMoveDirection = new Vector2(Mathf.Sign(input.x), 0f);
                    else if (axisLock == AxisLock.Vertical)
                        lastMoveDirection = new Vector2(0f, Mathf.Sign(input.y));
                }

            anim.SetBool("isMoving", isMoving);

            if (isMoving)
            {
                if (axisLock == AxisLock.Horizontal)
                {
                    anim.SetFloat("moveX", input.x);
                    anim.SetFloat("moveY", 0f);
                }
                else if (axisLock == AxisLock.Vertical)
                {
                    anim.SetFloat("moveY", input.y);
                    anim.SetFloat("moveX", 0f);
                }
            }
            else
            {
                anim.SetFloat("moveX", lastMoveDirection.x);
                anim.SetFloat("moveY", lastMoveDirection.y);
            }
        }
    }

    private void FixedUpdate()
    {
        if (GameStateManager.Instance.CanPlayerMove && canMove)
        {
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runningSpeed : walkingSpeed;
            Vector2 move = input * currentSpeed;
            rb.MovePosition(rb.position + move * Time.fixedDeltaTime);
        }
    }
}
