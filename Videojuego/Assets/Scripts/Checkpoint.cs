using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private bool isEndCheckpoint;
    [SerializeField] private Scene_Manager sceneManager;
    [SerializeField] private AudioClip CheckpointSound;
    [SerializeField] private float tiempoEspera; // Tiempo de espera para mostrar el puntaje

    [SerializeField] private CanvasGroup panelTransicion; // Panel para las transiciones de fade in y fade out
    [SerializeField] private float duracionTransicion; // Duración de cada transición

    private Animator animator;
    private Character_Controller characterController;
    private Rigidbody2D characterRB;
    private GameManager gameManager;
    private GameObject[] enemys;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            characterController = other.GetComponent<Character_Controller>();
            characterRB = other.GetComponent<Rigidbody2D>();
            if (gameManager != null)
            {
                gameManager.SetCheckpoint(transform.position);
                animator.SetBool("IsCheckpointActive", true);
                ControladorSonidos.Instance.EjecutarSonido(CheckpointSound);
            }

            if (isEndCheckpoint)
            {
                for (int i = 0;  i < enemys.Length; i++)
                {
                    Enemycontroller controller = enemys[i].GetComponent<Enemycontroller>();
                    controller.enabled = false;
                }

                characterRB.velocity = Vector2.zero;
                characterController.enabled = false;
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


        // 5. Cargar el siguiente nivel después del fade out
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

        // Desactivar el panel después del fade out, si es necesario
        
    }
}
