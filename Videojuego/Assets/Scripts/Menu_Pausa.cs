using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu_Pausa : MonoBehaviour
{

    private bool pausado = false;
    [SerializeField] private GameObject canva;
    [SerializeField] private Scene_Manager sceneManager;
    private GameManager gamemanager;
    private GameObject[] enemys;

    void Start()
    {
        canva.SetActive(false);
        gamemanager = GetComponent<GameManager>();
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
    }


    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            AlternarPausa();

        }

    }

    public void AlternarPausa()
    {

        pausado = !pausado;

        if (pausado)
        {
            canva.SetActive(true);
            Time.timeScale = 0f;

        }
        else
        {
            canva.SetActive(false);
            Time.timeScale = 1f;

        }
    }


    public void Continuar()
    {
        canva.SetActive(false);
        Time.timeScale = 1f;
    }




    public void Reiniciar()
    {
        
        canva.SetActive(false);
        Invoke("RespawnFromGameManager", 0.1f);
        for (int i = 0; i < enemys.Length; i++)
        {
            Enemycontroller controller = enemys[i].GetComponent<Enemycontroller>();
            controller.resetEnemy();
        }
        Time.timeScale = 1f;
    }


    public void Salir()
    {
        canva.SetActive(false);
        Time.timeScale = 1f;
        sceneManager.GameStart();
    }

    private void RespawnFromGameManager()
    {
        gamemanager.RespawnPlayer();
    }
}

