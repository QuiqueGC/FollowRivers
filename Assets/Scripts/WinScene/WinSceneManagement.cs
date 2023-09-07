using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneManagement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    // Start is called before the first frame update
    void Start()
    {
        score.text = Gats.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("MainTitle");
        }
    }
}
