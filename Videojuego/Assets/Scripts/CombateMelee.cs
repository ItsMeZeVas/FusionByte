using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CombateMelee : MonoBehaviour
{
    [SerializeField] private Transform AttackController;
    [SerializeField] private float radio;
    [SerializeField] private float Dano;
    [SerializeField] private AudioClip WoodSwordSound;
    public float tiempoEntreAtaques;
    public float tiempoSiguienteAtaque;
    private Animator animator;
    private Enemycontroller enemycontroller;
    private bool canAttackMelee = true;
    private Character_Controller characterController;
    private Rigidbody2D rb;
    public void Start()
    {
        characterController = GetComponent<Character_Controller>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (tiempoSiguienteAtaque>0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.P)&&tiempoSiguienteAtaque<=0 && canAttackMelee)
        {
            characterController.enabled = false;
            rb.velocity = Vector2.zero;
            canAttackMelee = false; 
            Golpe();
            tiempoSiguienteAtaque = tiempoEntreAtaques;
            StartCoroutine(Recarga());
            
        }
    }
    private void Golpe()
    {
        animator.SetTrigger("Golpe");
        ControladorSonidos.Instance.EjecutarSonido(WoodSwordSound);
        Collider2D[] Objetos = Physics2D.OverlapCircleAll(AttackController.position, radio);

        foreach (Collider2D collider2D in Objetos)
        {
            if (collider2D.CompareTag("Enemy"))
            {
                enemycontroller = collider2D.transform.GetComponent<Enemycontroller>();
                Debug.Log("Enemigo encontrado: " + collider2D.name);
                if (enemycontroller.getIsDead() == false)
                {
                    enemycontroller.TomarDano(Dano);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(AttackController.position, radio);
    }

    private IEnumerator Recarga()
    {
        yield return new WaitForSeconds(0.5f);
        canAttackMelee = true;
        characterController.enabled = true;
    }
}


