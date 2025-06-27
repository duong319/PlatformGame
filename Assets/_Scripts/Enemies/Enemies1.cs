using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using System.Collections;
using NUnit.Framework.Constraints;

public class Enemies1 : MonoBehaviour
{
    public GameObject itemDropPrefab;
    public float maxHealth = 5;
    public float moveSpeed = 1f;
    public float maxHeightDifference = 5f;

    public Transform checkpoint;
    public float distance = 1f;
    public LayerMask layerMask;

    public bool facingright;
    public bool inRange = false;

    public Transform player;
    public float attackRange = 10f;
    public float retrieveDistance = 2f;
    public float chaseSpeed = 3.2f;

    public Animator animator;
    public Rigidbody2D rb;

    public Transform attackPoint;
    public float attackRadius = 1f;
    public LayerMask attackLayer;

    [SerializeField]
    private Slider enemyHealthSlider;

    public float attackCooldown = 1f;
    private float lastAttackTime = -Mathf.Infinity;

    private bool isAttacking = false;
    private bool isHurting = false;
    private bool isDead=false;



    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (FindFirstObjectByType<GameManager>().isGameActive == false)
        {
            return;
        }

        if (maxHealth <= 0&&!isDead)
        {
            
            isDead = true;
            animator.SetTrigger("Die");
            Invoke("DestroyEnemy", 2);
            Invoke("DropItem", 2);
            return;

        }
        
        if (isHurting) return;

        inRange = Vector2.Distance(transform.position, player.position) <= attackRange;
        

        if (!isAttacking&&!isDead)
        {
            if (inRange&& Mathf.Abs(player.position.y - transform.position.y) <= maxHeightDifference )
            {
                if (player.position.x > transform.position.x && facingright == false)
                {
                    transform.eulerAngles = new Vector3(0, -0, 0);
                    facingright = true;
                }
                else if (player.position.x < transform.position.x && facingright == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    facingright = false;
                }

                if (Vector2.Distance(transform.position, player.position) > retrieveDistance)
                {
                    animator.SetBool("Attack", false);
                    transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
                }
                else
                {
                    animator.SetBool("Attack", true);

                }

            }
            else
            {

                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

                RaycastHit2D hit = Physics2D.Raycast(checkpoint.position, Vector2.down, distance, layerMask);

                if (hit == false && facingright)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    facingright = false;
                }
                else if (hit == false && facingright == false)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    facingright = true;
                }
            }   
        }




    }

    public void Attack()
    {
        if (Time.time < lastAttackTime + attackCooldown) return;
        isAttacking = true;
        lastAttackTime = Time.time;
        rb.velocity = Vector2.zero;

        Collider2D coll = Physics2D.OverlapCircle(attackPoint.position, attackRadius, attackLayer);
        //if (Time.time >= lastAttackTime + attackCooldown)
        //{
        //    lastAttackTime = Time.time;
        //}
      
            if (coll&&coll.gameObject.GetComponent<PlayerHealth>() != null)
        {

            coll.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
            AudioManager.PlayPlayerHurt();

        }

        StartCoroutine(EndAttackAfterDelay(attackCooldown));
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

    void DropItem()
    {
        if (itemDropPrefab != null)
        {
            Instantiate(itemDropPrefab, transform.position, Quaternion.identity);
        }
    }



    public void DestroyEnemy()
    {
        var questManager = FindFirstObjectByType<DailyQuestManager>();
        if (questManager != null)
        {
            questManager.AddProgressToQuest("1", 1);
        }
        else
        {
            Debug.LogWarning("DailyQuestManager ");
        }
        Destroy(this.gameObject);  
    }



    private void OnDrawGizmosSelected()
    {
        if (checkpoint == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(checkpoint.position, Vector2.down * distance);


        Gizmos.DrawWireSphere(transform.position, attackRange);

        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    private IEnumerator EndAttackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAttacking = false;
    }

    private IEnumerator EndHurtAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isHurting = false;
    }


}
