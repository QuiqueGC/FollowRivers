using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool paused;
    [SerializeField] AudioSource playingSong;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject inGameUI;
    AudioSource pauseSong;
    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        pauseSong = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            Time.timeScale = 0;
            paused = true;
            playingSong.Pause();
            pauseSong.Play();
            pauseUI.SetActive(true);
            inGameUI.SetActive(false);

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            Time.timeScale = 1;
            paused = false;
            playingSong.Play();
            pauseSong.Play();
            pauseUI.SetActive(false);
            inGameUI.SetActive(true);
            
        }
        else if (Input.GetKeyDown("z") && paused)
        {
            Application.Quit();
        }
    }
}
