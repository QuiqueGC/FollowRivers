using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D playerRigidBody;
    private AudioSource playerAudioSource;
    private SpriteRenderer playerSpriteRenderer;
    private Collider2D playerCollider;
    private Color originalColor;
    private bool invulnerable;
    [SerializeField] AudioClip damageReceived;
    List<Collider2D> enemiesColliders;

    // Start is called before the first frame update
    void Start()
    {
        enemiesColliders = new List<Collider2D>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAudioSource = GetComponent<AudioSource>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
        originalColor = playerSpriteRenderer.color;
        invulnerable = false;
    }

    // Update is called once per frame
    void Update()
    {
        RegularMovement();

        ManageAnimations();

        CheckIfEnemiesTouching();
    }

    private void RegularMovement()
    {
        if (!Gats.isDashing && !Gats.isAttacking)
        {
            playerRigidBody.velocity = new Vector2(
                Input.GetKey("a") ? -Gats.speed : Input.GetKey("d") ? Gats.speed : 0,
                Input.GetKey("s") ? -Gats.speed : Input.GetKey("w") ? Gats.speed : 0);
        }
    }

    private void ManageAnimations()
    {
        LeftAnimations();

        RightAnimations();

        TopAnimations();

        DownAnimations();
    }

    private void LeftAnimations()
    {
         if (Input.GetKey("a") && !animator.GetBool("goingTop") && !animator.GetBool("goingDown"))
        {
            animator.SetBool("goingLeft", true);
            TurnStopBoolsToFalse();
        }
        if (Input.GetKeyUp("a"))
        {
             animator.SetBool("goingLeft", false);
             animator.SetBool("stopLeft", true);
        }
    }

    private void RightAnimations()
    {
        if (Input.GetKey("d") && !animator.GetBool("goingTop") && !animator.GetBool("goingDown"))
        {
            animator.SetBool("goingRight", true);
            TurnStopBoolsToFalse();
        }
        if (Input.GetKeyUp("d"))
        {
            animator.SetBool("goingRight", false);
            animator.SetBool("stopRight", true);
        }
    }
    
    private void TopAnimations()
    {
        if (Input.GetKey("w") && !animator.GetBool("goingRight") && !animator.GetBool("goingLeft"))
        {
            animator.SetBool("goingTop", true);
            TurnStopBoolsToFalse();
        }
        if (Input.GetKeyUp("w"))
        {
            animator.SetBool("goingTop", false);
            animator.SetBool("stopTop", true);
        }
    }

    private void DownAnimations()
    {
        if (Input.GetKey("s") && !animator.GetBool("goingRight") && !animator.GetBool("goingLeft"))
        {
            animator.SetBool("goingDown", true);
            TurnStopBoolsToFalse();
        }
        if (Input.GetKeyUp("s"))
        {
            animator.SetBool("goingDown", false);
            animator.SetBool("stopDown", true);
        }
    }

    private void TurnStopBoolsToFalse()
    {
        animator.SetBool("stopLeft", false);
        animator.SetBool("stopRight", false);
        animator.SetBool("stopTop", false);
        animator.SetBool("stopDown", false);
    }

    private void CheckIfEnemiesTouching()
    {
        foreach (Collider2D c in enemiesColliders)
        {
            try
            {
                if (Physics2D.IsTouching(playerCollider, c))
                {
                    ReceiveAttack();
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("enemy") && !Gats.isDashing) 
        {
            enemiesColliders.Add(collision.collider);

            ReceiveAttack();

        }
        else if(collision.collider.CompareTag("enemy") && Gats.isDashing)
        {
            enemiesColliders.Add(collision.collider);

        }
        else if (collision.collider.CompareTag("cube"))
        {
            ReceiveAttack();
        }
    }
    
    private void ReceiveAttack()
    {
        if(!invulnerable)
        {
            SufferingDamage();
            
            StartCoroutine(InvulnerabilityForDamageRecieved());
        }

        if (Gats.lives <= 0)
        {
            LostGame();
        }
    }

    private void SufferingDamage()
    {
        playerAudioSource.PlayOneShot(damageReceived, 0.75f);

        Gats.lives--;
    }

    private IEnumerator InvulnerabilityForDamageRecieved()
    {
        invulnerable = true;
        playerSpriteRenderer.color = Color.red;

        yield return new WaitForSeconds(3);

        invulnerable = false;
        playerSpriteRenderer.color = originalColor;
    }

    private void LostGame()
    {
        SceneManager.LoadScene("GameOver");
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("enemy"))
        {
            enemiesColliders.Remove(collision.collider);
        }
    }

    
}
