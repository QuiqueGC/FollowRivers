using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementCube : MonoBehaviour
{
    private float SPEED = 9f;
    [SerializeField] private Transform target;
    public Animator animator;
    private float verticalDistanceFromTarget;
    private float horizontalDistanceFromTarget;
    private bool enemyOnTheRight;
    private bool enemyOnTheLeft;
    private float CUBE_DAMAGE = 100;
    //private float VERTICAL_DISTANCE_TO_WAKE_UP = 0.25f;
    

    // Start is called before the first frame update
    void Start()
    {
        enemyOnTheRight = false;
        enemyOnTheLeft = false;
        gameObject.tag = "Untagged";

    }

    // Update is called once per frame
    void Update()
    {

        verticalDistanceFromTarget = transform.position.y - target.position.y;
        horizontalDistanceFromTarget = transform.position.x - target.position.x;
        

        if (verticalDistanceFromTarget <= 0.25f && verticalDistanceFromTarget >= -0.25f)
        {
           
            CubeWakeUp();

        }

        CubeMovement();

    }

    private void CubeWakeUp()
    {
        if (horizontalDistanceFromTarget < 0)
        {
            enemyOnTheRight = true;
            animator.SetBool("findEnemy", true);

        }
        else if (horizontalDistanceFromTarget > 0)
        {
            enemyOnTheLeft = true;
            animator.SetBool("findEnemy", true);
        }

        gameObject.tag = "cube";
        gameObject.GetComponent<AudioSource>().Play();
    }



    private void CubeMovement()
    {
        if (enemyOnTheRight)
        {

            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1,0).normalized*SPEED;

        }
        else if (enemyOnTheLeft)
        {
           
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0).normalized * SPEED;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.collider.CompareTag("enemy") && animator.GetBool("findEnemy"))
        {
            collision.gameObject.GetComponent<EnemyHP>().GetDamage(CUBE_DAMAGE);

        }
        else if (!collision.collider.CompareTag("gats"))
        {
            StopAttacking();
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        StopAttacking();
    }


    private void StopAttacking()
    {
        enemyOnTheRight = false;
        enemyOnTheLeft = false;
        animator.SetBool("findEnemy", false);
        gameObject.tag = "Untagged";

        gameObject.GetComponent<AudioSource>().Stop();
    }
}
