using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject BtnClickSound;
    
    public void GamePlayScene()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void ShopScene()
    {
        SceneManager.LoadScene("Shop");
    }

    public void OptionScene()
    {
        SceneManager.LoadScene("Option");
    }

    public void ClickStartBtn()
    {
        Invoke("GamePlayScene", 2);
        BtnClickSound.SetActive(true);

    }

    public void ClickShopBtn()
    {
        Invoke("ShopScene", 2);
        BtnClickSound.SetActive(true);
    }

    public void ClickOptionBtn()
    {
        Invoke("OptionScene", 2);
        BtnClickSound.SetActive(true);
    }


}
