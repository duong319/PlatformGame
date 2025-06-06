using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector3 rotation=new Vector3(0,200,0);

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();    
    }

    
    void Update()
    {
        transform.Rotate(rotation*Time.deltaTime);
    }
}
