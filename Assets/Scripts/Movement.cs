using UnityEngine;

public class Movement : MonoBehaviour
{
    public float dSpeed = 5f; // Default speed
    public float runningSpeed = 5f;
    public float maxRunningSpeed = 10f;
    public float acceleration = 0.0001f;

    private bool isMoving = false;

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
        // Disclaimer - This project uses the old input system //

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        rb.linearVelocity = new Vector2(horizontal, vertical)* dSpeed ;

        // A function that will store the last direction the player was facing for the animations

        Vector2 movement = new Vector2(horizontal, vertical);
        if (movement != Vector2.zero)
        {
            anim.SetFloat("moveX", horizontal);
            anim.SetFloat("moveY", vertical);
            isMoving = true;

            lastMoveDirection = movement;
        }
        else
        {
            isMoving = false;
            anim.SetFloat("moveX", lastMoveDirection.x);
            anim.SetFloat("moveY", lastMoveDirection.y);
        }
        anim.SetBool("isMoving", isMoving);


        // Running //
        if (movement != Vector2.zero)
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.linearVelocity = new Vector2(horizontal, vertical) * runningSpeed ;
                runningSpeed += acceleration;
                anim.SetFloat("moveX", horizontal);
                anim.SetFloat("moveY", vertical);
                anim.speed = 1.7f;
            }
            else
            {
                runningSpeed = dSpeed;
                anim.speed = 1f;
            }
        runningSpeed = Mathf.Clamp(runningSpeed, 0f, maxRunningSpeed); // Clamps the running speed to a maximum value

        // Fixing diagonal animation //

        if (Mathf.Abs(movement.y) > 0)
        {
            anim.SetFloat("moveY", vertical);
            anim.SetFloat("moveX", 0); // if the player is moving vertically, set horizontal to 0
        }
        else if (Mathf.Abs(movement.x) > 0)
        {
            anim.SetFloat("moveX", horizontal);
            anim.SetFloat("moveY", 0); // if the player is moving horizontally, set vertical to 0
        }

    }
}