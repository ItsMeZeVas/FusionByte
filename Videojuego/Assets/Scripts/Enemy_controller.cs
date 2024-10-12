using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemycontroller : MonoBehaviour
{
    [SerializeField] private float vida = 100;
    private Animator animator;
    [SerializeField]private bool isDead;
    private bool takeDamage = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TomarDano(float Dano)
    {
        do
        {
            if (vida < Dano)
            {
                vida = 0;
            }
            else
            {
                vida -= Dano;
            }
            takeDamage = true;
        } while (vida > 0 && !takeDamage && !isDead);

        takeDamage = false;

        if(vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    { 
        isDead = true;
        animator.SetTrigger("Muerte");
    }

    public bool getIsDead()
    {
        return isDead;
    }
}

