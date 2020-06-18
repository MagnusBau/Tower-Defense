using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Animations;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;

    private GameObject building;
    private Renderer rend;
    private Color defaultColor;
    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();
        defaultColor = rend.material.color;
    }

    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = defaultColor;
    }

    private void OnMouseDown()
    {
        if(building != null)
        {
            return;
        }
        if(buildManager.GetBuildingToBuild() == null)
        {
            return;
        }
        Vector3 offset = new Vector3(0f, 0f, 0f);
        GameObject buildingToBuild = buildManager.GetBuildingToBuild();
        foreach(Transform child in buildingToBuild.transform)
        {
            if(child.name == "Tail")
            {
                offset = child.position;
            }
        }
        building = (GameObject)Instantiate(buildingToBuild, transform.position + offset, transform.rotation);
        buildManager.UpdateBalance();
    }
}
