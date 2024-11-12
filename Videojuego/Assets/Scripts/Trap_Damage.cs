using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Damage : MonoBehaviour
{
    [SerializeField]private int damage;
    private Player_Health health;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("collide");
            health = collision.gameObject.GetComponent<Player_Health>();

            Vector2 damageDirection = transform.position;
            health.takeDamage(damageDirection, damage);
            health.playerDead();
        }
    }
}
