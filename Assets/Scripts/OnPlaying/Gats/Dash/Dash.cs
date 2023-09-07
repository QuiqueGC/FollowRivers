using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Dash : MonoBehaviour
{
    [SerializeField] private Animator animator;
    Rigidbody2D playerRigidBody;
    const float DASH_DURATION = 0.15f;
    const float DASH_COOLDOWN = 0.75f;
    [SerializeField] private Image dashBar;
    [SerializeField] private Image dashBarBackground;
    [SerializeField] private AudioClip dashSound;
    float dashFullCooldownBar = 100;
    float dashActualCooldownBar;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();

        dashActualCooldownBar = dashFullCooldownBar;

        Gats.canDash = true;
        Gats.isDashing = false;
    }

    // Update is called once per frame
    void Update()
    {
        dashBar.fillAmount = dashActualCooldownBar / dashFullCooldownBar;

        if (Input.GetKeyDown(KeyCode.Space) && Gats.canDash)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(dashSound, 0.5f);

            StartCoroutine(DashCooldownCoroutine());

            StartCoroutine(DashBarCoroutine());

            StartCoroutine(DashCoroutine());


        }
    }

    private IEnumerator DashCooldownCoroutine()
    {
        Gats.canDash = false;

        dashBar.gameObject.SetActive(true);
        dashBarBackground.gameObject.SetActive(true);

        yield return new WaitForSeconds(DASH_COOLDOWN);

        Gats.canDash = true;

        dashBar.gameObject.SetActive(false);
        dashBarBackground.gameObject.SetActive(false);

    }

   private IEnumerator DashBarCoroutine()
    {
        
        for(dashActualCooldownBar = 0; dashActualCooldownBar < dashFullCooldownBar; dashActualCooldownBar += 10)
        {
            yield return new WaitForSeconds(DASH_COOLDOWN / 10);
        }
    }

    private IEnumerator DashCoroutine()
    {
        Gats.isDashing = true;


        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Gats"), LayerMask.NameToLayer("Goblin"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Gats"), LayerMask.NameToLayer("DeadCube"), true);

        ChooseSideToDash();

        playerRigidBody.velocity *= 6;

        yield return new WaitForSeconds(DASH_DURATION);

        gameObject.GetComponent<AudioSource>().Stop();

        TurnOffDashingBools();

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Gats"), LayerMask.NameToLayer("Goblin"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Gats"), LayerMask.NameToLayer("DeadCube"), false);

    }

    private void ChooseSideToDash()
    {
        bool lookingLeft = animator.GetBool("goingLeft") || animator.GetBool("stopLeft");
        bool lookingTop = animator.GetBool("goingTop") || animator.GetBool("stopTop");
        bool lookingRight = animator.GetBool("goingRight") || animator.GetBool("stopRight");
        bool lookingDown = animator.GetBool("goingDown") || animator.GetBool("stopDown");

        if (lookingLeft)
        {
            animator.SetBool("dashLeft", true);
        }
        else if (lookingTop)
        {
            animator.SetBool("dashTop", true);
        }
        else if (lookingRight)
        {
            animator.SetBool("dashRight", true);
        }
        else if (lookingDown)
        {
            animator.SetBool("dashDown", true);
        }
    }

    private void TurnOffDashingBools()
    {
        animator.SetBool("dashLeft", false);
        animator.SetBool("dashTop", false);
        animator.SetBool("dashRight", false);
        animator.SetBool("dashDown", false);
        Gats.isDashing = false;
    }
}
