using UnityEngine;

public class PlayerHealth : CharacterStats
{
    public float deathHeight;

    #region Singleton

    public static PlayerHealth instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion

    void Update()
    {
        if (transform.position.y <= deathHeight)
            TakeDamage(int.MaxValue);
    }

    public override void Die()
    {
        base.Die();

        Destroy(gameObject);

        GameManager.instance.Respawn();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Enemy"))
        {
            TakeDamage(col.collider.GetComponent<EnemyHealth>().damageToPlayer);
            CharacterStats stats = col.collider.GetComponent<CharacterStats>();
            stats.TakeDamage(int.MaxValue);
        }
    }
}
