using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    public Transform enemyManagarPrefab;

    private EnemyManager enemyManager;

    void Awake()
    {
        if (instance != null)
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
        enemyManager = Instantiate(enemyManagarPrefab).GetComponent<EnemyManager>();
    }

    void Update()
    {
        
    }

    public EnemyManager GetEnemyManager()
    {
        return enemyManager;
    }
}
