using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] RectTransform handToMove;
    Vector3 leftChoice = new Vector3(360, 100, 0);
    Vector3 rightChoice = new Vector3(1050, 100, 0);
    private bool choiceDone;
    private AudioSource gameOverAudioSource;
    private AudioSource handToMoveAudioSource;
    private const float SOUND_DURATION = 0.289f;

    void Start()
    {
        gameOverAudioSource = gameObject.GetComponent<AudioSource>();
        handToMoveAudioSource = handToMove.gameObject.GetComponent<AudioSource>();
        choiceDone = false;
        Gats.lives = 3;
        Gats.score = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown("a") && !choiceDone)
        {
            handToMove.position = leftChoice;

            handToMoveAudioSource.Play();
        }
        if (Input.GetKeyDown("d") && !choiceDone) 
        {
            handToMove.position = rightChoice;

            handToMoveAudioSource.Play();
        }

        if(Input.GetKeyDown("k") && !choiceDone) 
        {
            choiceDone = true;
            
            StartCoroutine(PlayingSoundAndChangingScene());
        }
    }

    private IEnumerator PlayingSoundAndChangingScene()
    {
        gameOverAudioSource.Play();

        yield return new WaitForSeconds(SOUND_DURATION);

        if (handToMove.position == leftChoice)
        {
            SceneManager.LoadScene("Stage1");
        }
        else
        {
            Application.Quit();
        }
    } 
}
