using UnityEngine;

public class EnemigoPerseguidor : MonoBehaviour
{
    [Header("Persecución")]
    public Transform jugador;          // Arrastra aquí el Player en el Inspector
    public float velocidad = 2f;       // Velocidad de persecución
    public float rangoDeteccion = 10f; // Distancia máxima para empezar a perseguir

    [Header("Salto")]
    public float fuerzaSalto = 5f;     // Qué tan alto salta
    public float tiempoEntreSaltos = 2f; // Cada cuántos segundos salta
    private float temporizadorSalto = 0f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        temporizadorSalto = tiempoEntreSaltos; // empieza el contador
    }

    void FixedUpdate()
    {
        if (jugador == null) return;

        float distancia = Vector2.Distance(transform.position, jugador.position);

        if (distancia < rangoDeteccion)
        {
            // Mover solo en X
            Vector2 nuevaPos = new Vector2(
                Mathf.MoveTowards(transform.position.x, jugador.position.x, velocidad * Time.fixedDeltaTime),
                transform.position.y
            );

            rb.MovePosition(nuevaPos);
        }

        // Contador de salto
        temporizadorSalto -= Time.fixedDeltaTime;
        if (temporizadorSalto <= 0f)
        {
            Saltar();
            temporizadorSalto = tiempoEntreSaltos; // reiniciar contador
        }
    }

    void Saltar()
    {
        // Solo salta si está en el suelo
        if (Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            Debug.Log("🟢 El enemigo saltó!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("⚠️ El enemigo tocó al jugador");
            // Aquí podrías restar vida al jugador
        }
    }
}
