using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    PlayerStats player;
    public bool isGameActive = true;
    public static int Coin;
   
    public static int inGameCoin;
    public Text inGameCoinText;

    private void Start()
    {
        player = PlayerStats.Instance;
        player.LoadData();
    }

    public void UpdateUI()
    {
        
        inGameCoinText.text = inGameCoin.ToString();
    }

    public void AddCoin(int amount)
    {
        inGameCoin += amount;
        player.PlayerCoin += amount;
        UpdateUI();
    }
}
