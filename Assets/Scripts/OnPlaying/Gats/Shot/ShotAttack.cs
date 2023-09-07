using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ShotAttack : MonoBehaviour
{
    [SerializeField] private Animator gatsAnimator;
    [SerializeField] private Transform shotHitBox;
    [SerializeField] private AudioClip shotSound;
    private Animator shotAnimator;
    float speedShot = 6.5f;
    float shotDuration = 0.19f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("j"))
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(shotSound, 0.25f);

            GameObject newShotHitBox = Instantiate(shotHitBox.gameObject);

            shotAnimator = newShotHitBox.GetComponent<Animator>();

            newShotHitBox.transform.position = new Vector3 (transform.GetComponentInParent<Transform>().position.x, transform.GetComponentInParent<Transform>().position.y, transform.GetComponentInParent<Transform>().position.z);
            
            StartCoroutine(ShotCoroutine(newShotHitBox));
        }
    }

    private IEnumerator ShotCoroutine(GameObject newShotHitBox)
    {
        bool lookingLeft = gatsAnimator.GetBool("goingLeft") || gatsAnimator.GetBool("stopLeft");
        bool lookingTop = gatsAnimator.GetBool("goingTop") || gatsAnimator.GetBool("stopTop");
        bool lookingRight = gatsAnimator.GetBool("goingRight") || gatsAnimator.GetBool("stopRight");
        bool lookingDown = gatsAnimator.GetBool("goingDown") || gatsAnimator.GetBool("stopDown");

        newShotHitBox.SetActive(true);

        if (lookingLeft)
        {
            LeftAttack(newShotHitBox);
        }
        else if (lookingTop)
        {
            TopAttack(newShotHitBox);
        }
        else if (lookingRight)
        {
            RightAttack(newShotHitBox);
        }
        else if (lookingDown)
        {
            BotAttack(newShotHitBox);
        }

        yield return new WaitForSeconds(shotDuration);

       /* if (newShotHitBox != null)
        {
            Destroy(newShotHitBox);
        }*/
        

        gatsAnimator.SetBool("hitingLeft", false);
        gatsAnimator.SetBool("hitingTop", false);
        gatsAnimator.SetBool("hitingRight", false);
        gatsAnimator.SetBool("hitingDown", false);


    }

    private void LeftAttack(GameObject newShotHitBox)
    {
        gatsAnimator.SetBool("hitingLeft", true);

        shotAnimator.SetBool("leftAttack", true);
       

        newShotHitBox.GetComponent<Rigidbody2D>().velocity = new Vector2(shotHitBox.GetComponent<Rigidbody2D>().velocity.x - speedShot, shotHitBox.GetComponent<Rigidbody2D>().velocity.y);
    }

    private void TopAttack(GameObject newShotHitBox)
    {
        gatsAnimator.SetBool("hitingTop", true);

        shotAnimator.SetBool("topAttack", true);

        newShotHitBox.GetComponent<Rigidbody2D>().velocity = new Vector2(shotHitBox.GetComponent<Rigidbody2D>().velocity.x, shotHitBox.GetComponent<Rigidbody2D>().velocity.y + speedShot);

    }

    private void RightAttack(GameObject newShotHitBox)
    {
        gatsAnimator.SetBool("hitingRight", true);

        shotAnimator.SetBool("rightAttack", true);

        newShotHitBox.GetComponent<Rigidbody2D>().velocity = new Vector2(shotHitBox.GetComponent<Rigidbody2D>().velocity.x + speedShot, shotHitBox.GetComponent<Rigidbody2D>().velocity.y);

    }

    private void BotAttack(GameObject newShotHitBox)
    {
        gatsAnimator.SetBool("hitingDown", true);

        shotAnimator.SetBool("downAttack", true);

        newShotHitBox.GetComponent<Rigidbody2D>().velocity = new Vector2(shotHitBox.GetComponent<Rigidbody2D>().velocity.x, shotHitBox.GetComponent<Rigidbody2D>().velocity.y - speedShot);

    }
}
