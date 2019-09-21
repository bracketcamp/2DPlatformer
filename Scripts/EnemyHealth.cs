using UnityEngine;

public class EnemyHealth : CharacterStats
{

    public int damageToPlayer = 10;

    public override void Die()
    {
        base.Die();

        Destroy(gameObject);
    }

}
