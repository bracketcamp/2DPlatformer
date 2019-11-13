using UnityEngine;

public class PlayerHealth : CharacterStats
{
    public float deathHeight;

    public int lives = 3;

    private GameManager manager;

    #region Singleton

    public static PlayerHealth instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion

    void Start()
    {
        currentHealth = maxHealth;

        manager = GameManager.instance;
    }

    void Update()
    {
        if (transform.position.y <= deathHeight)
            TakeDamage(int.MaxValue);
    }

    public override void Die()
    {
        base.Die();

        manager.RemainingLives--;

        manager.Respawn();

        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Enemy"))
        {
            TakeDamage(col.collider.GetComponent<EnemyHealth>().damageToPlayer);
            CharacterStats stats = col.collider.GetComponent<EnemyHealth>();
            stats.TakeDamage(int.MaxValue);
        }
    }
}
