using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine;



public class Checkpoint : MonoBehaviour
{
    [SerializeField] private bool isEndCheckpoint;
    [SerializeField] private Scene_Manager sceneManager;
    [SerializeField] private AudioClip CheckpointSound;
    private Puntaje puntaje;
    private Animator animator;
    //[SerializeField] private int nextLevel;

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

            if (isEndCheckpoint == true)
            {
                sceneManager.selectLevel(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}