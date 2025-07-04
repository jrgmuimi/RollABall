using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.AI.Navigation;

public class PlayerController : MonoBehaviour
{

    public float speed = 10.0f;

    public TextMeshProUGUI countText;
    public SpawnPickup spawnScript;
    public CameraController cameraScript;
    public static int winCount = 0;

    private Rigidbody rb;
    private int count = 0;
    private int totalCount;
    private float movementX;
    private float movementZ;
    private Vector2 scrollInput;
    private TrailRenderer trail;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        totalCount = spawnScript.spawnCountPickup;
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
        trail.emitting = false;

        SetCountText();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementZ = movementVector.y;
    }

    void OnScrollWheel(InputValue context)
    {
        scrollInput = context.Get<Vector2>();

        if (scrollInput.y > 0) { cameraScript.yOffset -= 1.0f; }
        else if (scrollInput.y < 0) { cameraScript.yOffset += 1.0f; }
    }

    void OnInteract(InputValue value)
    {
        count = totalCount;
        SetCountText();
    }

    void OnSprint(InputValue value)
    {
        if(value.isPressed)
        {
            speed = 20.0f;
            trail.emitting = true;
        }
        else
        {
            speed = 10.0f;
            trail.emitting = false;
        }
    }

    void SetCountText()
    {
        countText.text = "Collected: " + count.ToString() + " out of " + totalCount.ToString();
        if(count >= totalCount)
        {
            winCount += 1;
            SceneManager.LoadScene("Minigame");
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementZ);
        rb.AddForce(movement*speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count += 1;

            SetCountText();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            winCount = 0;
            SceneManager.LoadScene("Minigame");
        }
    }
}
