using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    public static MagicAttack Instance;

    [SerializeField] private float Velocity;
    [SerializeField] private float Damage;

    private int direction = 1; // 1 para derecha, -1 para izquierda

    private void Start()
    {
        Destroy(gameObject, 0.75f);
    }



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(int dir)
    {
        // Configura la dirección del proyectil al ser instanciado
        direction = dir;
    }

    private void Update()
    {

        // Mueve el ataque mágico en la dirección establecida
        transform.Translate(Vector2.right * direction * Velocity * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemycontroller>().TomarDano(Damage);
            Destroy(gameObject);
        }
    }
}
