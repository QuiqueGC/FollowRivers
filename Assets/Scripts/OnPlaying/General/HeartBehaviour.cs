using UnityEngine;

public class heartBehaviour : MonoBehaviour
{
    [SerializeField] private Transform heartTakenFX;
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
        if (collision.CompareTag("gats"))
        {
            TakingLivesOrScore();

            PlayingTakingLiveFX();

            Destroy(gameObject);
        }
        if (collision.CompareTag("enemy"))
        {
            PlayingTakingLiveFX();

            Destroy(gameObject);
        }

    }

    private void TakingLivesOrScore()
    {
        if (Gats.lives < 3)
        {
            Gats.lives++;

        }
        else
        {
            Gats.score += 50;
        }
    }

    private void PlayingTakingLiveFX()
    {
        GameObject newHeartTakenFX = Instantiate(heartTakenFX.gameObject);
        newHeartTakenFX.transform.position = transform.position;
        newHeartTakenFX.SetActive(true);
    }
}
