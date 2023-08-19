using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementCube : MonoBehaviour
{
    private float speed = 9f;
    [SerializeField] private Transform target;
    private Rigidbody2D rigidBody;
    //private const int distanceToAttack = 5;
    public Animator animator;
    bool canMove;
    private float paddingAttackY;
    private float paddingAttackX;
    private float DISTANCE_TO_GO = 100;
    private Vector3 targetPosition;
    private bool rightAttack;
    private bool leftAttack;
    

    // Start is called before the first frame update
    void Start()
    {
        rightAttack = false;
        leftAttack = false;
        rigidBody = GetComponent<Rigidbody2D>();
        


        //targetPosition = new Vector3(transform.position.x + DISTANCE_TO_GO, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {

        canMove = leftAttack && rightAttack;

        paddingAttackY = transform.position.y - target.position.y;
        paddingAttackX = transform.position.x - target.position.x;
        


        if (paddingAttackY <= 0.25f && paddingAttackY >= -0.25f)
        {
           

            if (paddingAttackX < 0)
            {
                rightAttack = true;
                animator.SetBool("findEnemy", true);

            }
            else if (paddingAttackX > -0)
            {
                leftAttack = true;
                animator.SetBool("findEnemy", true);
            }

        }
        if (rightAttack)
        {
            targetPosition = new Vector3(transform.position.x + DISTANCE_TO_GO, 0, 0);
            transform.position += targetPosition.normalized * speed * Time.deltaTime;
            
        }
        else if (leftAttack)
        {
            targetPosition = new Vector3(transform.position.x - DISTANCE_TO_GO, 0, 0);
            transform.position += targetPosition.normalized * speed * Time.deltaTime;
            

        }else
        {
            
        }

        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null) 
        {

            rightAttack = false;
            leftAttack = false;
            animator.SetBool("findEnemy", false);

        }

       
    }

    /*private void MoveToPlayer()
    {
        if (target != null && canMove)
        {
            if (Vector3.Distance(target.transform.position, transform.position) < distanceToAttack)
            {

                rigidBody.velocity = (target.position - transform.position).normalized * speed;

                animator.SetBool("findEnemy", true);

            }
            else
            {
                rigidBody.velocity = Vector3.zero;
                animator.SetBool("findEnemy", false);
            }
        } 
        else if (target == null)
        {
           
        }
    }*/


}
