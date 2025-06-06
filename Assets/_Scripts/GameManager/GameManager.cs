using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isGameActive = true;
    public float Coin;
    public Text CoinText;

    public void Update()
    {
        CoinText.text=Coin.ToString();
    }
}
