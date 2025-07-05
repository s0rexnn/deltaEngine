using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5;
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

        if (Mathf.Abs(horizontal) > 0) vertical = 0; // Locks the diagonal movement

    }
}