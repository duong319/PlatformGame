using UnityEngine;
using UnityEngine.UI;
using System.Collections;


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
    public Transform attackPoint;
    public float attackRadius = 1f;
    public LayerMask attackLayer;
    private bool isCastingSkill = false;

    [Header("Health & Enrage")]
    public float maxHealth = 100f;
    private float currentHealth;
    private bool isEnraged = false;
    private bool isHurting = false;
    private bool isImmune = false;
    [SerializeField]
    private Slider enemyHealthSlider;
    public GameObject HealthBarCanvas;

    [Header("Special Skill")]
    private int hitCounter = 0;
    public int hitsBeforeSkill = 4;

    private Rigidbody2D rb;
    private Animator animator;
    private bool facingLeft = true;
    [SerializeField]
    public GameObject skillPrefab;
    public Transform[] spawnPoints;

    public DifficultManager difficultManager;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        difficultManager=GetComponent<DifficultManager>();

        switch (DifficultManager.Difficulty)
        {
            case DifficultManager.Difficulties.Easy:
                maxHealth = 30f;
                break;

            case DifficultManager.Difficulties.Medium:
                maxHealth = 60f;
                break;

            case DifficultManager.Difficulties.Hard:
                maxHealth = 100f;
                break;


        }
    }

    void Update()
    {
        if (player == null || currentHealth <= 0) return;
        if (isHurting)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("IsMoving", false);
            return;
        }
        if (isImmune) return;
        // Enrage check
        if (!isEnraged && currentHealth <= maxHealth / 2f)
        {
            Enrage();
        }
        if (isCastingSkill)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("IsMoving", false);
            return;

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
            AudioManager.Instance.PlaySFXWithDelay(AudioManager.Instance.BossBattle, 0.1f, 0.07f);
            AudioManager.DisableBackGround();   
            HealthBarCanvas.gameObject.SetActive(true);
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
        AudioManager.Instance.PlaySFXWithDelay(AudioManager.Instance.BossSwing, 0.5f);
        rb.linearVelocity = Vector2.zero;
        animator.SetTrigger("Attack");
        isAttacking = true;
        attackTimer = attackCooldown;
    }
    public void DealDamage()
    {
        Collider2D coll = Physics2D.OverlapCircle(attackPoint.position, attackRadius, attackLayer);
        if (coll)
        {
            coll.gameObject.GetComponent<PlayerHealth>().TakeDamage(5);
            AudioManager.PlayPlayerHurt();
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0 || isImmune) return;
        AudioManager.PlayBossHurt();
        currentHealth -= damage;
      //  animator.SetTrigger("Hurt");

        hitCounter++;
        if (hitCounter >= hitsBeforeSkill)
        {
            FindFirstObjectByType<HealthPotionManager>().SpawnAtRandomPoint();
            Invoke("CastSpecialSkill", 2.1f);
            hitCounter = 0;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
        enemyHealthSlider.value = currentHealth;
        isHurting = true;
        if (!isEnraged)
        {
            StartCoroutine(EndHurtAfterDelay(0.5f));
            StartCoroutine(TriggerImmune(2f));
            animator.SetTrigger("Hurt");
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
        StartCoroutine(SpawnInSequence());

    }

    void Die()
    {
        animator.SetTrigger("Die");
        rb.linearVelocity = Vector2.zero;

        StartCoroutine(DieSequence());


    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    private IEnumerator EndHurtAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isHurting = false;
    }

    private IEnumerator TriggerImmune(float duration)
    {
        isImmune = true;
        yield return new WaitForSeconds(duration);
        isImmune = false;
    }

    IEnumerator SpawnInSequence()
    {
        isImmune = true;
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("IsMoving", false);

        foreach (Transform point in spawnPoints)
        {
            Instantiate(skillPrefab, point.position, point.rotation);
            AudioManager.Instance.PlaySFXWithDelay(AudioManager.Instance.BossSkill,0f);

            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(5f);
        isImmune = false;
        isCastingSkill = false;
    }
    IEnumerator DieSequence()
    {
        var questManager = FindFirstObjectByType<DailyQuestManager>();
        if (questManager != null)
        {
            questManager.AddProgressToQuest("2", 1);
        }
        else
        {
            Debug.LogWarning("DailyQuestManager ");
        }
        AudioManager.Instance.StopSFX(AudioManager.Instance.BossBattle);
        yield return new WaitForSeconds(4f);
        FindFirstObjectByType<GameClearScene>().LevelClear();
        AudioManager.Instance.PlaySFXWithDelay(AudioManager.Instance.LevelClear,0f);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
