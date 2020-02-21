using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Transform player;
    public Transform spawnPoint;

    public RectTransform playerStatsIndicator;

    public TextMeshProUGUI playerHPText;
    public TextMeshProUGUI playerScoreText;

    public Transform[] livesImages;

    public GameObject gameOverPanel;

    public float spawnDelay = 2f;

    public int remainingLives;

    int CurScore;

    public int curScore
    {
        get
        {
            return CurScore;
        }

        set
        {
            AddScore(value);
        }
    }

    public int RemainingLives
    {

        get
        {
            return remainingLives;
        }

        set
        {
            remainingLives = Mathf.Clamp(value, 0, PlayerHealth.instance.lives);
        }

    }

    #region Singleton

    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion

    void Start()
    {
        RemainingLives = PlayerHealth.instance.lives;
    }

    public void AddScore(int score)
    {
        CurScore += score;

        playerScoreText.text = curScore.ToString();
    }

    public void Respawn()
    {
        if (RemainingLives == 0)
        {
            gameOverPanel.SetActive(true);

            return;
        }

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

        CharacterStats stats = p.GetComponent<CharacterStats>();

        stats.currentHealth = stats.maxHealth;

        if (stats.indicator == null)
            stats.indicator = playerStatsIndicator;

        if (stats.HPText == null)
            stats.HPText = playerHPText;

        stats.UpdateUI(stats.maxHealth);
    }

}
