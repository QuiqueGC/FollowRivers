using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] RectTransform handToMove;
    Vector3 leftChoice = new Vector3(360, 100, 0);
    Vector3 rightChoice = new Vector3(1050, 100, 0);
    private bool choiceDone;



    // Start is called before the first frame update
    void Start()
    {
        choiceDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a") && !choiceDone)
        {
            handToMove.position = leftChoice;

            handToMove.gameObject.GetComponent<AudioSource>().Play();
        }
        if (Input.GetKeyDown("d") && !choiceDone) 
        {
            handToMove.position = rightChoice;

            handToMove.gameObject.GetComponent<AudioSource>().Play();
        }

        if(Input.GetKeyDown("k") && !choiceDone) 
        {
            choiceDone = true;
            
            StartCoroutine(PlayingSoundAndChangingScene());
            
        }
        
    }

    private IEnumerator PlayingSoundAndChangingScene()
    {
        gameObject.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(0.289f);

        if (handToMove.position == leftChoice)
        {
            SceneManager.LoadScene("Stage1");
        }
        else
        {
            SceneManager.LoadScene("MainTitle");
        }

    } 
}
