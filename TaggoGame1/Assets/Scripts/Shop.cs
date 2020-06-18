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
        buildManager.SetTurretToBuild(buildManager.DefaultBuildingPrefab, buildManager.defaultturretPrice);
    }

    public void SelectWall()
    {
        Debug.Log("Wall Purchased");
        buildManager.SetTurretToBuild(buildManager.WallPrefab, buildManager.wallPrice);
    }
}
