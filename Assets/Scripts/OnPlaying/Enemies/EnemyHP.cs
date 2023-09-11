using System.Collections;
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

    void Start()
    {
        HPMax = ThisEnemy.HP;
        HPActual = HPMax;
        randomLoot = Random.Range(0, 100);
    }

    void Update()
    {
        HPBar.fillAmount = HPActual / HPMax;
    }

    public void EnemyGetDamage (float damage)
    {
        HPActual -= damage;

        if (HPActual <= 0)
        {
            EnemyDie();
        }
    }

    private void EnemyDie()
    {
        Gats.score += 10;

        StartCoroutine(DyingAnimation());
    }

    private IEnumerator DyingAnimation()
    {
        AudioSource enemyAudioSource = GetComponent<AudioSource>();
        Collider2D enemyCollider = GetComponent<Collider2D>();
        Animator enemyAnimator = GetComponent<Animator>();
        const float SECONDS_TO_FINISH_ANIMATION = 0.20f;

        enemyAudioSource.Play();

        enemyCollider.enabled = false;

        enemyAnimator.SetBool("dying", true);

        yield return new WaitForSeconds(SECONDS_TO_FINISH_ANIMATION);

        if (randomLoot > 75)
        {
            GameObject heart = Instantiate(heartCollider.gameObject, transform.position, Quaternion.Euler(0, 0, 0));
        }

        Destroy(gameObject);
    }
}
