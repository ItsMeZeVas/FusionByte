using UnityEngine;

public class CamaraFijaY : MonoBehaviour
{
    [SerializeField] private Transform jugador; // Referencia al transform del jugador
    [SerializeField] private float suavizado = 0.1f; // Factor de suavizado
    private float posYCamara; // Posici�n Y fija de la c�mara

    private void Awake()
    {
        // Guardar la posici�n Y inicial de la c�mara
        posYCamara = transform.position.y;
    }

    private void Update()
    {
        // Obtener la posici�n X del jugador
        float posXJugador = jugador.position.x;

        // Suavizar el movimiento de la c�mara en X
        float nuevaPosX = Mathf.Lerp(transform.position.x, posXJugador, suavizado);

        // Actualizar la posici�n de la c�mara, manteniendo Y fija
        transform.position = new Vector3(nuevaPosX, posYCamara, transform.position.z);
    }
}
