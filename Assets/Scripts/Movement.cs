using NUnit.Framework.Internal.Filters;
using Unity.VisualScripting;
using UnityEngine;

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

    public Rigidbody2D rb;
    public Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        if (GameStateManager.Instance.CanPlayerMove)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector2 movement = new Vector2(horizontal, vertical);
            float currentSpeed = walkingSpeed;

            if (Input.GetKey(KeyCode.LeftShift) && movement != Vector2.zero || !canMove)
            {
                runningSpeed += acceleration;
                runningSpeed = Mathf.Clamp(runningSpeed, walkingSpeed, maxSpeed);
                currentSpeed = runningSpeed;
                anim.speed = 1.7f;
            }
            else
            {
                runningSpeed = walkingSpeed;
                anim.speed = 1f;
            }

            rb.linearVelocity = movement.normalized * currentSpeed;

            if (rb.linearVelocity.sqrMagnitude > 0.01f)
            {
                anim.SetFloat("moveX", rb.linearVelocity.x);
                anim.SetFloat("moveY", rb.linearVelocity.y);
                isMoving = true;
                lastMoveDirection = movement;
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
                isMoving = false;
                anim.SetFloat("moveX", lastMoveDirection.x);
                anim.SetFloat("moveY", lastMoveDirection.y);
            }

            anim.SetBool("isMoving", isMoving);


            if (Mathf.Abs(movement.y) > 0)
            {
                anim.SetFloat("moveY", vertical);
                anim.SetFloat("moveX", 0);
            }
            else if (Mathf.Abs(movement.x) > 0)
            {
                anim.SetFloat("moveX", horizontal);
                anim.SetFloat("moveY", 0);
            }
        }
    }
}
