using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartBehaviour : MonoBehaviour
{
    [SerializeField] private Transform heartTakenFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("gats"))
        {
            
            if (Gats.lives < 3)
            {
                Gats.lives++;

            }
            else
            {
                Gats.score += 50;
            }

            playingTakingLiveFX();

            Destroy(gameObject);
        }
        if (collision.CompareTag("enemy"))
        {
            playingTakingLiveFX();

            Destroy(gameObject);
        }

    }

    private void playingTakingLiveFX()
    {
        GameObject newHeartTakenFX = Instantiate(heartTakenFX.gameObject);
        newHeartTakenFX.transform.position = transform.position;
        newHeartTakenFX.SetActive(true);
    }
}
