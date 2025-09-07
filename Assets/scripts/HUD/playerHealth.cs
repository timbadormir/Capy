using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Configuración de Vida")]
    public int maxHealth = 3;         // Máximo número de corazones
    public int currentHealth;         // Vida actual

    [Header("Sprites de Corazones")]
    public Image[] fullHearts;        // Corazones llenos
    public Image[] emptyHearts;       // Corazones vacíos

    void Start()
    {
        currentHealth = maxHealth;    // Inicia con vida completa
        UpdateHearts();
    }

    void Update()
    {
        // Simulación de daño: tecla O
        if (Input.GetKeyDown(KeyCode.O))
        {
            TakeDamage(1);
        }

        // Recuperar vida: tecla K
        if (Input.GetKeyDown(KeyCode.K))
        {
            Heal(1);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHearts();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        UpdateHearts();
    }

    public void UpdateHearts()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            if (i < currentHealth)
            {
                fullHearts[i].enabled = true;
                emptyHearts[i].enabled = false;
            }
            else
            {
                fullHearts[i].enabled = false;
                emptyHearts[i].enabled = true;
            }
        }
    }
}
