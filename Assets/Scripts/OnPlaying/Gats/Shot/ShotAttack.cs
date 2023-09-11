using System.Collections;
using UnityEngine;

public class ShotAttack : MonoBehaviour
{
    [SerializeField] private Animator gatsAnimator;
    [SerializeField] private Transform shotHitBox;
    [SerializeField] private AudioClip shotSound;
    private AudioSource playerAudioSource;
    private Animator shotAnimator;

    void Start()
    {
        playerAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown("j"))
        {
            playerAudioSource.PlayOneShot(shotSound, 0.25f);

            GameObject newShotHitBox = ShotCreation();

            StartCoroutine(ShotCoroutine(newShotHitBox));
        }
    }

    private GameObject ShotCreation()
    {
        float playerPositionX = transform.GetComponentInParent<Transform>().position.x;
        float playerPositionY = transform.GetComponentInParent<Transform>().position.y;

        GameObject newShotHitBox = Instantiate(shotHitBox.gameObject);

        shotAnimator = newShotHitBox.GetComponent<Animator>();

        newShotHitBox.transform.position = new Vector3(playerPositionX, playerPositionY, 0);

        return newShotHitBox;
    }

    private IEnumerator ShotCoroutine(GameObject newShotHitBox)
    {
        bool lookingLeft = gatsAnimator.GetBool("goingLeft") || gatsAnimator.GetBool("stopLeft");
        bool lookingTop = gatsAnimator.GetBool("goingTop") || gatsAnimator.GetBool("stopTop");
        bool lookingRight = gatsAnimator.GetBool("goingRight") || gatsAnimator.GetBool("stopRight");
        bool lookingDown = gatsAnimator.GetBool("goingDown") || gatsAnimator.GetBool("stopDown");

        const float SPEED_SHOT = 6.5f;
        const float SHOT_DURATION = 0.19f;

        newShotHitBox.SetActive(true);

        if (lookingLeft)
        {
            LeftAttack(newShotHitBox, SPEED_SHOT);
        }
        else if (lookingTop)
        {
            TopAttack(newShotHitBox, SPEED_SHOT);
        }
        else if (lookingRight)
        {
            RightAttack(newShotHitBox, SPEED_SHOT);
        }
        else if (lookingDown)
        {
            BotAttack(newShotHitBox, SPEED_SHOT);
        }

        yield return new WaitForSeconds(SHOT_DURATION);

        gatsAnimator.SetBool("hitingLeft", false);
        gatsAnimator.SetBool("hitingTop", false);
        gatsAnimator.SetBool("hitingRight", false);
        gatsAnimator.SetBool("hitingDown", false);
    }

    private void LeftAttack(GameObject newShotHitBox, float SPEED_SHOT)
    {
        Rigidbody2D newShotHitBoxRigidBody2D = newShotHitBox.GetComponent<Rigidbody2D>();
        gatsAnimator.SetBool("hitingLeft", true);
        shotAnimator.SetBool("leftAttack", true);

        newShotHitBoxRigidBody2D.velocity = new Vector2(-SPEED_SHOT, 0);
    }

    private void TopAttack(GameObject newShotHitBox, float SPEED_SHOT)
    {
        Rigidbody2D newShotHitBoxRigidBody2D = newShotHitBox.GetComponent<Rigidbody2D>();

        gatsAnimator.SetBool("hitingTop", true);
        shotAnimator.SetBool("topAttack", true);

        newShotHitBoxRigidBody2D.velocity = new Vector2(0, SPEED_SHOT);

    }

    private void RightAttack(GameObject newShotHitBox, float SPEED_SHOT)
    {
        Rigidbody2D newShotHitBoxRigidBody2D = newShotHitBox.GetComponent<Rigidbody2D>();

        gatsAnimator.SetBool("hitingRight", true);
        shotAnimator.SetBool("rightAttack", true);

        newShotHitBoxRigidBody2D.velocity = new Vector2(SPEED_SHOT, 0);

    }

    private void BotAttack(GameObject newShotHitBox, float SPEED_SHOT)
    {
        Rigidbody2D newShotHitBoxRigidBody2D = newShotHitBox.GetComponent<Rigidbody2D>();

        gatsAnimator.SetBool("hitingDown", true);
        shotAnimator.SetBool("downAttack", true);

        newShotHitBoxRigidBody2D.velocity = new Vector2(0, -SPEED_SHOT);

    }
}
