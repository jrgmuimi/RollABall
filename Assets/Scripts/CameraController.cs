using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    public float yOffset = 20.0f;
    //public float zOffset = 10.0f;

    // Update is called once per frame. Runs after all updates are done
    void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, yOffset, player.transform.position.z /*- zOffset*/);
    }
}
