using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    [Header("Tracking")]
    [SerializeField] private string playerName = "Player";
    [SerializeField] private float maxSpeed;
    private Transform playerTransform;
    private Rigidbody2D rigidBody;

    private void Awake()
    {
        // Getting the Player Gameobject
        playerTransform = GameObject.Find(playerName).transform;

        // Getting a reference to the RigidBody 2D Component
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Calculating the direction
        Vector2 direction = playerTransform.position - transform.position;
        direction = direction.normalized;

        // Setting Enemy Velocity in that direction
        rigidBody.linearVelocityX = direction.x * maxSpeed;
        rigidBody.linearVelocityY = direction.y * maxSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}