using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePauseUi : MonoBehaviour
{
    public GameObject gamePausePanel;
    public Button continueButton;
    public Button optionButton;
    public Button mainmenuButton;
    public GameObject InGameOption;

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
        AudioManager.DisableBackGround();
        Time.timeScale = 0f;
        gamePausePanel.SetActive(true);
    }

    public void OnContinue()
    {
        AudioManager.EnableBackGround();
        Time.timeScale = 1f;
        gamePausePanel.SetActive(false);
    }

    public void OnOption()
    {
        InGameOption.SetActive(true);
        Time.timeScale = 0f;
        
    }

    public void OnMainMenu()
    {
        AudioManager.DisableBackGround();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

   
}
