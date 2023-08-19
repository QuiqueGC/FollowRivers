using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PunchHitBox : MonoBehaviour
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
        if (collision.CompareTag("enemy"))
        {
            collision.GetComponent<EnemyHP>().GetDamage(Gats.punchDamage);

            //Rigidbody2D enemyRigidBody = collision.GetComponent<Rigidbody2D>();
        }
    }

   
}
