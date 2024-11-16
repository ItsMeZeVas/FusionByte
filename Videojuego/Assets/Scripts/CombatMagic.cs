using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class CombatMagic : MonoBehaviour
{

    [SerializeField] private GameObject magicAttackPrefab;
    [SerializeField] private Transform magicAttackSpawnPoint;

    private Animator animator;
    private Player_Health playerHealth;
    private bool canAttack = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<Player_Health>();
    }

    private void Update()
    {
        if (playerHealth.getRecibiendoDano() == false)
        {
            if (Input.GetKeyDown(KeyCode.O) && canAttack)
            {
                canAttack = false;
                animator.SetTrigger("Magia");
                LaunchMagicAttack();
                StartCoroutine(Recarga());
            }
        }
    }
    // Start is called before the first frame update
    public void LaunchMagicAttack()
    {
        // Determinar la direcci�n en la que el personaje est� mirando
        int direction = transform.localScale.x > 0 ? 1 : -1;

        // Instanciar el proyectil y pasarle la direcci�n
        GameObject magicAttack = Instantiate(magicAttackPrefab, magicAttackSpawnPoint.position, Quaternion.identity);
        magicAttack.GetComponent<MagicAttack>().SetDirection(direction);
        
    }

    private IEnumerator Recarga()
    {
        yield return new WaitForSeconds(2);
        canAttack = true;
    }
}
