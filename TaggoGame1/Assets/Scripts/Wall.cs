using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class Wall : MonoBehaviour
{
    public float tileWidth = 4;
    public float tileDivider = 1;
    public string wallTag = "Wall";

    public Transform wallFiller;

    void Start()
    {
        float maxDistToNeighbour = (tileWidth + tileDivider)*1.25f;
        GameObject[] walls = GameObject.FindGameObjectsWithTag(wallTag);
        foreach(GameObject wall in walls)
        {
            float distanceToWall = UnityEngine.Vector3.Distance(transform.position, wall.transform.position);
            if (!wall.Equals(gameObject) && distanceToWall < maxDistToNeighbour)
            {
                WallManager.instance.FillWall(gameObject.GetComponent<Wall>(), wall.GetComponent<Wall>());
            }
        }
    }

    private void OnDestroy()
    {
        WallManager.instance.ReportWallRemoved(gameObject.GetComponent<Wall>());
    }
}
