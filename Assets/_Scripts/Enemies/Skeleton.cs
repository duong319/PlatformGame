using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Transform checkpoint;
    public float distance = 1f;
    public LayerMask layerMask;
    private bool facingright;


    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
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
            facingright=true;
        }


    }


    private void OnDrawGizmosSelected()
    {
        if (checkpoint == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(checkpoint.position, Vector2.down * distance);
    }
}
