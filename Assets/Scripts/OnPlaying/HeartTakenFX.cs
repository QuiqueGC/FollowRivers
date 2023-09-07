using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartTakenFX : MonoBehaviour
{
    [SerializeField] private Transform player;
    private bool used;
    // Start is called before the first frame update
    void Start()
    {
        used = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;

        if (!used)
        {
            used = true;

            gameObject.GetComponent<AudioSource>().Play();

            StartCoroutine(playingFXAndDestroyingObject());
        }
    }
    IEnumerator playingFXAndDestroyingObject()
    {


        yield return new WaitForSeconds(0.462f);

        Destroy(gameObject);
    }
}
