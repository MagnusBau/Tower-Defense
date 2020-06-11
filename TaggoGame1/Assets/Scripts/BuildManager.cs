using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public GameObject DefaultBuilding;
    private GameObject BuildingToBuild;

    public GameObject GetBuildingToBuild()
    {
        return BuildingToBuild;
    }
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one Build Manager!");
        }
    }

    private void Start()
    {
        BuildingToBuild = DefaultBuilding;
    }
}

