using UnityEngine;

public class EnemigoPerseguidor : MonoBehaviour
{
    [Header("Persecución")]
    public Transform jugador;
    public float velocidad = 2f;
    public float rangoDeteccion = 10f;

    [Header("Salto")]
    public float fuerzaSalto = 10f;
    public float tiempoEntreSaltos = 2f;
    public LayerMask capaSuelo;
    public float distanciaSuelo = 0.4f;

    [Header("Daño")]
    public int dañoPorContacto = 1;
    public GameObject hud;

    private Rigidbody2D rb;
    private float temporizadorSalto = 0f;
    private Transform pies;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        temporizadorSalto = tiempoEntreSaltos;

        if (jugador == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                jugador = playerObj.transform;
            else
                Debug.LogWarning("⚠️ No se encontró un objeto con la etiqueta 'Player'.");
        }

        pies = transform.Find("Pies") ?? transform;
    }

    void FixedUpdate()
    {
        if (jugador == null) return;

        float distancia = Vector2.Distance(transform.position, jugador.position);

        if (distancia < rangoDeteccion)
        {
            // Dirección y movimiento
            Vector2 direccion = (jugador.position - transform.position).normalized;
            Vector2 nuevaPos = new Vector2(
                transform.position.x + direccion.x * velocidad * Time.fixedDeltaTime,
                transform.position.y
            );

            rb.MovePosition(nuevaPos);

            // 🔹 Animación de movimiento (Idle si está quieto, Walk si se mueve)
            animator.SetFloat("Movimiento", Mathf.Abs(direccion.x * velocidad));

            // Girar sprite
            if (direccion.x > 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else if (direccion.x < 0)
                transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            // 🔹 Si no está persiguiendo, está idle
            animator.SetFloat("Movimiento", 0);
        }

        // Contador de salto
        if (distancia < rangoDeteccion)
        {
            temporizadorSalto -= Time.fixedDeltaTime;
            if (temporizadorSalto <= 0f && EstaEnElSuelo())
            {
                Saltar();
                temporizadorSalto = tiempoEntreSaltos;
            }
        }
    }

    void Saltar()
    {
        rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        animator.SetTrigger("Saltar"); // 🔹 Animación de salto
        Debug.Log("🟢 El enemigo saltó!");
    }

    bool EstaEnElSuelo()
    {
        return Physics2D.OverlapCircle(pies.position, distanciaSuelo, capaSuelo);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (hud != null)
            {
                PlayerHealth salud = hud.GetComponent<PlayerHealth>();
                if (salud != null)
                {
                    salud.TakeDamage(dañoPorContacto);
                    Debug.Log($"⚠️ El enemigo infligió {dañoPorContacto} daño al jugador.");
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if (pies != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pies.position, distanciaSuelo);
        }
    }
}
