using UnityEngine;

public class MovementCube : MonoBehaviour
{
    private const float CUBE_SPEED = 9f;
    [SerializeField] private Transform target;
    [SerializeField] private Animator cubeAnimator;
    private AudioSource cubeAudioSource;
    private Rigidbody2D cubeRigidBody;
    private float verticalDistanceFromTarget;
    private float horizontalDistanceFromTarget;
    private bool enemyOnTheRight;
    private bool enemyOnTheLeft;
    private const float CUBE_DAMAGE = 100;
    
    void Start()
    {
        cubeRigidBody = GetComponent<Rigidbody2D>();
        cubeAudioSource = GetComponent<AudioSource>();
        enemyOnTheRight = false;
        enemyOnTheLeft = false;
        gameObject.tag = "Untagged";
    }

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
            cubeAnimator.SetBool("findEnemy", true);
        }
        else if (horizontalDistanceFromTarget > 0)
        {
            enemyOnTheLeft = true;
            cubeAnimator.SetBool("findEnemy", true);
        }

        gameObject.tag = "cube";
        cubeAudioSource.Play();
    }

    private void CubeMovement()
    {
        if (enemyOnTheRight)
        {
            cubeRigidBody.velocity = new Vector2(1,0).normalized*CUBE_SPEED;
        }
        else if (enemyOnTheLeft)
        {
            cubeRigidBody.velocity = new Vector2(-1, 0).normalized * CUBE_SPEED;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.collider.CompareTag("enemy") && cubeAnimator.GetBool("findEnemy"))
        {
            collision.gameObject.GetComponent<EnemyHP>().EnemyGetDamage(CUBE_DAMAGE);
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
        cubeAnimator.SetBool("findEnemy", false);
        gameObject.tag = "Untagged";

        cubeAudioSource.Stop();
    }
}
