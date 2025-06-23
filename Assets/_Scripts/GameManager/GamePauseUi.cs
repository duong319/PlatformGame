using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePauseUi : MonoBehaviour
{
    public GameObject gamePausePanel;
    public Button continueButton;
    public Button optionButton;
    public Button mainmenuButton;

    private void Start()
    {
        gamePausePanel.SetActive(false);

        continueButton.onClick.AddListener(OnContinue);
        optionButton.onClick.AddListener(OnOption);
        mainmenuButton.onClick.AddListener(OnMainMenu);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        gamePausePanel.SetActive(true);
    }

    public void OnContinue()
    {
        Time.timeScale = 1f;
        gamePausePanel.SetActive(false);
    }

    public void OnOption()
    {
        SceneManager.LoadScene("Option");
    }

    public void OnMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

   
}
