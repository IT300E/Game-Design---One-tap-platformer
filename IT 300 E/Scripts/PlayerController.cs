using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject gameOverUI; // Reference to the Game Over UI
    public TextMeshProUGUI gameOverText; // Reference to the Game Over Text
    public Button restartButton; // Reference to the Restart Button
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    
    public float moveSpeed = 2f;
   
    public float fallThreshold = -5f;  // Set this value to the Y position where the player falls off the screen
    public LayerMask groundLayer;      // To detect platforms and ground

    private bool isGrounded = false;    // Check if the player is on the ground/platform


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Auto-move player forward
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        // Jump on click or tap
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            rb.linearVelocity = Vector2.up * jumpForce;
        }

        // Check if the player's Y position is below the fall threshold
        if (transform.position.y < fallThreshold)
        {
            GameOver();
        }

    }

   

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player hits the platform or ground, set isGrounded to true
        if (collision.collider.CompareTag("Platform") || collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        // If the player hits a spike, trigger game over
        if (collision.collider.CompareTag("Spike"))
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");

        // Show the Game Over UI
        gameOverUI.SetActive(true);

        // Disable player controls (optional)
        // playerController.enabled = false;

        // Make sure the player cannot move after the game is over

        // Show Game Over text
        gameOverText.text = "Game Over!";

        // Activate Restart button
        restartButton.gameObject.SetActive(true);

        // Add listener for restart button
        restartButton.onClick.AddListener(RestartGame);
    }

    private void RestartGame()
    {
        // Restart the scene (reload the current scene)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
