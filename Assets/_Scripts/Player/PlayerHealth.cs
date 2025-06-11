using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Splines;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 10;
    public Animator animator;
    public Rigidbody2D rb;
    public float knockBackForce = 2f;
    public bool isHurting = false;
    




    [SerializeField]
    public Slider healthSlider;

    public void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (isHurting)
        {
            GetComponent<PlayerMovement>().enabled = false;
        }
        else
        {
            GetComponent<PlayerMovement>().enabled = true;
        }
        if (playerHealth <= 0)
        {
            PlayerDie();
        }
    }

    public void TakeDamage(int damage)
    {
        isHurting = true;
        FindFirstObjectByType<PlayerMovement>().rb.linearVelocityX = 0f;
        //if ()
        //{
        //    FindFirstObjectByType<PlayerMovement>().rb.AddForce(Vector2.left * 1f, ForceMode2D.Impulse);
        //}
        //else 
        //{
        //    FindFirstObjectByType<PlayerMovement>().rb.AddForce(Vector2.right * 1f, ForceMode2D.Impulse);
        //}
        if (playerHealth <= 0)
        {
            return;
        }

        playerHealth -= damage;
        healthSlider.value = playerHealth;
        animator.SetTrigger("Hurt");
        StartCoroutine(EndHurtAfterDelay(1));

    }

    public void PlayerDie()
    {
        FindFirstObjectByType<GameManager>().isGameActive = false;
        animator.SetTrigger("Die");

        Destroy(this.gameObject, 3);

    }
    private IEnumerator EndHurtAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<PlayerMovement>().enabled = true;
        isHurting = false;

    }



}
