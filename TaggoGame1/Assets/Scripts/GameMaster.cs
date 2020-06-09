using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    public float countdown = 5f;

    public float timeBetweenEnemies = 1f;
    public int waveCount = 5;

    public string enemySpawnerTag = "EnemySpawner";
    public Transform enemy;
    public Transform stronkEnemy;
    public Text enemyCountText;

    private GameObject[] enemySpawners;
    private EnemySpawner[] enemySpawnerScrips;
    private int enemyCount;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("GameMaster Duplicate!!!");
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        enemySpawners = GameObject.FindGameObjectsWithTag(enemySpawnerTag);
        enemySpawnerScrips = new EnemySpawner[enemySpawners.Length];
        for(int i = 0; i < enemySpawners.Length; i++)
        {
                enemySpawnerScrips[i] = enemySpawners[i].GetComponent<EnemySpawner>();
        }
        enemyCount = waveCount * enemySpawnerScrips.Length;
        enemyCountText.text = "Enemies Left: " + enemyCount;
    }

    void Update()
    {
        enemyCountText.text = "Enemies Left: " + enemyCount;
        if (countdown <= 0 && waveCount > 0)
        {
            foreach(EnemySpawner enemySpawner in enemySpawnerScrips)
            {
                int rnd = Random.Range(0, 10) + 1;
                if(rnd > 9)
                {
                    SpawnEnemy(enemySpawner, stronkEnemy);
                }
                else
                {
                    SpawnEnemy(enemySpawner, enemy);
                }
            }
            waveCount--;
            countdown = timeBetweenEnemies;
        }
        countdown -= Time.deltaTime;
    }

    void SpawnEnemy(EnemySpawner spawner, Transform enemy)
    {
        spawner.SpawnEnemy(enemy);
    }

    public void ReportEnemyDeath()
    {
        enemyCount--;
    }
}
