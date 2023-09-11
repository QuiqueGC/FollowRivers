using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool paused;
    [SerializeField] AudioSource playingSong;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject inGameUI;
    AudioSource pauseSong;

    void Start()
    {
        paused = false;
        pauseSong = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            ResumeGame();
        }
        else if (Input.GetKeyDown("z") && paused)
        {
            ExitGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        paused = true;
        playingSong.Pause();
        pauseSong.Play();
        pauseUI.SetActive(true);
        inGameUI.SetActive(false);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        paused = false;
        playingSong.Play();
        pauseSong.Play();
        pauseUI.SetActive(false);
        inGameUI.SetActive(true);
    }
    private void ExitGame()
    {
        Application.Quit();
    }
}
