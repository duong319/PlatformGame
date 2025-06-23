using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public PlayerStats player;
    public static ShopManager Instance;



    public int atkCost => player.PlayerAtk * 2;
    public int hpCost => Mathf.CeilToInt(player.PlayerHp / 5f);

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        player = PlayerStats.Instance;
        player.LoadData();
       

    }



    public bool UpgradeATK()
    {
        if (player.SpendCoins(atkCost))
        {
            player.AddAtk(1);
            player.SaveData();
            return true;
        }
        return false;
    }

    public bool UpgradeHP()
    {
        if (player.SpendCoins(hpCost))
        {
            player.AddHP(1);
            player.SaveData();
            return true;
        }
        return false;
    }


}
