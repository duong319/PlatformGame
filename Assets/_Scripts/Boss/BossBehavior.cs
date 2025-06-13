using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [Header("Player & Movement")]
    public Transform player;
    public float chaseRange = 10f;
    public float stopDistance = 2f;
    public float moveSpeed = 2.5f;

    [Header("Attack")]
    public float attackCooldown = 2f;
    private float attackTimer = 0f;
    private bool isAttacking = false;

    [Header("Health & Enrage")]
    public float maxHealth = 100f;
    private float currentHealth;
    private bool isEnraged = false;

    [Header("Special Skill")]
    private int hitCounter = 0;
    public int hitsBeforeSkill = 10;

    private Rigidbody2D rb;
    private Animator animator;
    private bool facingLeft = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (player == null || currentHealth <= 0) return;

        // Enrage check
        if (!isEnraged && currentHealth <= maxHealth / 2f)
        {
            Enrage();
        }

        
        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                isAttacking = false;
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
                return; 
            }
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange && distanceToPlayer > stopDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);
            animator.SetBool("IsMoving", true);

            // Flip facing
            if (direction.x < 0 && facingLeft)
            {
                transform.eulerAngles = new Vector3(0, -0, 0);
                facingLeft = false;
            }
            if (direction.x > 0 && !facingLeft) 
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                facingLeft = true;
            }

        }
        else if (distanceToPlayer <= stopDistance)
        {
            Attack();
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            animator.SetBool("IsMoving", false);
        }
    }

    void Attack()
    {
        rb.linearVelocity = Vector2.zero;
        animator.SetTrigger("Attack"); 
        isAttacking = true;
        attackTimer = attackCooldown;
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        hitCounter++;
        if (hitCounter >= hitsBeforeSkill)
        {
            CastSpecialSkill();
            hitCounter = 0;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Enrage()
    {
        isEnraged = true;
        moveSpeed *= 1.5f;
        attackCooldown *= 0.7f; 
        animator.SetTrigger("Enrage");
        Debug.Log("Boss is ENRAGED!");
    }

    void CastSpecialSkill()
    {
        animator.SetTrigger("SpecialSkill");
        Debug.Log("Boss casts SPECIAL SKILL!");
        
    }

    void Die()
    {
        animator.SetTrigger("Die");
        rb.linearVelocity = Vector2.zero;
        this.enabled = false;
        
    }

  
}
