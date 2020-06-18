using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public float balance;
    public Text balanceDisplay;

    [Header("Prices")]
    public float defaultturretPrice;
    public float wallPrice;

    [Header("Buildings")]
    public GameObject DefaultBuildingPrefab;
    public GameObject WallPrefab;
    private GameObject BuildingToBuild;
    private float priceForBuilding;

    public GameObject GetBuildingToBuild()
    {
        return BuildingToBuild;
    }

    public void SetTurretToBuild(GameObject bulding, float price)
    {
        BuildingToBuild = bulding;
        priceForBuilding = price;
    }

    public void UpdateBalance()
    {
        if(priceForBuilding <= balance && balance >= 0)
        {
            balance -= priceForBuilding;
            Debug.Log(balance);
        }
        else
        {
            //TODO: legg til melding som sier at man ikke har råd
            return;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("BuildManager Duplicate!!!");
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        Debug.Log("Hei fra start");
        balanceDisplay.text = "$" + balance.ToString();
    }

    void Update()
    {
        balanceDisplay.text = "$" + balance.ToString();
    }
}

