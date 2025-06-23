using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    public int PlayerHp;
    public int PlayerAtk;
    public int PlayerCoin;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData(); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void AddHP(int Amount)
    {
        PlayerHp += Amount;

    }

    public void AddAtk(int Amount)
    {
        PlayerAtk += Amount;
    }

    public bool SpendCoins(int amount)
    {
        if (PlayerCoin >= amount)
        {
            PlayerCoin -= amount;
            return true;
        }
        return false;
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("coins", PlayerCoin);
        PlayerPrefs.SetInt("atk", PlayerAtk);
        PlayerPrefs.SetInt("maxHP", PlayerHp);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        PlayerAtk = PlayerPrefs.GetInt("atk", 1);
        PlayerHp = PlayerPrefs.GetInt("maxHP", 20);
        PlayerCoin = PlayerPrefs.GetInt("coins", 0);

        PlayerAttack.playerAttack = PlayerAtk;
        PlayerHealth.playerMaxHealth = PlayerHp;
        GameManager.Coin = PlayerCoin;

    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }

    void OnApplicationQuit()
    {
        SaveData();
    }

}
