using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Player_Health : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth = 5;
    private bool recibiendoDano;
    private bool isDead = false;
    public float FuerzaRebote = 10000f;
    
    private Character_Controller characterController;
    private GameManager gameManager;
    private HUD hud;
    private Rigidbody2D rb;

    void Start()
    {
        currentHealth = maxHealth;
        characterController = GetComponent<Character_Controller>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        playerDead();

        if (Input.GetKeyDown(KeyCode.R))
        {
            gameManager.Respawn();
        }
    }

    public void takeDamage(Vector2 direccion, int damage)
    {
        if (!recibiendoDano)
        {
            recibiendoDano = true;
            currentHealth -= damage;

            //LA MALDITA MUÑECA NO REBOTA CUANDO SE CHOCA >:c
            Vector2 rebote = new Vector2(rb.transform.position.x - direccion.x, 0f).normalized;
            Debug.Log("Rebote en X: " + rebote.x + ", Rebote en Y: " + rebote.y);
            rb.AddForce(rebote * FuerzaRebote, ForceMode2D.Impulse);
            Debug.Log("Rebote aplicado: " + rebote * FuerzaRebote);

            hud.DesactivarVida(currentHealth);

            print("taking damage");
        }

        recibiendoDano = false;
    }

    public void takeDamage(int damage)
    {
        if (!recibiendoDano)
        {
            recibiendoDano = true;
            currentHealth -= damage;
            hud.DesactivarVida(currentHealth);

            print("taking damage");
            
        }

        recibiendoDano = false;
    }

    public bool getRecibiendoDano()
    {
        return recibiendoDano;
    }

    public void playerDead()
    {
        if (currentHealth == 0)
        {
            gameManager.setStartCheckpoint();
            gameManager.Respawn();
            currentHealth = maxHealth;
            hud.ResetVidas();
        }
    }
}
