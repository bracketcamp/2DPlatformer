﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyHealth : CharacterStats
{

    public int damageToPlayer = 10;

    public static List<Transform> enemies = new List<Transform>();

    public override void Die()
    {
        base.Die();

        enemies.Remove(transform);

        Destroy(gameObject);
    }

}
