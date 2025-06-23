using UnityEngine;
using UnityEngine.UI;

public class ShopUi : MonoBehaviour
{
    public Text coinText;
    public Text atkCostText;
    public Text hpCostText;
    public Text atkText;
    public Text hpText;


    public Button upgradeATKButton;
    public Button upgradeHPButton;
    private ShopManager shopManager;
    void Start()
    {
        shopManager = ShopManager.Instance;

        upgradeATKButton.onClick.AddListener(OnUpgradeATK);
        upgradeHPButton.onClick.AddListener(OnUpgradeHP);

        UpdateUI();
    }
    void OnUpgradeATK()
    {
        if (shopManager.UpgradeATK())
        {
            UpdateUI();
        }
    }

    void OnUpgradeHP()
    {
        if (shopManager.UpgradeHP())
        {
            UpdateUI();
        }
    }



    void UpdateUI()
    {
        coinText.text = shopManager.player.PlayerCoin.ToString();
        atkCostText.text = shopManager.atkCost.ToString();
        hpCostText.text = shopManager.hpCost.ToString();
        atkText.text = shopManager.player.PlayerAtk.ToString();
        hpText.text = shopManager.player.PlayerHp.ToString();
    }

}
