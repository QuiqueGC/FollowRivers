using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class ShotHitBox : MonoBehaviour
{
    [SerializeField] private Transform shotFXObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            collision.GetComponent<EnemyHP>().GetDamage(Gats.shotDamage);

            callingFXToBrokenShot();

            Destroy(gameObject);

        }else if (collision.CompareTag("heart"))
        {

            Physics2D.IgnoreCollision(collision, gameObject.GetComponent<Collider2D>());
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
