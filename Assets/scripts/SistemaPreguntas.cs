using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;

[System.Serializable]
public class Opcion
{
    public string id;
    public string texto;
}

[System.Serializable]
public class Pregunta
{
    public int id;
    public string enunciado;
    public Opcion[] opciones;
    public string respuesta_correcta;
}

[System.Serializable]
public class Materia
{
    public List<Pregunta> nivel_1;
    public List<Pregunta> nivel_2;
    public List<Pregunta> nivel_3;
    public List<Pregunta> nivel_4;
}

[System.Serializable]
public class Root
{
    public Materia matematicas;
}

public class SistemaPreguntas : MonoBehaviour
{
    [Header("UI - arrastra aquí")]
    public TextMeshProUGUI textoPregunta;
    public Button[] botonesOpciones;
    public TextMeshProUGUI[] textosOpciones;

    [Header("JSON")]
    public string jsonResourceName = "Preguntas";
    public string nivelActual = "nivel_1";

    [Header("Gameplay")]
    public GameObject tilemapConPilar;
    public int respuestasNecesarias = 5;
    public float tiempoBajada = 2f;
    public float distanciaBajada = 5f;

    [Header("Vida del jugador")]
    public PlayerHealth playerHealth;   // 👈 referencia al script PlayerHealth

    private int respuestasCorrectas = 0;
    private bool desbloqueado = false;

    private Root root;
    private List<Pregunta> pool;
    private Pregunta preguntaActual;

    void Start()
    {
        if (!LoadJson()) return;
        SetupButtonListeners();
        MostrarPreguntaAleatoria();
    }

    bool LoadJson()
    {
        TextAsset ta = Resources.Load<TextAsset>(jsonResourceName);
        if (ta == null)
        {
            Debug.LogError($"[SistemaPreguntas] No se encontró Resources/{jsonResourceName}.json");
            return false;
        }

        root = JsonUtility.FromJson<Root>(ta.text);
        if (root == null || root.matematicas == null)
        {
            Debug.LogError("[SistemaPreguntas] JSON parse falló.");
            return false;
        }

        switch (nivelActual)
        {
            case "nivel_1": pool = root.matematicas.nivel_1; break;
            case "nivel_2": pool = root.matematicas.nivel_2; break;
            case "nivel_3": pool = root.matematicas.nivel_3; break;
            case "nivel_4": pool = root.matematicas.nivel_4; break;
            default: pool = root.matematicas.nivel_1; break;
        }

        return pool != null && pool.Count > 0;
    }

    void SetupButtonListeners()
    {
        for (int i = 0; i < botonesOpciones.Length; i++)
        {
            int idx = i;
            botonesOpciones[i].onClick.RemoveAllListeners();
            botonesOpciones[i].onClick.AddListener(() => OnOpcionSeleccionada(idx));
        }
    }

    void MostrarPreguntaAleatoria()
    {
        preguntaActual = pool[Random.Range(0, pool.Count)];
        textoPregunta.text = preguntaActual.enunciado;

        for (int i = 0; i < textosOpciones.Length; i++)
        {
            if (i < preguntaActual.opciones.Length)
                textosOpciones[i].text = preguntaActual.opciones[i].texto;
            else
                textosOpciones[i].text = "";
        }
    }

    void OnOpcionSeleccionada(int indice)
    {
        if (preguntaActual == null) return;
        if (indice >= preguntaActual.opciones.Length) return;

        string seleccionado = preguntaActual.opciones[indice].id;
        bool correcto = seleccionado == preguntaActual.respuesta_correcta;

        if (correcto)
        {
            Debug.Log("✅ Correcto!");
            respuestasCorrectas++;

            if (!desbloqueado && respuestasCorrectas >= respuestasNecesarias)
            {
                desbloqueado = true;
                if (tilemapConPilar != null)
                {
                    StartCoroutine(BajarPilar());
                }
            }
        }
        else
        {
            Debug.Log("❌ Incorrecto. Pierdes un corazón.");
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);   // 👈 Le quita 1 vida
            }
        }

        MostrarPreguntaAleatoria();
    }

    IEnumerator BajarPilar()
    {
        Vector3 inicio = tilemapConPilar.transform.position;
        Vector3 destino = inicio + new Vector3(0, -distanciaBajada, 0);

        float t = 0f;
        while (t < tiempoBajada)
        {
            t += Time.deltaTime;
            tilemapConPilar.transform.position = Vector3.Lerp(inicio, destino, t / tiempoBajada);
            yield return null;
        }

        tilemapConPilar.transform.position = destino;

        TilemapCollider2D collider = tilemapConPilar.GetComponent<TilemapCollider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }
}
