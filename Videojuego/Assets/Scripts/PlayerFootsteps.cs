using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioClip footstepClip; // El clip completo que contiene los sonidos de los pasos
    public float stepInterval = 0.5f; // Intervalo entre pasos (en segundos)
    private float stepTimer = 0f; // Temporizador para controlar el intervalo de pasos
    private AudioSource audioSource; // Fuente de audio para reproducir el sonido

    private float clipLength; // Longitud total del clip en segundos
    private int stepSampleCount; // Número de muestras para un solo paso
    private bool isGrounded = false; // Flag para saber si el personaje está tocando el suelo

    void Start()
    {
        // Obtener la fuente de audio
        audioSource = GetComponent<AudioSource>();

        // Obtener la longitud del clip de audio
        clipLength = footstepClip.length;

        // Calcular cuántas muestras corresponden a un solo paso (suponiendo que el archivo tiene 2 pasos)
        stepSampleCount = Mathf.FloorToInt(footstepClip.samples / 2); // Suponiendo 2 pasos en el clip
    }

    void Update()
    {
        // Verifica si el personaje está en el suelo y en movimiento
        if (isGrounded && (Input.GetKey("a") || Input.GetKey("d")))
        {
            // Incrementar el temporizador
            stepTimer += Time.deltaTime;

            // Si el temporizador alcanza el intervalo de paso, reproducir un fragmento del sonido
            if (stepTimer >= stepInterval)
            {
                PlayFootstepSound();
                stepTimer = 0f; // Reiniciar el temporizador
            }
        }
    }

    void PlayFootstepSound()
    {
        // Calcular en qué parte del clip se encuentra el siguiente paso
        float stepStartTime = (stepTimer / stepInterval) % 2 * stepSampleCount / (float)footstepClip.frequency;

        // Crear un nuevo AudioClip con solo el fragmento del paso
        AudioClip stepClip = AudioClip.Create("Footstep", stepSampleCount, footstepClip.channels, footstepClip.frequency, false);
        float[] stepData = new float[stepSampleCount];

        // Copiar solo la sección correspondiente del clip original
        footstepClip.GetData(stepData, Mathf.FloorToInt(stepStartTime * footstepClip.frequency));
        stepClip.SetData(stepData, 0);

        // Reproducir el fragmento del paso
        audioSource.PlayOneShot(stepClip);
    }

    // Método para detectar cuando el personaje está tocando el suelo
    private void OnCollisionStay2D(Collision2D collision)
    {
        // Aquí verificamos si el personaje está tocando un objeto con el tag "Ground"
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true; // El personaje está tocando el suelo
        }
    }

    // Método para detectar cuando el personaje deja de estar en el suelo
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false; // El personaje ha dejado de tocar el suelo
        }
    }
}
