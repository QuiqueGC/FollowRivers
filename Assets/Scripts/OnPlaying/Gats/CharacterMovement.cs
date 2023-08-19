using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    public Animator animator;
    Rigidbody2D playerRigidBody;
    private Color color;
    private bool invulnerable;
    [SerializeField] Canvas UI;
    private string actualHP;
    List<Collider2D> enemiesColliders = new List<Collider2D>();

    // Start is called before the first frame update
    void Start()
    {  
        playerRigidBody = GetComponent<Rigidbody2D>();
        color = transform.GetComponent<SpriteRenderer>().color;
        invulnerable = false;
        Gats.lives = 3;
        

    }


    // Update is called once per frame
    void Update()
    {
        RegularMovement();

        ManageAnimations();

        actualHP = "Vidas: " + Gats.lives.ToString();

        UI.GetComponentInChildren<TextMeshProUGUI>().text = actualHP;

        //hay que ver si con un try-catch deja de darme problemas (no sé si es con el remove o sólo al hacer
        //el paseo por el foreach
        foreach (Collider2D c in enemiesColliders)
        {
            if (Physics2D.IsTouching(GetComponent<Collider2D>(), c))
            {
                if (Gats.isDashing)
                {
                    
                    StartCoroutine(DashingInvulnerability(c));

                }
                else
                {
                    SufferingDamage();
                }

            }

        }

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

    void LeftAnimations()
    {
         if (Input.GetKey("a") && !animator.GetBool("goingTop") && !animator.GetBool("goingDown"))
        {
            animator.SetBool("goingLeft", true);
            UpdateStopBoolsToFalse();
        }
        if (Input.GetKeyUp("a"))
        {
             animator.SetBool("goingLeft", false);
             animator.SetBool("stopLeft", true);
        }
    }

    void RightAnimations()
    {
        if (Input.GetKey("d") && !animator.GetBool("goingTop") && !animator.GetBool("goingDown"))
        {
            animator.SetBool("goingRight", true);
            UpdateStopBoolsToFalse();
        }
        if (Input.GetKeyUp("d"))
        {
            animator.SetBool("goingRight", false);
            animator.SetBool("stopRight", true);
        }
    }
    
    void TopAnimations()
    {
        if (Input.GetKey("w") && !animator.GetBool("goingRight") && !animator.GetBool("goingLeft"))
        {
            animator.SetBool("goingTop", true);
            UpdateStopBoolsToFalse();
        }
        if (Input.GetKeyUp("w"))
        {
            animator.SetBool("goingTop", false);
            animator.SetBool("stopTop", true);
        }
    }

    void DownAnimations()
    {
        if (Input.GetKey("s") && !animator.GetBool("goingRight") && !animator.GetBool("goingLeft"))
        {
            animator.SetBool("goingDown", true);
            UpdateStopBoolsToFalse();
        }
        if (Input.GetKeyUp("s"))
        {
            animator.SetBool("goingDown", false);
            animator.SetBool("stopDown", true);
        }
    }

    void UpdateStopBoolsToFalse()
    {
        animator.SetBool("stopLeft", false);
        animator.SetBool("stopRight", false);
        animator.SetBool("stopTop", false);
        animator.SetBool("stopDown", false);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        

        if (collision.collider.CompareTag("enemy") && !Gats.isDashing) 
        {
            enemiesColliders.Add(collision.collider);

            SufferingDamage();

        }
        else if(collision.collider.CompareTag("enemy") && Gats.isDashing)
        {
            enemiesColliders.Add(collision.collider);

            Collision2D enemy = collision;
            //casi lo tengo, pero es una puta mierda
            StartCoroutine(DashingInvulnerability(enemy.collider));
            

        }
        if (collision.collider.CompareTag("cube"))
        {
            SufferingDamage();
        }
    }
    

    private void SufferingDamage()
    {
        if(!invulnerable)
        {
            Gats.lives--;

            StartCoroutine(InvulnerabilityForDamageRecieved());
        }
        

        if (Gats.lives <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private IEnumerator InvulnerabilityForDamageRecieved()
    {
        invulnerable = true;
        transform.GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(3);

        invulnerable = false;
        transform.GetComponent<SpriteRenderer>().color = color;


    }
    

    private IEnumerator DashingInvulnerability(Collider2D enemy)
    {
        
        float invulnerabilityTimeDuringDash = 0.15f;

        Physics2D.IgnoreCollision(enemy, GetComponent<Collider2D>(), true);

        yield return new WaitForSeconds(invulnerabilityTimeDuringDash);

        Physics2D.IgnoreCollision(enemy, GetComponent<Collider2D>(), false);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("enemy"))
        {
            enemiesColliders.Remove(collision.collider);
        }
    }
}
