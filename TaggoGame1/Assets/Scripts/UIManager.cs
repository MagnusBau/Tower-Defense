using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private EnemyManager enemyManager;
    void Start()
    {
        enemyManager = EnemyManager.instance;
    }

    public void NextWave()
    {
        enemyManager.NextWave();
    }
}
