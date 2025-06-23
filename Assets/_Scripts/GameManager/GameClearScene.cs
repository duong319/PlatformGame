using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClearScene : MonoBehaviour
{
    public GameObject gameClearPanel;
    public Button nextLevelButton;
    public Button optionButton;
    public Button mainmenuButton;

    private void Start()
    {
        gameClearPanel.SetActive(false);

        nextLevelButton.onClick.AddListener(NextLevel);
        optionButton.onClick.AddListener(OnOption);
        mainmenuButton.onClick.AddListener(OnMainMenu);

    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level2");
    }

    public void OnOption()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Option");
    }

    public void OnMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void LevelClear()
    {
        Time.timeScale = 0f;
        gameClearPanel.gameObject.SetActive(true);
    }
}
