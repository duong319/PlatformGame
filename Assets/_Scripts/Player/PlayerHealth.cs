using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Splines;

public class PlayerHealth : MonoBehaviour
{
    public static int playerMaxHealth = 20;
    public Animator animator;
    public Rigidbody2D rb;
    public float knockBackForce = 2f;
    public bool isHurting = false;
    public int currentHealth;
    public HealthBar healthBar;
    public bool isDead = false;

    public void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = playerMaxHealth;
        healthBar = GetComponent<HealthBar>();
    }

    public void Update()
    {
        healthBar.SetMaxHealth(playerMaxHealth);
        healthBar.SetHealth(currentHealth);
        if (isHurting)
        {
            GetComponent<PlayerMovement>().enabled = false;
        }
        else
        {
            GetComponent<PlayerMovement>().enabled = true;
        }
        if (currentHealth <= 0&&!isDead)
        {
            PlayerDie();
        }
    }

    public void TakeDamage(int damage)
    {
        isHurting = true;
        if(isDead) return;
        FindFirstObjectByType<PlayerMovement>().rb.linearVelocityX = 0f;
        //if ()
        //{
        //    FindFirstObjectByType<PlayerMovement>().rb.AddForce(Vector2.left * 1f, ForceMode2D.Impulse);
        //}
        //else 
        //{
        //    FindFirstObjectByType<PlayerMovement>().rb.AddForce(Vector2.right * 1f, ForceMode2D.Impulse);
        //}
        if (currentHealth <= 0)
        {
            isDead = true;
            
        }

        currentHealth -= damage;

        animator.SetTrigger("Hurt");
        StartCoroutine(EndHurtAfterDelay(1));

    }

    public void Revive()
    {
        isDead = false;
        currentHealth = playerMaxHealth;
        gameObject.SetActive(true);
        animator.Rebind(); 
        animator.Update(0f);
        FindFirstObjectByType<GameManager>().isGameActive = true;
        AudioManager.EnableBackGround();

    }

    public void PlayerDie()
    {
        isDead = true;
        FindFirstObjectByType<GameManager>().isGameActive = false;
        animator.SetTrigger("Die");
        AudioManager.Instance.PlaySFXWithDelay(AudioManager.Instance.GameOver, 3f);
        StartCoroutine(ShowGameOverAfterDelay(3f));
        StartCoroutine(DisablePlayerAfterDelay(3.5f));


    }
    private IEnumerator EndHurtAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<PlayerMovement>().enabled = true;
        isHurting = false;

    }

    IEnumerator ShowGameOverAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        FindFirstObjectByType<GameOverUi>().ShowGameOver();
        AudioManager.DisableBackGround();
    }

    private IEnumerator DisablePlayerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (isDead)
        {
            gameObject.SetActive(false);
        }
    }



}
