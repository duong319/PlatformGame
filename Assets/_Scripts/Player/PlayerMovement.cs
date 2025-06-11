using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;


public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    private Animator Anim;

    private SpriteRenderer spriteRenderer;

    public float MoveSpeed = 5f;
    public float JumpForce = 10f;
    public float horizontalInput;
    public bool Grounded;

    public int maxJumpCount = 2;
    private int jumpCount = 0;





    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();


    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount<maxJumpCount)
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
             
           transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (horizontalInput < -0.01f)
        {
             
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
        jumpCount++;
        Grounded = false;
        Anim.SetTrigger("Jump");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Grounded = true;
            jumpCount = 0;
        }

;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Coin"))
        {
            FindFirstObjectByType<GameManager>().Coin += 1f;
            col.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Collected");
            Destroy(col.gameObject, 1f);
        }
        if (col.gameObject.CompareTag("Enemies"))
        {
            FindFirstObjectByType<PlayerHealth>().TakeDamage(1);
            Debug.Log("hit");
        }


    }




}
