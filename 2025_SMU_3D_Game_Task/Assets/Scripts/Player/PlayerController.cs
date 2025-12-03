using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float mouseSensitivity = 200f;

    [SerializeField] private float dashDistance = 7f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;
    private Vector3 dashDirection;

    private Rigidbody rb;
    private float rotationX = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;   

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        LookAround();

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            TryDash();
        }

        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z).normalized * moveSpeed;

        if (isDashing)
        {
            move += dashDirection * (dashDistance / dashDuration);

            dashTimer -= Time.fixedDeltaTime;
            if (dashTimer <= 0f)
            {
                isDashing = false;
            }
        }

        rb.MovePosition(rb.position + move * Time.fixedDeltaTime);
    }

    private void TryDash()
    {
        if (isDashing) return;        
        if (dashCooldownTimer > 0f) return;

        StartDash();
    }

    private void StartDash()
    {
        Debug.Log("Dash!");
        isDashing = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;
        dashDirection = transform.forward;
    }

    private void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
