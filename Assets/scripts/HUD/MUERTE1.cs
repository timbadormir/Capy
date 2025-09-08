using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaMuerte : MonoBehaviour
{
    public GameObject panelMuerte;

    void Start()
    {
        panelMuerte.SetActive(false); // oculta al inicio
    }

    // Mostrar pantalla de muerte
    public void ActivarPantalla()
    {
        panelMuerte.SetActive(true);
        Time.timeScale = 0f; // pausa el juego
    }

    // Botón de reintentar
    public void Reintentar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Botón del menú principal (las tres rayitas)
    public void IrAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu"); // 👈 pon aquí el nombre de tu escena de menú
    }
}
