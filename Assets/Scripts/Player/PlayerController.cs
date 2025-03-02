using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D playerBody;
    [SerializeField] private Transform gunTransform;
    [SerializeField] private Transform gunPoint;
    [SerializeField] private GameObject bulletPrefab;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 1500f;
    [SerializeField] private float jumpHeight = 75f;
    [SerializeField] private Vector2 groundedBoxSize;
    [SerializeField] private LayerMask groundLayer;
    private bool _isGrounded;

    [Header("Shooting")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Vector2 gunPositionOffset;

    [Header("Input")]
    private float _inputVector;
    private bool _jumpPressed;

    private void Start()
    { 
        // Getting Player Rigidbody 2D
        playerBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Getting Player Input
        _inputVector = Input.GetAxis("Horizontal");

        // Checking if Player is still on the ground
        _isGrounded = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - 0.5f), groundedBoxSize, 0f, groundLayer);

        // Verticle Movement Based on Space-Bar [Velocity = SquareRoot(2 * gravity * Height)]
        _jumpPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        if (_jumpPressed && _isGrounded)
            playerBody.linearVelocityY = Mathf.Sqrt(2 * Mathf.Abs(Physics2D.gravity.y) * jumpHeight);

        // Updating Gun Rotation to point toward the Mouse Cursor
        if (gunPoint != null && gunTransform != null)
            UpdateGunRotation(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        // Shoot Bullet on Mouse-Click
        if (Input.GetMouseButtonDown(0) && bulletPrefab != null)
            ShootBullet();
    }
    private void FixedUpdate()
    {
        // Horizontal Movement Based on W-A-S-D Keys or Arrow Keys
        playerBody.linearVelocityX = _inputVector * moveSpeed * Time.deltaTime;
    }

    private void UpdateGunRotation(Vector2 mousePosition)
    {
        // Setting Gun Position Relative to Player Square
        gunTransform.position = new Vector2(transform.position.x + gunPositionOffset.x, transform.position.y + gunPositionOffset.y);

        // Calculating the Direction Between the Gun and Mouse Position [Target Position - Initial Position]
        Vector2 direction = mousePosition - new Vector2(gunTransform.position.x, gunTransform.position.y);

        // Calculating the Rotation [Atan2() outputs in radians]
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Applying the Rotation [Quaternions are just 4D Vectors which represent rotations of 3D Objects efficiently]
        gunTransform.rotation = Quaternion.Euler(new Vector3(gunTransform.rotation.x, gunTransform.rotation.y, rotation));
    }

    private void ShootBullet()
    {
        // Adding a Extra Rotation Offset
        Quaternion rotationOffset = Quaternion.Euler(0f, 0f, -90f);

        // Instantiate the bullet and apply the rotation offset
        GameObject bullet = GameObject.Instantiate(bulletPrefab, gunPoint.position, gunTransform.rotation * rotationOffset);

        // Moving the Bullet with Set-Velocity
        Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D>();
        bulletBody.linearVelocityX = bullet.transform.up.x * bulletSpeed;
        bulletBody.linearVelocityY = bullet.transform.up.y * bulletSpeed;
    }
}