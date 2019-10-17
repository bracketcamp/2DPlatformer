using UnityEngine;
using TMPro;

public class CharacterStats : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth
    {
        get
        {
            return curHealth;
        }

        private set
        {
            curHealth = Mathf.Clamp(value, 0, maxHealth);
        }
    }

    private int curHealth;

    public RectTransform indicator;

    public TextMeshProUGUI HPText;

    void Start()
    {
        currentHealth = maxHealth;

        UpdateUI(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        UpdateUI(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " Died");
    }

    public void UpdateUI(int cur)
    {
        if (indicator == null || HPText == null)
            return;

        float val = (float)cur / maxHealth;

        indicator.localScale = new Vector3(val, indicator.localScale.y, indicator.localScale.z);

        HPText.text = currentHealth + " / " + maxHealth;
    }

}
