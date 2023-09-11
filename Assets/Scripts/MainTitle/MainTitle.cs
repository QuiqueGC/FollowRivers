using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainTitle : MonoBehaviour
{
    [SerializeField] GameObject pauseUI;
    private bool choiceDone;
    private bool firstKeyDown;
    private AudioSource mainTitleAudioSource;
    const float SOUND_DURATION = 0.289f;

    void Start()
    {
        mainTitleAudioSource = GetComponent<AudioSource>();
        choiceDone = false;
        firstKeyDown = true;
    }

    void Update()
    {
        if (Input.anyKey && firstKeyDown)
        {
            GoToNextUI();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !firstKeyDown && !choiceDone)
        {
            StartGame();
        }
        else if (Input.GetKeyDown("z") && !firstKeyDown && !choiceDone)
        {
            QuitGame();
        }
    }

    private void GoToNextUI()
    {
        firstKeyDown = false;
        pauseUI.SetActive(true);
        mainTitleAudioSource.Play();
    }

    private void StartGame()
    {
        choiceDone = true;
        StartCoroutine(callingFXPreviousToStart());
    }

    private IEnumerator callingFXPreviousToStart()
    {
        mainTitleAudioSource.Play();

        yield return new WaitForSeconds(SOUND_DURATION);

        SceneManager.LoadScene("Stage1");
    }

    private void QuitGame()
    {
        choiceDone = true;

        Application.Quit();
    }
}
