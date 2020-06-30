using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    public Transform enemyManagerPrefab;
    public Transform buildManagerPrefab;

    private EnemyManager enemyManager;
    private BuildManager buildManager;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("GameMaster Duplicate!!!");
        }
        else
        {
            instance = this;
            enemyManager = Instantiate(enemyManagerPrefab).GetComponent<EnemyManager>();
            buildManager = Instantiate(buildManagerPrefab).GetComponent<BuildManager>();
        }
    }
    void Start()
    {
        Debug.Log("Hei fra gamemaster start");
        
    }

    void Update()
    {
        
    }

    public GameObject completLevelUI;

    public void Completelevel()
    {
        Debug.Log("LVL WON ");
        completLevelUI.SetActive(true);
    }

    bool gameHasEnded = false;
    //public float restartDelay = 1f;

    public GameObject GameOverUI;
    public void GameOver()
    {
        if (gameHasEnded == false)
        {
            GameOverUI.SetActive(true);
            gameHasEnded = true;
            Debug.Log("Game over!");
            //Invoke("Restart", restartDelay);
            //restart()
        }
    }

    /*
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    */

    public EnemyManager GetEnemyManager()
    {
        return enemyManager;
    }

    public BuildManager GetBuildManager()
    {
        return buildManager;
    }
/*
    public void EndGame()
    {
        Application.LoadLevel(0);
    }
    */
}
