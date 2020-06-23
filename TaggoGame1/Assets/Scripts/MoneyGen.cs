using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyGen : MonoBehaviour
{
    public float amountPerGeneration = 69f;
    public float generationRate = 0.3f;
    public float cooldown;
    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
        cooldown = 1 / generationRate;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0f)
        {
            GenerateMoney();
        }
    }

    public void GenerateMoney()
    {
        buildManager.AddCurrency(amountPerGeneration);
        cooldown = 1 / generationRate;
        Debug.Log("Building money has been gen");
    }
}
