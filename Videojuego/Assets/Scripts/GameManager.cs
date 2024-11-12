using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    private Vector3 checkpointPosition;
    [SerializeField] private Transform startCheckpoint;
    [SerializeField] private Transform endCheckpoint;
    public int PuntosTotalesInsignia { get { return puntosTotalesInsignia; } }
    private int puntosTotalesInsignia;

    public int PuntosTotalesCalavera { get { return puntosTotalesCalavera; } }
    private int puntosTotalesCalavera;

    public int PuntosTotalesOjo { get { return puntosTotalesOjo; } }
    private int puntosTotalesOjo;

    public HUD hud;
    private int VidasActuales=5;
    private Collectable_Controller tipo;
   
    private Transform player;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
        player.transform.position = checkpointPosition;
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
}

