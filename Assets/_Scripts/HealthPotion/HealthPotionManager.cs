using UnityEngine;

public class HealthPotionManager : MonoBehaviour
{
    [SerializeField]
    public GameObject HealthPotion;
    public Transform[] spawnPoints;

    public void SpawnAtRandomPoint()
    {
        if (spawnPoints.Length == 0) return;

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        GameObject obj = Instantiate(HealthPotion, spawnPoint.position, spawnPoint.rotation);

    }

    

}
