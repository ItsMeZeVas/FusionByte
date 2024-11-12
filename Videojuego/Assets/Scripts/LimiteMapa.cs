using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimiteMapa : MonoBehaviour
{
    private int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            Player_Health health = collision.GetComponent<Player_Health>();
            health.takeDamage(damage);
            gameManager.Respawn();
        }
    }
}
