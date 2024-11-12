using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puntaje : MonoBehaviour
{
    public GameObject[] Estrellas;
    // Start is called before the first frame update
    void Start()
    { 
        Estrellas[0].SetActive(false);
        Estrellas[1].SetActive(false);
        Estrellas[2].SetActive(false);
    }

    public void EstrellasPantalla()
    {
        Debug.Log("Funciona");
    }

    
}
