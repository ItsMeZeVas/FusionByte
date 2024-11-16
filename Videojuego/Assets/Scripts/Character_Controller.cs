using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Character_Controller : MonoBehaviour
{
    public float Speed = 10f;
    public float JumpForce = 12f;
    public float MaxHoldTime = 0.5f; // Tiempo m�ximo que se puede mantener presionada la tecla de salto
    public LayerMask capaFloor;
    public int MaxJumps = 1;
    public float fallMultiplier = 2.5f; // Factor que incrementa la velocidad de ca�da
    public float lowJumpMultiplier = 2f; // Factor que desacelera la ca�da cuando se suelta la tecla

    private bool LookRight = true;
    private int RestJumps=0;
    private Rigidbody2D playerRigidBody;
    private CapsuleCollider2D capsuleCollider;
    private Player_Health playerHealth;
    private bool isJumping;
    private float jumpTimeCounter;
    private bool recibiendoDano;


    private Animator animator;
    [SerializeField] private AudioClip JumpSound;
  

    public void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<Player_Health>();
    }

    void Update()
    {
        if (playerHealth.getRecibiendoDano() == false)
        {
            print("Saltos restantes: " + RestJumps);
            Jump();
        }
        ApplyGravityModifiers();
    }

    private void FixedUpdate()
    {
        if (playerHealth.getRecibiendoDano() == false)
        {
            Movement();
        }
    }

    bool InFloor()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center, new Vector2(capsuleCollider.bounds.size.x, capsuleCollider.bounds.size.y), 0f, Vector2.down, 0.005f, capaFloor);
        Debug.DrawRay(capsuleCollider.bounds.center, Vector2.down * 0.8f, Color.red);
        return raycastHit.collider != null;
    }

    void Jump()
    {
        // Si el personaje esta en el suelo, restablece el n�mero de saltos disponibles
        if (InFloor())
        {
            RestJumps = MaxJumps;
            animator.SetBool("InFloor",true);
        
        }
        if (!InFloor())
        {
            animator.SetBool("InFloor", false);
        }

        // Inicia el salto
        if (Input.GetKeyDown(KeyCode.Space) && RestJumps > 0)
        {
            RestJumps--;
            isJumping = true;
            jumpTimeCounter = MaxHoldTime;
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, JumpForce);
            ControladorSonidos.Instance.EjecutarSonido(JumpSound);
        }

        // Mant�n el salto mientras se mantenga presionada la tecla

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, JumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        // Finaliza el salto al soltar la tecla
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
     
    }

    void ApplyGravityModifiers()
    {
        if (playerRigidBody.velocity.y < 0)
        {
            // Aplica un multiplicador para acelerar la ca�da
            playerRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (playerRigidBody.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            // Aplica un multiplicador para suavizar la ca�da si se suelta la tecla de salto
            playerRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void Movement()
    {
        
        // Movimiento del personaje
        float inputMovement = Input.GetAxis("Horizontal");
        animator.SetFloat("Horizontal", Mathf.Abs(inputMovement));
        animator.SetFloat("VelocidadY", playerRigidBody.velocity.y);
        playerRigidBody.velocity = new Vector2(inputMovement * Speed, playerRigidBody.velocity.y);
        Orientation(inputMovement);
        //animator.SetBool("RecibeDano", playerHealth.getRecibiendoDano());
    }

    void Orientation(float inputMovement)
    {
        // Orientaci�n del personaje
        if ((LookRight && inputMovement < 0) || (!LookRight && inputMovement > 0))
        {
            LookRight = !LookRight;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);

        }
    }
}

