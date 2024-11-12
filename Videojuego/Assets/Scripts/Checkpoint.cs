using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private bool isEndCheckpoint;
    [SerializeField] private Scene_Manager sceneManager;
    [SerializeField] private AudioClip CheckpointSound;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float tiempoEspera; // Tiempo de espera para mostrar el puntaje

    [SerializeField] private CanvasGroup panelTransicion; // Panel para las transiciones de fade in y fade out
    [SerializeField] private float duracionTransicion; // Duraci�n de cada transici�n

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager player = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            if (player != null)
            {
                player.SetCheckpoint(transform.position);
                animator.SetBool("IsCheckpointActive", true);
                ControladorSonidos.Instance.EjecutarSonido(CheckpointSound);
            }

            if (isEndCheckpoint)
            {
                StartCoroutine(TransicionFinal());
            }
        }
    }

    private IEnumerator TransicionFinal()
    {
        // 1. Hacer fade in para oscurecer la pantalla
        panelTransicion.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1)); // Fade in hacia opaco

        // 2. Mostrar el puntaje en el canvas de estrellas
        gameManager.PuntajeFinal();

        // 3. Esperar para que el jugador vea el puntaje
        yield return new WaitForSeconds(tiempoEspera);

        // 4. Hacer fade out para oscurecer nuevamente la pantalla
        yield return StartCoroutine(Fade(0)); // Fade out hacia transparente

        yield return new WaitForSeconds(tiempoEspera);


        // 5. Cargar el siguiente nivel despu�s del fade out
        yield return StartCoroutine(Fade(1)); // Fade in hacia opaco

        yield return new WaitForSeconds(0.5f);
        sceneManager.selectLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = panelTransicion.alpha;
        float elapsedTime = 0;

        while (elapsedTime < duracionTransicion)
        {
            elapsedTime += Time.deltaTime;
            panelTransicion.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duracionTransicion);
            yield return null;
        }

        panelTransicion.alpha = targetAlpha;

        // Desactivar el panel despu�s del fade out, si es necesario
        
    }
}
