using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementCube : MonoBehaviour
{
    private float speed = 9f;
    [SerializeField] private Transform target;
    public Animator animator;
    private float paddingAttackY;
    private float paddingAttackX;
    private float DISTANCE_TO_GO = 100;
    private Vector3 positionToGo;
    private bool rightAttack;
    private bool leftAttack;
    private float cubeDamage = 100;
    

    // Start is called before the first frame update
    void Start()
    {
        rightAttack = false;
        leftAttack = false;
       
        


        //targetPosition = new Vector3(transform.position.x + DISTANCE_TO_GO, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {


        paddingAttackY = transform.position.y - target.position.y;
        paddingAttackX = transform.position.x - target.position.x;
        


        if (paddingAttackY <= 0.25f && paddingAttackY >= -0.25f)
        {
           

            if (paddingAttackX < 0)
            {
                rightAttack = true;
                animator.SetBool("findEnemy", true);

            }
            else if (paddingAttackX > 0)
            {
                leftAttack = true;
                animator.SetBool("findEnemy", true);
            }

        }
        if (rightAttack)
        {
            positionToGo = new Vector3(transform.position.x + DISTANCE_TO_GO, 0, 0);
            transform.position += positionToGo.normalized * speed * Time.deltaTime;

        }
        else if (leftAttack)
        {
            positionToGo = new Vector3(transform.position.x - DISTANCE_TO_GO, 0, 0);
            transform.position += positionToGo.normalized * speed * Time.deltaTime;

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
            


        if (collision.collider.CompareTag("enemy") && animator.GetBool("findEnemy"))
        {
            collision.gameObject.GetComponent<EnemyHP>().GetDamage(cubeDamage);

        }
        else
        {
            rightAttack = false;
            leftAttack = false;
            animator.SetBool("findEnemy", false);
        }


       
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        rightAttack = false;
        leftAttack = false;
        animator.SetBool("findEnemy", false);
    }


}
