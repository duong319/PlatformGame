using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;


public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator Anim;

    private SpriteRenderer spriteRenderer;

    public float MoveSpeed = 5f;
    public float JumpForce = 10f;
    public float horizontalInput;
    private bool Grounded;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space)&&Grounded)
        {
            Jump();
        }
        Flip();
        //Anim
        Anim.SetBool("Walk", horizontalInput != 0);
        Anim.SetBool("Grounded", Grounded);


    }

    private void FixedUpdate()
    {

        rb.linearVelocity = new Vector2(horizontalInput * MoveSpeed, rb.linearVelocity.y);

    }


    private void Flip()
    {
        if (horizontalInput > 0.01f)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < -0.01f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
        Grounded = false;
        Anim.SetTrigger("Jump");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Grounded = true;
        }
;
    }


}
