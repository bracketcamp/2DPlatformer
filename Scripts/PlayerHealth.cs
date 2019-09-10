using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    private int Health;

    [HideInInspector]
    public int health
    {

        get
        {
            return Health;
        }

        set
        {
            Health = Mathf.Clamp(value, 0, maxHealth);
        }


    }

    public int maxHealth = 100;

    public float deathHeight = -4f;

    public static PlayerHealth instance;

    void Awake()
    {
        Health = maxHealth;

        if (instance == null)
            instance = this;
    }

    void Update()
    {
        if (transform.position.y <= deathHeight)
            DamagePlayer(maxHealth);
    }

    public void DamagePlayer(int damage)
    {
        Health -= damage;

        if (Health <= 0)
            KillPlayer();
    }

    void KillPlayer()
    {
        Destroy(gameObject);
    }

}
