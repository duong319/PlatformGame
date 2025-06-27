using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUi : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Button replayButton;
    public Button mainMenuButton;
    public Button reviveButton;
    public GameObject reviveConfirm;
    public Button yesBtn;
    public Button noBtn;

    private void Start()
    {
        gameOverPanel.SetActive(false);

        replayButton.onClick.AddListener(OnReplay);
        mainMenuButton.onClick.AddListener(OnMainMenu);
        reviveButton.onClick.AddListener(OnRevive);
        yesBtn.onClick.AddListener(ConfirmRevive);
        noBtn.onClick.AddListener(ConfirmNotRevive);

    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void OnReplay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GamePlay");
    }

    void OnMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    void OnRevive()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(false);
        reviveConfirm.SetActive(true);

    }

    public void ConfirmRevive()
    {
        if (GameManager.Coin < 5)
        {
            Debug.Log("Not Enough Coin");
            return;
        }
        Time.timeScale = 1f;
        GameManager.Coin -= 5;
        reviveConfirm.SetActive(false);
        gameOverPanel.SetActive(false);
        FindFirstObjectByType<PlayerHealth>().Revive();
    }

    public void ConfirmNotRevive()
    {
        gameOverPanel.SetActive(true);
        reviveConfirm.SetActive(false);
    }
}
