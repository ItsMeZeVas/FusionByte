using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    private Vector3 checkpointPosition;
    
    [SerializeField] private Transform startCheckpoint;
    [SerializeField] private Transform endCheckpoint;
    [SerializeField] CinemachineVirtualCamera camera_follow;

    [SerializeField] GameObject character;
    private Character_Controller characterController;
    private Player_Health playerHealthClass;
    private Transform playerPosition;
    private Rigidbody2D playerRB;
    public int puntajeFinal;
    public int PuntosTotalesInsignia { get { return puntosTotalesInsignia; } }
    private int puntosTotalesInsignia;

    public int PuntosTotalesCalavera { get { return puntosTotalesCalavera; } }
    private int puntosTotalesCalavera;

    public int PuntosTotalesOjo { get { return puntosTotalesOjo; } }
    private int puntosTotalesOjo;

    public HUD hud;
    public Puntaje hudPuntaje;
    private Collectable_Controller tipo;
    private Checkpoint Checkpoint;

    private void Awake()
    {
        playerPosition = character.GetComponent<Transform>();
        characterController = character.GetComponent<Character_Controller>();
        playerHealthClass = character.GetComponent<Player_Health>();
        playerRB = character.GetComponent<Rigidbody2D>();
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        checkpointPosition = newCheckpoint;
    }

    public void setStartCheckpoint()
    {
        checkpointPosition = startCheckpoint.position;
    }

    public void Respawn()
    {

        playerRB.velocity = Vector2.zero;
        playerPosition.transform.position = checkpointPosition;
        camera_follow.Follow = playerPosition;
        
    }

    public void SumarPuntos(int puntosASumar, TypeCollectable typeCollectable)
    {
        if (typeCollectable == TypeCollectable.INSIGNIA)
        {
            puntosTotalesInsignia += puntosASumar;
            hud.ActualizarPuntos(typeCollectable);
        }
        if (typeCollectable == TypeCollectable.CALAVERA)
        {
            puntosTotalesCalavera += puntosASumar;
            hud.ActualizarPuntos(typeCollectable);
        }
        if (typeCollectable == TypeCollectable.OJO)
        {
            puntosTotalesOjo += puntosASumar;
            hud.ActualizarPuntos(typeCollectable);
        }   
    }
    public void PuntajeFinal()
    {
        puntajeFinal = puntosTotalesCalavera + puntosTotalesInsignia + puntosTotalesOjo;
        hudPuntaje.EstrellasPantalla(puntajeFinal);
        
    }

    public void RespawnPlayer()
    {
        characterController.enabled = true;
        setStartCheckpoint();
        Respawn();
        playerHealthClass.setCurrenHealth(playerHealthClass.getMaxHealth());
        hud.ResetVidas();
        playerHealthClass.setIsDead(false);
    }


}

