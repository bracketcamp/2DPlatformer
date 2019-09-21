using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " Died");
    }

}
