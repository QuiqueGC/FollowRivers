using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using UnityEngine;
using UnityEngine.UI;

public class PunchAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform punchHitBox;
    [SerializeField] private Image punchBar;
    [SerializeField] private Image punchBarBackground;
    const float PUNCH_TIMING = 0.2f;
    const float PUNCH_COOLDOWN = 0.5f;
    private float punchActualCooldownBar;
    private float punchMaxCooldownBar = 100;

    // Start is called before the first frame update
    void Start()
    {
        punchActualCooldownBar = punchMaxCooldownBar;

        Gats.canAttack = true;
        Gats.isAttacking = false;
        
    }

    // Update is called once per frame
    void Update()
    {
       
            punchBar.fillAmount = punchActualCooldownBar / punchMaxCooldownBar;
      

        if (Input.GetKeyDown("k") && Gats.canAttack)
        {

            StartCoroutine(PunchCooldownCoroutine());

            StartCoroutine(PunchBarCoroutine());

            StartCoroutine(PunchCoroutine());
        }
    }

    private IEnumerator PunchBarCoroutine()
    {
        for (punchActualCooldownBar = 0; punchActualCooldownBar < punchMaxCooldownBar; punchActualCooldownBar += 10)
        {
            yield return new WaitForSeconds(PUNCH_COOLDOWN / 10);
        }
    }

    private IEnumerator PunchCooldownCoroutine()
    {
        Gats.canAttack = false;

        punchBar.gameObject.SetActive(true);
        punchBarBackground.gameObject.SetActive(true);


        yield return new WaitForSeconds(PUNCH_COOLDOWN);



        Gats.canAttack = true;

        punchBar.gameObject.SetActive(false);
        punchBarBackground.gameObject.SetActive(false);


    }

    private IEnumerator PunchCoroutine()
    {
       
        

        Gats.isAttacking = true;


      

        animator.SetBool("attackArea", true);

        HitBoxActivation(true);

        yield return new WaitForSeconds(PUNCH_TIMING);

        animator.SetBool("attackArea", false);
        Gats.isAttacking = false;

        

        HitBoxActivation(false);

        
    }

   

    private void HitBoxActivation(Boolean activation)
    {
        Animator hitBoxAnimator = punchHitBox.GetComponent<Animator>();

        punchHitBox.gameObject.SetActive(activation);
        hitBoxAnimator.SetBool("isAttacking", activation);
    }

   
}
