using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartBehaviour : MonoBehaviour
{
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

            Destroy(gameObject);
        }
        if (collision.CompareTag("enemy"))
        {
            Destroy(gameObject);
        }

    }
}
