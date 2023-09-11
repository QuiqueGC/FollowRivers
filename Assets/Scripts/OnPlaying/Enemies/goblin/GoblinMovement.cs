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


    void Start()
    {
        goblinRigidBody = GetComponent<Rigidbody2D>();
        canMove = true;
        targetAlreadySeen = false;
        visualRange = Random.Range(goblin.VisualRangeMin, goblin.VisualRangeMax);

    }

    void Update()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        if (target != null && canMove)
        {
            StartMovement();
        }
        else if (target == null)
        {
            StopAllCoroutines();
        }
    }

    private void StartMovement()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= visualRange || targetAlreadySeen)
        {
            targetAlreadySeen = true;

            goblinRigidBody.velocity = (target.position - transform.position).normalized * goblin.Speed;

            AnimatorManagement();
        }
    }

    private void AnimatorManagement()
    {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("punchHitBox"))
        {
            KnockBack();
        }
    }

    private void KnockBack()
    {
        canMove = false;
        goblinRigidBody.velocity = ((transform.position - target.position).normalized * goblin.Speed);
    }
}
