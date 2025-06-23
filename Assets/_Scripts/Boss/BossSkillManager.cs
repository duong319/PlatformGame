using UnityEngine;

public class BossSkillManager : MonoBehaviour
{
    [SerializeField]
    float laserWidth = 0.2f;
    public float maxLength = 5f;            
    public float expandDuration = 0.2f;     
    public float damage = 10f;              
    public LayerMask targetLayer;           

    private BoxCollider2D Collider2D;
    private Vector3 originalScale;
    private bool isExpanding = false;

    void Awake()
    {
        Collider2D = GetComponent<BoxCollider2D>();
        Collider2D.isTrigger = true;
        originalScale = transform.localScale;


        transform.localScale = new Vector3(laserWidth, 0f, 1f);
        Collider2D.size = new Vector2(laserWidth, 0f);
        Collider2D.offset = new Vector2(0f, 0f);    
    }

    void Start()
    {
        StartCoroutine(ExtendLaser());
    }

    private System.Collections.IEnumerator ExtendLaser()
    {
        isExpanding = true;
        float timer = 0f;

        while (timer < expandDuration)
        {
            float t = timer / expandDuration;
            float currentLength = Mathf.Lerp(0f, maxLength, t);


            transform.localScale = new Vector3(laserWidth, currentLength, 1f);
            Collider2D.size = new Vector2(laserWidth/10f, currentLength);
            Collider2D.offset = new Vector2(0f, -currentLength / 2f);

            timer += Time.deltaTime;
            yield return null;
        }

        
        transform.localScale = new Vector3(laserWidth, maxLength, 1f);
        Collider2D.size = new Vector2(laserWidth/10f,maxLength);
        Collider2D.offset = new Vector2(0f, -maxLength/2f);
            
        isExpanding = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            
            var health = collision.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(2);
            }
        }
    }
}
