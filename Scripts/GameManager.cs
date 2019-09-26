﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public Transform player;
    public Transform spawnPoint;

    public float spawnDelay = 2f;

    #region Singleton

    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion

    public void Respawn()
    {
        StartCoroutine(RespawnPlayer());
    }

    private IEnumerator RespawnPlayer()
    {
        ObjectPooler.instance.SpawnFromPool("RespawnParticles", spawnPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnDelay);
        Transform p = Instantiate(player, spawnPoint.position, Quaternion.identity);

        FindObjectOfType<PlayerFollow>().health = p.GetComponent<PlayerHealth>();
        foreach (EnemyAI ai in FindObjectsOfType<EnemyAI>())
        {
            ai.player = p;
        }
    }

}
