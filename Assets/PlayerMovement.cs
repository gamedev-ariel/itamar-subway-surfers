using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 10f;
    public float laneDistance = 3f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private int currentLane = 1; // 0 = Left, 1 = Center, 2 = Right

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Move forward
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // Handle lateral movement
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 0)
        {
            currentLane--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < 2)
        {
            currentLane++;
        }

        // Calculate target X position
        float targetX = (currentLane - 1) * laneDistance;
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

        // Smoothly move to the target lane
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10);

        // Handle jump
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.linearVelocity.y) < 0.1f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            Time.timeScale = 0; // Pause the game
        }
    }

}