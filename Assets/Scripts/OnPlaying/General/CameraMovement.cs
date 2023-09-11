using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
 
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.position.x, player.position.y, -10);
        }
    }
}
