using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBossTrap : MonoBehaviour
{
    public GameObject BossTrap;
    public GameObject bossTrap;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BossTrap.SetActive(true);
            bossTrap.SetActive(true);
        }
    }
}
