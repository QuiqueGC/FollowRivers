using System.Collections;
using UnityEngine;

public class ShotImpactFX : MonoBehaviour
{
    [SerializeField] private Transform player;
    private bool used;
    private AudioSource shotImpactAudioSource;
    private const float SOUND_DURATION = 0.115f;

    void Start()
    {
        shotImpactAudioSource = GetComponent<AudioSource>();
        used = false;
    }

    void Update()
    {
        transform.position = player.position;

        if (!used) 
        {
            used = true;

            shotImpactAudioSource.Play();

            StartCoroutine(playingFXAndDestroyingObject());
        }
    }

    IEnumerator playingFXAndDestroyingObject()
    {
        yield return new WaitForSeconds(SOUND_DURATION);

        Destroy(gameObject);
    }
}
