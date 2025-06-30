
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlyEnemies : MonoBehaviour
{
    public float maxHealth = 5;
    public float moveSpeed = 3f;
    public float diveSpeed = 6f;
    public float returnSpeed = 4f;

    public Transform checkpoint;
    public float distance = 1f;
    public LayerMask layerMask;
    public bool facingright = true;

    public float attackRange = 5f;
    public Transform player;
    public LayerMask playerLayer;
    public Transform attackPoint;

    public float diveCooldown = 3f;         
    private float lastDiveTime = -Mathf.Infinity;


    public Animator animator;
    public Rigidbody2D rb;
    private Vector2 startPosition;

    [SerializeField]
    private Slider enemyHealthSlider;

    private bool isHurting = false;

    private enum State { Patrolling, Diving, Returning }
    private State currentState = State.Patrolling;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {

        if (maxHealth <= 0)
        {
            rb.AddForce(Vector2.down * 4f);
            animator.SetTrigger("Die");
            Invoke("DestroyEnemy", 2);
            Invoke("DropItem", 2);
            return;

        }
        if (isHurting)return;
        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
                DetectPlayer();
                break;
            case State.Diving:
                Dive();
                break;
            case State.Returning:
                ReturnToStart();
                break;
        }
    }

    void Patrol()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        RaycastHit2D hit = Physics2D.Raycast(checkpoint.position, Vector2.down, distance, layerMask);

        if (hit.collider != null && facingright)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            facingright = false;
        }
        else if (hit.collider != null && !facingright)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            facingright = true;
        }
    }

    void DetectPlayer()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && Time.time >= lastDiveTime + diveCooldown)
        {
            currentState = State.Diving;
            lastDiveTime = Time.time;
        }
    }

    void Dive()
    {
        //rb.linearVelocity = new Vector2(0, -diveSpeed);
        transform.position = Vector2.MoveTowards(transform.position, attackPoint.position, diveSpeed * Time.deltaTime);

        if (transform.position.y <= player.position.y - 1.5f)
        {
            rb.velocity = Vector2.zero;
            currentState = State.Returning;
        }
    }

    void ReturnToStart()
    {
        Vector2 target = new Vector2(transform.position.x, startPosition.y);
        transform.position = Vector2.MoveTowards(transform.position, target, returnSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            currentState = State.Patrolling;
        }
    }

    public void TakeDamage(int damage)
    {
        if (maxHealth <= 0)
        {
            return;
        }
        animator.SetTrigger("Hurt");
        maxHealth -= damage;
        enemyHealthSlider.value = maxHealth;
        isHurting = true;

        StartCoroutine(EndHurtAfterDelay(1));
    }





    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private IEnumerator EndHurtAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isHurting = false;
    }
}
