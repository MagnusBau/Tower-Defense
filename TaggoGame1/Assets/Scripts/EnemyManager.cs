using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [Header("Wave setup data")]
    public string enemySpawnerTag = "EnemySpawner";
    public string enemyCounterTag = "EnemyCounter";
    public string nextWaveButtonTag = "NextWaveButton";
    public Transform enemy;
    public Transform stronkEnemy;
    public Text enemyCountText;
    public GameObject nextWaveButton;

    private EnemySpawner[] enemySpawners;
    private FileHandler fileHandler;
    private List<string> wave;
    private List<string> spawnerIndexes;
    private List<string> currentLine;
    private Transform selectedEnemy;
    private float delay = 1f;
    private float delayStandard = 1f;
    private int currentWave = 0;
    private int enemyCount = 0;
    private int waveLength;
    private int currentLineIndex = 0;
    private int numberOfWaves;
    private string switchCase;
    private bool waveSpawning = false;


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
        enemyCountText = GameObject.FindGameObjectWithTag(enemyCounterTag).GetComponent<Text>() as Text;
        nextWaveButton = GameObject.FindGameObjectWithTag(nextWaveButtonTag);
        enemyCountText.text = "Enemies Left: " + enemyCount;
        fileHandler = new FileHandler();
        numberOfWaves = fileHandler.FindWaveCount();
        ReadNextWave();
    }

    void Update()
    {
        enemyCountText.text = "Enemies Left: " + enemyCount;
        delay -= Time.deltaTime;
        if(currentWave <= numberOfWaves)
        {
            if (waveSpawning)
            {
                SpawnWave();
            }
            else if (enemyCount <= 0)
            {
                nextWaveButton.SetActive(true);
            }
        }
        else if (enemyCount <= 0)
        {
            //endscreen
            GameMaster.instance.EndGame();
        }
    }

    public void ReadNextWave()
    {
        currentWave++;
        if(currentWave <= numberOfWaves)
        {
            wave = fileHandler.ReadWave(currentWave);
        }
        waveLength = wave.Count - 1;
        currentLineIndex = 0;
    }

    void SpawnSingleEnemy(EnemySpawner spawner, Transform enemy)
    {
        spawner.SpawnEnemy(enemy);
        enemyCount++;
    }

    public void ReportEnemyDeath()
    {
        enemyCount--;

        Debug.Log("enemycount " + enemyCount);
    }

    public void NextWave()
    {
        waveSpawning = true;
        nextWaveButton.SetActive(false);
    }

    /*
     * Selects what enemy to spawn
     */
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

    /*
     * Used to spawn enemies from a single line in the wave file
     */
    private void SpawnLine()
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

    /*
     * Method used to spawn an entire wave
     */
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
                        SpawnLine();
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
            waveSpawning = false;
            ReadNextWave();
        }
    }

    public int GetEnemyCount()
    {
        return enemyCount;
    }

    public bool GetWaveSpawning()
    {
        return waveSpawning;
    }
}
