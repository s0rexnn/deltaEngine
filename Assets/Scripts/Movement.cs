using NUnit.Framework.Internal.Filters;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    public float walkingSpeed = 0f;
    public float runningSpeed = 0f;
    public float maxSpeed = 0f;
    public float acceleration = 0.0001f;
    private bool isMoving = false;
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
            input = new Vector2(horizontal, vertical).normalized;

            isMoving = input != Vector2.zero;

            float currentSpeed = walkingSpeed;

            if (Input.GetKey(KeyCode.LeftShift) && isMoving || !canMove)
            { 
                runningSpeed += acceleration * Time.deltaTime;
                runningSpeed = Mathf.Clamp(runningSpeed, walkingSpeed, maxSpeed);
                currentSpeed = runningSpeed;
            }
            else
            {
                runningSpeed = walkingSpeed;
                anim.speed = 1f;
            }

            if (currentSpeed > 4.5f)
                anim.speed = 1.8f;
            else if (currentSpeed > 3.5f)
                anim.speed = 1.4f;

            if (isMoving)
            {
                lastMoveDirection = input;
            }

            anim.SetBool("isMoving", isMoving);

            if (isMoving)
            {
                if (Mathf.Abs(input.y) > 0)
                {
                    anim.SetFloat("moveY", input.y);
                    anim.SetFloat("moveX", 0);
                }
                else if (Mathf.Abs(input.x) > 0)
                {
                    anim.SetFloat("moveX", input.x);
                    anim.SetFloat("moveY", 0);
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