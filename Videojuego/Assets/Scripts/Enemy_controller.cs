using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum EnemyType
{
    DUMMY, MOCO, ROCA, GHOST, DIABLO, ALMA
}

public class Enemycontroller : MonoBehaviour
{
    
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private BoxCollider2D boxCollider;

    [SerializeField] private float maxHealth = 100;
    private float currentHealth = 100;

    [SerializeField] private Transform player ;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float detectionRadius = 1.0f;
    [SerializeField] private EnemyType enemyType;
    
    private Rigidbody2D rb;
    private Vector2 movement;
    float distanceToPlayer;
    bool lookLeft = true;
    private bool isDead = false;
    private bool recibiendoDano;
    private Vector2 posicionInicial;
    private Player_Health playerHealth;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Health>();
      
    }

    private void Awake()
    {
        posicionInicial = transform.position;
        print(posicionInicial);
        currentHealth = maxHealth;
    }
    private void Update()
    {
        if (playerHealth.getIsDead())
        {
            resetEnemy();
        }
        
    }

    private void FixedUpdate()
    {
        movementAndDetection();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void movementAndDetection()
    {
        if (player != null)
        {
            // Calcula la distancia entre el enemigo y el jugador
            distanceToPlayer = Vector2.Distance(transform.position, player.position);
            //print("ENEMY: " + enemyType + ", Distance: " + distanceToPlayer);

            // Verifica si está dentro del radio de detección y no es un enemigo DUMMY
            if (distanceToPlayer < detectionRadius && enemyType != EnemyType.DUMMY && isDead == false)
            {
                //print("Detected");
                Vector2 direction = (player.position - transform.position).normalized;
                enemyOrientation();
                movement = new Vector2(direction.x, 0);
                animator.SetBool("isMoving", true);
                rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
            }
            else
            {
                movement = Vector2.zero;
            }
            
        }
    }

    private void enemyOrientation()
    {
        if ((lookLeft && player.position.x > transform.position.x) || (!lookLeft && player.position.x < transform.position.x))
        {
            lookLeft = !lookLeft;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    public void TomarDano(float Dano)
    {

        do
        {
            if (currentHealth < Dano)
            {
                currentHealth = 0;
            }
            else
            {
                currentHealth -= Dano;
            }
            recibiendoDano = true;
        } while (currentHealth > 0 && !recibiendoDano && !isDead);

        recibiendoDano = false;

        if (currentHealth <= 0 || isDead)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        isDead = true;
        animator.SetBool("isDead", isDead);
        boxCollider.enabled = false;
        print("Is Dead");
    }

    public bool getIsDead()
    {
        return isDead;
    }
     public void resetEnemy()
    {
        rb.velocity = Vector2.zero;
        transform.position = posicionInicial;
        currentHealth = maxHealth;
        boxCollider.enabled = true;
        isDead = false;
        animator.SetBool("isDead", isDead);
 
    }

}

