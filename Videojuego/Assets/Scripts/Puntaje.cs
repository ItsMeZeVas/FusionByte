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
        Estrellas[3].SetActive(false);
    }

    public void EstrellasPantalla(int cantidad)
    {
        if (cantidad == 0)
        {
            Estrellas[0].SetActive(true);
        }
        else if (cantidad >= 1 && cantidad <= 2)
        {
            Estrellas[1].SetActive(true);
        }
        else if (cantidad >= 3 && cantidad <= 4)
        {
            Estrellas[2].SetActive(true);
        }
        else if (cantidad >= 5)
        {
            Estrellas[3].SetActive(true);
        }
    }

    
}
