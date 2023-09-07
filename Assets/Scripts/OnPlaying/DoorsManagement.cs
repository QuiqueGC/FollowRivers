using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorsManagement : MonoBehaviour
{
    //public static DoorsManagement instance;
    [SerializeField] private GameObject player;
    private bool isTouching;
    [SerializeField] private Sprite openDoor;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        OpeningDoors();
        
    }

    private void OpeningDoors()
    {

        if (isTouching && Input.GetKeyDown("l"))
        {
            transform.gameObject.GetComponent<Collider2D>().enabled = false;

            transform.gameObject.GetComponent<SpriteRenderer>().sprite = openDoor;

            gameObject.GetComponent<AudioSource>().Play();


            if(gameObject.tag == "finalDoor")
            {
                SceneManager.LoadScene("WinScene");
            }
            
        }
    }

   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("gats"))
        {
            isTouching = true;
        }
    }

    

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("gats"))
        {
            isTouching = false;
        }
    }
}
