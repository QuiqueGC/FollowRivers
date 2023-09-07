using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    private float HPMax;
    private float HPActual;
    [SerializeField] private Image HPBar;
    [SerializeField] private Enemy thisEnemy;
    [SerializeField] private Transform heartCollider;
    private float randomLoot;

    public Enemy ThisEnemy { get => thisEnemy; set => thisEnemy = value; }

    // Start is called before the first frame update
    void Start()
    {
        HPMax = ThisEnemy.HP;
        HPActual = HPMax;
        randomLoot = Random.Range(0, 100);
    }

    // Update is called once per frame
    void Update()
    {

        HPBar.fillAmount = HPActual / HPMax;
        
    }

    public void GetDamage (float damage)
    {
        HPActual -= damage;

        if (HPActual <= 0)
        {
            Die();
        }
    }

    private void Die()
    {

        StartCoroutine(DyingAnimation());
    }


    private IEnumerator DyingAnimation()
    {
        gameObject.GetComponent<AudioSource>().Play();

        gameObject.GetComponent<Collider2D>().enabled = false;

        gameObject.GetComponent<Animator>().SetBool("dying", true);

        yield return new WaitForSeconds(0.20f);

        if (randomLoot > 75)
        {
            GameObject heart = Instantiate(heartCollider.gameObject, transform.position, Quaternion.Euler(0, 0, 0));
        }

        Destroy(gameObject);
    }
}
