using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinSceneManagement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI bestPlayerName;
    [SerializeField] private TextMeshProUGUI bestPlayerScore;
    private string playerName;
    [SerializeField] private Selectable inputNameField;

    void Start()
    {
        score.text = Gats.score.ToString();
        inputNameField.Select();

        bestPlayerName.text = PlayerPrefs.GetString("playerName","Nadie juega a esto");
        bestPlayerScore.text = PlayerPrefs.GetFloat("playerScore", 0).ToString();
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (PlayerPrefs.GetFloat("playerScore", 0) < Gats.score)
            {
                PlayerPrefs.SetString("playerName", playerName);
                PlayerPrefs.SetFloat("playerScore", Gats.score);
            }

            Application.Quit();
        }
    }

    public void ChangePlayerName(string name)
    {
        playerName = name;
    }
}
