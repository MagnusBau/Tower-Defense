using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public float countdown = 5f;

    public float timeBetweenEnemies = 1f;
    public int waveCount = 5;

    public string enemySpawnerTag = "EnemySpawner";
    public string enemyCounterTag = "EnemyCounter";
    public Transform enemy;
    public Transform stronkEnemy;
    public Text enemyCountText;

    private EnemySpawner[] enemySpawners;
    private int enemyCount;

    void Start()
    {
        
        if (instance == null)
        {
            if (GameMaster.instance != null)
            {
                instance = GameMaster.instance.GetEnemyManager();
            }
            else
            {
                Debug.Log("EnemyManager instance is null (GameMaster missing)");
            }
        }
        else
        {
            Debug.Log("EnemyManager Duplicate");
        }
        

        GameObject[] enemySpawnersTemp = GameObject.FindGameObjectsWithTag(enemySpawnerTag);
        enemySpawners = new EnemySpawner[enemySpawnersTemp.Length];
        for (int i = 0; i < enemySpawners.Length; i++)
        {
            enemySpawners[i] = enemySpawnersTemp[i].GetComponent<EnemySpawner>();
        }
        enemyCount = waveCount * enemySpawners.Length;

        enemyCountText = GameObject.FindGameObjectWithTag(enemyCounterTag).GetComponent<Text>() as Text;
        enemyCountText.text = "Enemies Left: " + enemyCount;
        
    }

    void Update()
    {
        enemyCountText.text = "Enemies Left: " + enemyCount;
        if (countdown <= 0 && waveCount > 0)
        {
            foreach (EnemySpawner enemySpawner in enemySpawners)
            {
                int rnd = Random.Range(0, 10) + 1;
                if (rnd > 9)
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

    public void ReportDeath()
    {
        //enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void ReportEnemyDeath()
    {
        enemyCount--;
        if(enemyCount <= 0)
        {
            GameMaster.instance.EndGame();
        }

        Debug.Log("enemycount " + enemyCount);
    }
}
