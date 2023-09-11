using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PunchAttack : MonoBehaviour
{
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private Transform punchHitBox;
    [SerializeField] private Image punchBar;
    [SerializeField] private Image punchBarBackground;
    [SerializeField] private AudioClip hitSound;
    private Animator hitBoxAnimator;
    private AudioSource playerAudioSource;
    const float PUNCH_TIMING = 0.2f;
    const float PUNCH_COOLDOWN = 0.5f;
    private float punchActualCooldownBar;
    private float punchMaxCooldownBar = 100;

    // Start is called before the first frame update
    void Start()
    {
        hitBoxAnimator = punchHitBox.GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
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
            playerAudioSource.PlayOneShot(hitSound, 0.5f);

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

        characterAnimator.SetBool("attackArea", true);

        HitBoxActivation(true);

        yield return new WaitForSeconds(PUNCH_TIMING);

        characterAnimator.SetBool("attackArea", false);
        Gats.isAttacking = false;

        gameObject.GetComponent<AudioSource>().Stop();

        HitBoxActivation(false);
    }

   

    private void HitBoxActivation(Boolean activation)
    {
        punchHitBox.gameObject.SetActive(activation);
        hitBoxAnimator.SetBool("isAttacking", activation);
    }

   
}
