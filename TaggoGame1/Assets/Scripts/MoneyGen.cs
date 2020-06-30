using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyGen : MonoBehaviour
{
    public float amountPerGeneration = 69f;
    public float generationRate = 0.3f;
    public float cooldown;
    BuildManager buildManager;
    EnemyManager enemyManager;

    void Start()
    {
        buildManager = BuildManager.instance;
        enemyManager = EnemyManager.instance;
        cooldown = 1 / generationRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyManager.GetEnemyCount() > 0 || enemyManager.GetWaveSpawning())
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0f)
            {
                GenerateMoney();
            }
        }
    }

    public void GenerateMoney()
    {
        buildManager.AddCurrency(amountPerGeneration);
        cooldown = 1 / generationRate;
        Debug.Log("Building money has been gen");
    }
}
