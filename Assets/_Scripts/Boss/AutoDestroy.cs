using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float destroyAfter = 1f;

    void Start()
    {
        Destroy(gameObject, destroyAfter);
    }
}
