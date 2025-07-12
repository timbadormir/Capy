using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        // Mover el objeto
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);

        // Enviar velocidad al Blend Tree (valor absoluto para animaci�n)
        animator.SetFloat("xVelocity", Mathf.Abs(moveX));

        // Flip sprite seg�n direcci�n
        if (moveX > 0)
            spriteRenderer.flipX = true;
        else if (moveX < 0)
            spriteRenderer.flipX = false;
    }
}
