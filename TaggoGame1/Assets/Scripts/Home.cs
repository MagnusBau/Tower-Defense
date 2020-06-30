using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    public float hp = 15f;
    public Color destroyedColor;
    public float amountPerGeneration = 100f;
    public float generationRate = 0.5f;
    public float cooldown;
    BuildManager buildManager;
    EnemyManager enemyManager;


    public TextMesh hpCounterText;

    void Start()
    {
        hpCounterText.text = hp.ToString();
        buildManager = BuildManager.instance;
        enemyManager = EnemyManager.instance;
        cooldown = 1 / generationRate;
    }

    private void Update()
    {
        if(enemyManager.GetEnemyCount() > 0 || enemyManager.GetWaveSpawning())
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0f)
            {
                GenerateMoney();
            }
        }
    }
    public void Damage(float points)
    {
        hp -= points;
        if(hp <= 0.001f)
        {
            Debug.Log("Took damage: " + points);
            GetComponent<Renderer>().material.color = destroyedColor;
        }
        hpCounterText.text = hp.ToString();
    }

    public void GenerateMoney()
    {
        buildManager.AddCurrency(amountPerGeneration);
        cooldown = 1 / generationRate;
        Debug.Log("Money!");
    }
}
