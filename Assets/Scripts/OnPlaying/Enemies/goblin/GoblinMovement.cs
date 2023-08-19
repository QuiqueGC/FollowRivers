using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMovement : MonoBehaviour
{
    public Enemy goblin;
    [SerializeField] private Transform target;
    private Rigidbody2D goblinRigidBody;
    public Animator animator;
    public bool canMove;
    private bool targetAlreadySeen;
    private float visualRange;



    // Start is called before the first frame update
    void Start()
    {
        goblinRigidBody = GetComponent<Rigidbody2D>();
        canMove = true;
        targetAlreadySeen = false;
        visualRange = Random.Range(goblin.VisualRangeMin, goblin.VisualRangeMax);

    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        if (target != null && canMove)
        {
            if (Vector3.Distance(target.transform.position, transform.position) <= visualRange || targetAlreadySeen)
            {
                targetAlreadySeen = true;

                goblinRigidBody.velocity = (target.position - transform.position).normalized * goblin.Speed;

                animator.SetBool("findEnemy", true);

                if (transform.position.y - target.transform.position.y <= 0)
                {
                    animator.SetBool("goingDown", false);
                }
                else
                {
                    animator.SetBool("goingDown", true);
                }

            }
        }
        else if (target == null)
        {
            StopAllCoroutines();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("punchHitBox"))
        {

           StartCoroutine(KnockBack());

        }
        
      
    }


    private IEnumerator KnockBack()
    {
        
        canMove = false;
        Vector2 velocityToCheck = new Vector2(((target.position - transform.position).normalized * goblin.Speed).x,((target.position - transform.position).normalized * goblin.Speed).y);

        goblinRigidBody.velocity = ((transform.position - target.position).normalized * goblin.Speed) * 1.5f;


        while(goblinRigidBody.velocity != velocityToCheck) {

            velocityToCheck = new Vector2(((target.position - transform.position).normalized * goblin.Speed).x, ((target.position - transform.position).normalized * goblin.Speed).y);

            goblinRigidBody.velocity = Vector2.MoveTowards(goblinRigidBody.velocity,velocityToCheck,Time.deltaTime*12);

            yield return null;
        }

        canMove = true;
    }





}
