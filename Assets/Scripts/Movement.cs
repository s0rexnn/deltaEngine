using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public float runningSpeed = 5f;
    public float maxRunningSpeed = 90f;
    public float acceleration = 0.0001f;
    private bool isMoving = false;

    public Rigidbody2D rb;
    public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }


    void FixedUpdate()
    {
        // Debugging // 
        Debug.Log("Current speed : " + runningSpeed); // Checking speed value

        // Disclaimer - This project uses the old input system //

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        rb.linearVelocity = new Vector2(horizontal, vertical) * speed;

        // A function that will store the last direction the player was facing for the animations

        Vector2 movement = new Vector2(horizontal, vertical);
        if (movement != Vector2.zero)
        {
            anim.SetFloat("moveX", horizontal);
            anim.SetFloat("moveY", vertical);
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        anim.SetBool("isMoving", isMoving);

        // Running //
        if (movement != Vector2.zero)
            if (Input.GetKey(KeyCode.LeftShift))
            {
            rb.linearVelocity = new Vector2(horizontal, vertical) * runningSpeed;
            runningSpeed += acceleration;
            anim.SetFloat("moveX", horizontal);
            anim.SetFloat("moveY", vertical);
            anim.speed = 1.7f;
        }
        else
        {
            runningSpeed = speed;
            anim.speed = 1f; 
        }
        runningSpeed = Mathf.Clamp(runningSpeed, 0f, maxRunningSpeed); // Clamps the running speed to a maximum value

    }
}