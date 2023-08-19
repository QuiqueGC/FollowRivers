using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] RectTransform handToMove;
    Vector3 leftChoice = new Vector3(360, 100, 0);
    Vector3 rightChoice = new Vector3(1050, 100, 0);



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a"))
        {
            handToMove.position = leftChoice;
        }
        if (Input.GetKey("d")) 
        {
            handToMove.position = rightChoice;
        }

        if(Input.GetKey("k")) 
        {
            SceneManager.LoadScene("MainTitle");
        }
        
    }
}
