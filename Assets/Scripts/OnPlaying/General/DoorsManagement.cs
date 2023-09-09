using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorsManagement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private bool isTouching;
    [SerializeField] private Sprite openDoor;
    private Collider2D doorCollider;
    private SpriteRenderer doorSpriteRenderer;
    private AudioSource doorAudioSource;


    // Start is called before the first frame update
    void Start()
    {
        doorCollider = GetComponent<Collider2D>();
        doorSpriteRenderer = GetComponent<SpriteRenderer>();
        doorAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        OpeningDoors();
    }

    private void OpeningDoors()
    {

        if (isTouching && Input.GetKeyDown("l"))
        {
            OpeningAnyDoors();

            JustOpeningFinalDoors();
        }
    }

    private void OpeningAnyDoors()
    {
        doorCollider.enabled = false;

        doorSpriteRenderer.sprite = openDoor;

        doorAudioSource.Play();
    }

    private void JustOpeningFinalDoors()
    {
        if (gameObject.tag == "finalDoor")
        {
            SceneManager.LoadScene("WinScene");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("gats"))
        {
            isTouching = true;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("gats"))
        {
            isTouching = false;
        }
    }
}
