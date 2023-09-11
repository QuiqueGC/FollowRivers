using System.Collections;
using UnityEngine;

public class HeartTakenFX : MonoBehaviour
{
    [SerializeField] private Transform player;
    private AudioSource heartTakenAudioSource;
    private bool used;
    private const float FXDURATION = 0.462f;

    void Start()
    {
        heartTakenAudioSource = GetComponent<AudioSource>();
        used = false;
    }

    void Update()
    {
        if (!used)
        {
            used = true;

            transform.position = player.position;

            StartCoroutine(playingFXAndDestroyingObject());
        }
    }

    IEnumerator playingFXAndDestroyingObject()
    {
        heartTakenAudioSource.Play();

        yield return new WaitForSeconds(FXDURATION);

        Destroy(gameObject);
    }
}
