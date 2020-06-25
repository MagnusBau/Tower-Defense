using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [Header("Wave variables")]
    public float countdown = 5f;
    public float timeBetweenEnemies = 1f;
    public int waveCount = 5;

    [Header("Wave setup data")]
    public string enemySpawnerTag = "EnemySpawner";
    public string enemyCounterTag = "EnemyCounter";
    public Transform enemy;
    public Transform stronkEnemy;
    public Text enemyCountText;

    private EnemySpawner[] enemySpawners;
    private int enemyCount;
    private FileHandler fileHandler;
    private int waveNumber = 1;
    private List<string> wave;
    private float delay = 1f;
    private float delayStandard = 1f;
    private List<string> spawnerIndexes;
    private int waveLength;
    private int currentLineIndex = 0;
    private List<string> currentLine;
    private string switchCase;
    private Transform selectedEnemy;
    private bool waveOngoing = true;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("EnemyManager Duplicate!!!");
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        GameObject[] enemySpawnersTemp = GameObject.FindGameObjectsWithTag(enemySpawnerTag);
        enemySpawners = new EnemySpawner[enemySpawnersTemp.Length];
        for (int i = 0; i < enemySpawners.Length; i++)
        {
            enemySpawners[i] = enemySpawnersTemp[i].GetComponent<EnemySpawner>();
        }
        enemyCount = waveCount * enemySpawners.Length;

        enemyCountText = GameObject.FindGameObjectWithTag(enemyCounterTag).GetComponent<Text>() as Text;
        enemyCountText.text = "Enemies Left: " + enemyCount;
        fileHandler = new FileHandler();
        ReadNextWave();
    }

    void Update()
    {
        enemyCountText.text = "Enemies Left: " + enemyCount;
        delay -= Time.deltaTime;
        if (waveOngoing)
        {
            SpawnWave();
        }
    }

    public void ReadNextWave()
    {
        wave = fileHandler.ReadWave(waveNumber);
        waveLength = wave.Count - 1;
        waveNumber++;
    }

    void SpawnSingleEnemy(EnemySpawner spawner, Transform enemy)
    {
        spawner.SpawnEnemy(enemy);
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

    private Transform SelectEnemy()
    {
        switchCase = currentLine[3];
        switch (switchCase)
        {
            case "enemy":
                return enemy;
            case "stronkEnemy":
                return stronkEnemy;
            default:
                return enemy;
        }
    }
    private void Spawn()
    {
        selectedEnemy = SelectEnemy();
        spawnerIndexes = currentLine[1].Split('.').ToList();
        foreach (string spawner in spawnerIndexes)
        {
            int numberOfEnemies = int.Parse(currentLine[2]);
            for (int i = 0; i < numberOfEnemies; i++)
            {
                SpawnSingleEnemy(enemySpawners[int.Parse(spawner)], selectedEnemy);
            }
        }
    }
    public void SpawnWave()
    {
        if(currentLineIndex <= waveLength)
        {
            if(delay <= 0f)
            {
                currentLine = wave[currentLineIndex].Split(',').ToList();
                switchCase = currentLine[0];
                switch (switchCase)
                {
                    case "PAUSE":
                        delay = int.Parse(currentLine[1]) / 1000;
                        break;
                    case "DELAY":
                        delayStandard = int.Parse(currentLine[1]) / 1000;
                        delay = delayStandard;
                        break;
                    case "SPAWN":
                        Spawn();
                        delay = delayStandard;
                        break;
                    default:
                        Debug.LogError("Error interpreting wave file");
                        break;
                }
                currentLineIndex++;
            }
        }
        else
        {
            waveOngoing = false;
            ReadNextWave();
        }
    }
}
