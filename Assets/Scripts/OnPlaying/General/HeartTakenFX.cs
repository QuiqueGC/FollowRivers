using System.Collections;
using UnityEngine;

public class HeartTakenFX : MonoBehaviour
{
    [SerializeField] private Transform player;
    private AudioSource heartTakenAudioSource;
    private bool used;
    private const float FXDURATION = 0.462f;
    // Start is called before the first frame update
    void Start()
    {
        heartTakenAudioSource = GetComponent<AudioSource>();
        used = false;
    }

    // Update is called once per frame
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
