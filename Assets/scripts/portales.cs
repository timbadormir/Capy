using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioDeEscenaPorColision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
  Debug.Log("Entró al portal");
        if (collision.CompareTag("Portal"))
        {
            SceneManager.LoadScene("sabana");
        }
    }
}
