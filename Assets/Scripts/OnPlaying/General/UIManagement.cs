using TMPro;
using UnityEngine;

public class UIManagement : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI livesTextUI;
    [SerializeField] TextMeshProUGUI scoreTextUI;
    private string actualHP;
    private string actualScore;


    void Update()
    {
        CheckAndWriteLivesOnUI();

        CheckAndWriteScoreOnUI();
    }

    private void CheckAndWriteLivesOnUI()
    {
        actualHP = "Vidas: " + Gats.lives.ToString();

        livesTextUI.text = actualHP;
    }

    private void CheckAndWriteScoreOnUI()
    {
        actualScore = "Comida para el invierno: " + Gats.score.ToString();

        scoreTextUI.text = actualScore;
    }
}
