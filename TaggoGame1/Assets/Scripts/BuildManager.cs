using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public float balance;

    [Header("Prices")]
    public float defaultturretPrice;
    public float wallPrice;
    public float bitcoinPrice;

    [Header("Buildings")]
    public GameObject DefaultBuildingPrefab;
    public GameObject WallPrefab;
    public GameObject bitcoinPrefab;
    private GameObject BuildingToBuild;
   
    private float priceForBuilding;
    private Text balanceDisplay;

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
        balanceDisplay = GameObject.FindGameObjectWithTag("BalanceDisplay").GetComponent<Text>() as Text;
        balanceDisplay.text = "$" + balance.ToString();
    }

  

    void Update()
    {
        balanceDisplay.text = "$" + balance.ToString();
    }

    public float GetPriceForBuilding()
    {
        return priceForBuilding;
    }


    public GameObject GetBuildingToBuild()
    {
        return BuildingToBuild;
    }

    public void SetBuildingToBuild(GameObject bulding, float price)
    {
        BuildingToBuild = bulding;
        priceForBuilding = price;
    }

    public bool UpdateBalance()
    {
        if(priceForBuilding <= balance && balance >= 0)
        {
            balance -= priceForBuilding;
            Debug.Log(balance);
            return true;
        }
        else
        {
            Debug.Log("Ikke nok pæng");
            return false;
        }
    }

    public void AddCurrency(float amount)
    {
        balance += amount;
    }
}

