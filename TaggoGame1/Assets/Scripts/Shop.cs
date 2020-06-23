using UnityEngine;

public class Shop : MonoBehaviour
{

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret()
    {
        Debug.Log(buildManager);
        buildManager.SetBuildingToBuild(buildManager.DefaultBuildingPrefab, buildManager.defaultturretPrice);
    }

    public void SelectWall()
    {
        Debug.Log("Wall Purchased");
        buildManager.SetBuildingToBuild(buildManager.WallPrefab, buildManager.wallPrice);
    }

    public void SelectBitcoin()
    {
        Debug.Log("Bitcoin Purchased");
        buildManager.SetBuildingToBuild(buildManager.bitcoinPrefab, buildManager.bitcoinPrice);
    }

   
}
