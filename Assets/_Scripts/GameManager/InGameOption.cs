using UnityEngine;
using UnityEngine.UI;

public class InGameOption : MonoBehaviour
{
    public GameObject InGameOptionPanel;
    public Button BackBtn;

    private void Start()
    {
        InGameOptionPanel.SetActive(false);
        BackBtn.onClick.AddListener(Back);
    }

    public void Back()
    {
        InGameOptionPanel.SetActive(false);
    }
}
