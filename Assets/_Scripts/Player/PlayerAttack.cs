using UnityEngine;


public enum ComboState
{
    None,
    Attack1,
    Attack2,
    Attack3
}

public class PlayerAttack : MonoBehaviour
{
    private Animator Anim;
    public Transform attackPoint;
    public float attackRadius = 1f;
    public LayerMask attackLayer;
    public int playerAttack = 1;


    private bool TimeReset;

    private float default_Combo_Timer = 0.5f;
    private float current_combo_Timer;

    private ComboState current_Combo_State;



    void Awake()
    {
        Anim = GetComponent<Animator>();

    }

    void Start()
    {
        current_combo_Timer = default_Combo_Timer;
        current_Combo_State = ComboState.None;
    }

    void Update()
    {
        ComboAttack();
        ResetComboState();

    }

    void ComboAttack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {

            if (current_Combo_State == ComboState.Attack3)
            {
                return;
            }
            current_Combo_State++;
            TimeReset = true;
            current_combo_Timer = default_Combo_Timer;

            if (current_Combo_State == ComboState.Attack1)
            {
                Anim.SetTrigger("Attack1");
            }

            if (current_Combo_State == ComboState.Attack2)
            {
                Anim.SetTrigger("Attack2");
            }

            if (current_Combo_State == ComboState.Attack3)
            {
                Anim.SetTrigger("Attack3");
            }
            GetComponent<PlayerMovement>().enabled = false;
            FindFirstObjectByType<PlayerMovement>().rb.linearVelocityX = 0f;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Space)&&FindFirstObjectByType<PlayerHealth>().isHurting==false)
        {
            GetComponent<PlayerMovement>().enabled = true;
        }
    }

    void ResetComboState()
    {
        if (TimeReset)
        {
            current_combo_Timer -= Time.deltaTime;
            if (current_combo_Timer <= 0f)
            {
                current_Combo_State = ComboState.None;
                TimeReset = false;
            }
        }
    }

    public void Attack()
    {
        Collider2D coll = Physics2D.OverlapCircle(attackPoint.position, attackRadius, attackLayer);
        if (coll)
        {
            if (coll.gameObject.GetComponent<Enemies1>() != null)
            {
                coll.gameObject.GetComponent<Enemies1>().TakeDamage(playerAttack);
            }
            if (coll.gameObject.GetComponent<FlyEnemies>() != null)
            {
                coll.gameObject.GetComponent<FlyEnemies>().TakeDamage(playerAttack);
            }
            if (coll.gameObject.GetComponent<BossBehavior>() != null)
            {
                coll.gameObject.GetComponent<BossBehavior>().TakeDamage(playerAttack);
            }

        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
