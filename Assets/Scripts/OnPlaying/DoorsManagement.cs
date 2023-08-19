using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsManagement : MonoBehaviour
{
    //public static DoorsManagement instance;
    [SerializeField] private GameObject player;
    private bool isTouching;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (isTouching && Input.GetKeyDown("l")) 
        {
            Destroy(transform.gameObject);
        }
        
    }

    private void Awake()
    {
        //instance = this;
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
