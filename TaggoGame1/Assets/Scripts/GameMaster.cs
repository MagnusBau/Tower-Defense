﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        }
    }
    void Start()
    {
        Debug.Log("Hei fra gamemaster start");
        enemyManager = Instantiate(enemyManagerPrefab).GetComponent<EnemyManager>();
        buildManager = Instantiate(buildManagerPrefab).GetComponent<BuildManager>();
    }

    void Update()
    {
        
    }

    public EnemyManager GetEnemyManager()
    {
        return enemyManager;
    }

    public BuildManager GetBuildManager()
    {
        return buildManager;
    }
}
