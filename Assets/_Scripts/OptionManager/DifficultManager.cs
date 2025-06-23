using UnityEngine;
using UnityEngine.UI;

public class DifficultManager : MonoBehaviour
{
    public GameObject DifficultyToggles;
    public  enum Difficulties { Easy, Medium, Hard };
    public static Difficulties Difficulty = Difficulties.Easy;



    private void Start()
    {
        DifficultyToggles.transform.GetChild((int)Difficulty).GetComponent<Toggle>().isOn = true;
    }

    public void SetEasyDifficulty(bool isOn)
    {
        if (isOn)
        {
            Difficulty = Difficulties.Easy;
            Debug.Log("Easy");
        }
    }

    public void SetMediumDifficulty(bool isOn)
    {
        if (isOn)
        {
            Difficulty = Difficulties.Medium;
            Debug.Log("Medium");
        }
    }

    public void SetHardDifficulty(bool isOn)
    {
        if (isOn)
        {
            Difficulty = Difficulties.Hard;
            Debug.Log("Hard");
        }
    }
}
