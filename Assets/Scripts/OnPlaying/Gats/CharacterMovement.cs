using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CharacterMovement : MonoBehaviour
{
    public Animator animator;
    Rigidbody2D playerRigidBody;
    private Color color;
    private bool invulnerable;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] AudioClip damageReceived;
    private string actualHP;
    private string actualScore;
    List<Collider2D> enemiesColliders = new List<Collider2D>();

    // Start is called before the first frame update
    void Start()
    {  
        playerRigidBody = GetComponent<Rigidbody2D>();
        color = transform.GetComponent<SpriteRenderer>().color;
        invulnerable = false;
        
        

    }


    // Update is called once per frame
    void Update()
    {
        RegularMovement();

        ManageAnimations();

        actualHP = "Vidas: " + Gats.lives.ToString();

        livesText.text = actualHP;

        actualScore = "Comida para el invierno: " + Gats.score.ToString();

        scoreText.text = actualScore;

        
        foreach (Collider2D c in enemiesColliders)
        {
            try {
                if (Physics2D.IsTouching(GetComponent<Collider2D>(), c))
                {
                    SufferingDamage();
                }
            }catch (Exception e)
            {
                Debug.Log(e);
                Debug.Log("algo falla");
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
            gameObject.GetComponent<AudioSource>().PlayOneShot(damageReceived, 0.75f);
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
    
    

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("enemy"))
        {
            enemiesColliders.Remove(collision.collider);
        }
    }

    
}
