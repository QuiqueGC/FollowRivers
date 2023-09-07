using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainTitle : MonoBehaviour
{
    [SerializeField] GameObject pauseUI;
    private bool choiceDone;
    private bool firstKeyDown;

    // Start is called before the first frame update
    void Start()
    {
        
        choiceDone = false;
        firstKeyDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && firstKeyDown)
        {
            firstKeyDown = false;
            pauseUI.SetActive(true);
            gameObject.GetComponent<AudioSource>().Play();

        }
        if (Input.GetKeyDown(KeyCode.Escape) && !firstKeyDown && !choiceDone)
        {
            choiceDone = true;
            StartCoroutine(callingFXPreviousToStart());
        }
        else if (Input.GetKeyDown("z") && !firstKeyDown && !choiceDone)
        {
            choiceDone = true;

            Application.Quit();

        }

       
    }

    private IEnumerator callingFXPreviousToStart()
    {
        gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.289f);

        SceneManager.LoadScene("Stage1");
    }
}
