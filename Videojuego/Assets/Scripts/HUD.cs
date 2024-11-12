using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI Puntos_Insignia;
    public TextMeshProUGUI Puntos_Calavera;
    public TextMeshProUGUI Puntos_Ojo;

    public GameObject[] vidas;
    public GameObject[] vidasPerdidas;

    private void Start()
    {
        vidasPerdidas[0].SetActive(false);
        vidasPerdidas[1].SetActive(false);
        vidasPerdidas[2].SetActive(false);
        vidasPerdidas[3].SetActive(false);
        vidasPerdidas[4].SetActive(false);
    }
   
    public void ActualizarPuntos(TypeCollectable tipo)
    {
        if (tipo == TypeCollectable.INSIGNIA)
        {
            Puntos_Insignia.text = gameManager.PuntosTotalesInsignia.ToString();
        }

        if (tipo == TypeCollectable.CALAVERA)
        {
            Puntos_Calavera.text = gameManager.PuntosTotalesCalavera.ToString();
        }

        if (tipo == TypeCollectable.OJO)
        {
            Puntos_Ojo.text = gameManager.PuntosTotalesOjo.ToString();
        }
    }

    public void DesactivarVida(int indice)
    {
        vidas[indice].SetActive(false);
        vidasPerdidas[indice].SetActive(true);
    }

    public void ResetVidas()
    {
        for (int i = 0; i < 5; i++)
        {
            vidas[i].SetActive(true);
            vidasPerdidas[i].SetActive(false);
        }
    }
}
