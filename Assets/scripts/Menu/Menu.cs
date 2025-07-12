using UnityEngine; // Highly likely to be present
using System.Collections; // Often included by default
using System.Collections.Generic; // Often included by default
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir()
    {
        Application.Quit();
    }
}