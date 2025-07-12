using UnityEngine;

public class CapyDash : MonoBehaviour
{
    [Header("Dash Settings")]
    public float dashForceX = 20f; // Fuerza para X
    public float dashForceY = 15f; // Fuerza para Y
    public float dashDuration = 0.2f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Visuals")]
    public SpriteRenderer spriteRenderer;
    public Color dashReadyColor = Color.white;
    public Color dashCooldownColor = Color.gray;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool canDash = true;
    private bool isDashing = false;
    private float dashTimeLeft;
    private Vector2 dashDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded && !canDash && !isDashing)
        {
            canDash = true;
        }

        spriteRenderer.color = canDash ? dashReadyColor : dashCooldownColor;

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartDash();
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            float dashFactor = dashTimeLeft / dashDuration;

            // Aplica fuerza separada para X e Y
            Vector2 dashVelocity = new Vector2(
                dashDirection.x * dashForceX,
                dashDirection.y * dashForceY
            );

            rb.linearVelocity = dashVelocity * dashFactor;

            dashTimeLeft -= Time.fixedDeltaTime;

            if (dashTimeLeft <= 0f)
            {
                EndDash();
            }
        }
    }

    private void StartDash()
    {
        canDash = false;
        isDashing = true;
        dashTimeLeft = dashDuration;

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        dashDirection = new Vector2(moveX, moveY);

        if (dashDirection.magnitude < 0.1f)
        {
            dashDirection = transform.localScale.x >= 0 ? Vector2.right : Vector2.left;
        }

        dashDirection.Normalize();
        rb.gravityScale = 0f;
    }

    private void EndDash()
    {
        isDashing = false;
        rb.gravityScale = 3f;
        rb.linearVelocity = Vector2.zero;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
