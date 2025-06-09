using UnityEngine;
using Unity.UI;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 10;
    public Animator animator;
    public Rigidbody2D rb;
    public float knockBackForce = 2f;
    

    [SerializeField]
    public Slider healthSlider;

    public void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (playerHealth <= 0)
        {
            PlayerDie();
        }
    }

    public void TakeDamage(int damage)
    {
        if (playerHealth <= 0)
        {
            return;
        }
        playerHealth -= damage;
        healthSlider.value = playerHealth;
        animator.SetTrigger("Hurt");
     
    }

    public void PlayerDie()
    {
        FindFirstObjectByType<GameManager>().isGameActive = false;
        animator.SetTrigger("Die");

        Destroy(this.gameObject, 3);

    }

 

}
