using UnityEngine;

public class PunchHitBox : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            collision.GetComponent<EnemyHP>().EnemyGetDamage(Gats.punchDamage);
        }
    }
}
