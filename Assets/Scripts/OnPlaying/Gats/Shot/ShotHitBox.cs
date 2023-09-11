using UnityEngine;

public class ShotHitBox : MonoBehaviour
{
    [SerializeField] private Transform shotFXObject;
    private Collider2D shotHitBoxCollider2D;

    void Start()
    {
        shotHitBoxCollider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            collision.GetComponent<EnemyHP>().GetDamage(Gats.shotDamage);

            callingFXToBrokenShot();

            Destroy(gameObject);
        }
        else if (collision.CompareTag("heart"))
        {
            Physics2D.IgnoreCollision(collision, shotHitBoxCollider2D);
        }
        else if (!collision.CompareTag("gats"))
        {
            callingFXToBrokenShot();

            Destroy(gameObject);
        }
    }

    private void callingFXToBrokenShot()
    {
        GameObject newShotFXObject = Instantiate(shotFXObject.gameObject);
        newShotFXObject.transform.position = transform.position;
        newShotFXObject.SetActive(true);
    }
}
