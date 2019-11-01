using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{

    public Transform[] enemies;

    public float waveDelay = 2f;
    public float spawnDelay = 0.5f;

    public int minEnemies = 2;
    public int maxEnemies = 10;

    private WaveType mode;

    private float countdown;

    void Start()
    {
        countdown = waveDelay;
    }

    void Update()
    {
        if (PlayerHealth.instance == null)
            return;

        if (mode == WaveType.Spawning || mode == WaveType.Waiting)
        {
            if (EnemyHealth.enemies.Count == 0)
                mode = WaveType.Counting;

            return;
        }

        if (countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            mode = WaveType.Waiting;

            countdown = waveDelay;
        }
        else
        {
            countdown -= Time.deltaTime;

            mode = WaveType.Counting;
        }
    }

    IEnumerator SpawnWave()
    {
        mode = WaveType.Spawning;

        int enemiesCount = Random.Range(minEnemies, maxEnemies);

        for (int i = 0; i < enemiesCount; i++)
        {
            Transform randEnemy = enemies[Random.Range(0, enemies.Length - 1)];

            Vector2 playerPos = PlayerHealth.instance.transform.position;

            Vector2 pos = new Vector2(Random.Range(-20f, 20f) + playerPos.x, Random.Range(-5f, 20f) + playerPos.y);

            Transform enemy = Instantiate(randEnemy, pos, Quaternion.identity);

            EnemyHealth.enemies.Add(enemy);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

}

public enum WaveType
{
    Spawning,
    Counting,
    Waiting
}