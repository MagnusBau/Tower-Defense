using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 offset;

    private GameObject building;
    private Renderer rend;
    private Color defaultColor;

    private void Start()
    {
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
        GameObject buildingToBuild = BuildManager.instance.GetBuildingToBuild();
        building = (GameObject)Instantiate(buildingToBuild, transform.position + offset, transform.rotation);
    }
}
