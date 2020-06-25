using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SceneManagement;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public static WallManager instance;

    private List<WallFiller> wallFillers = new List<WallFiller>();

    public WallFiller wallFiller;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("WallManager Duplicate!!!");
        }
        else
        {
            instance = this;
        }
    }

    public void FillWall(Wall w1, Wall w2)
    {
        foreach(WallFiller wallFiller in wallFillers)
        {
            if(wallFiller.EqualWalls(w1, w2))
            {
                return;
            }
        }
        Vector3 dir = w1.transform.position - w2.transform.position;
        Vector3 pos = (w1.transform.position - dir / 2f);
        Quaternion rot = Quaternion.LookRotation(dir);
        WallFiller newWallFiller = Instantiate(wallFiller, pos, rot);
        newWallFiller.Set(w1, w2);
        wallFillers.Add(newWallFiller);
    }

    public void ReportWallRemoved(Wall w1)
    {
        Debug.Log("wallFillers count " + wallFillers.Count);
        for (int i = wallFillers.Count - 1; i >= 0; i--)
        {
            Debug.Log("Checking one");
            if (wallFillers[i].ContainsWall(w1))
            {
                Debug.Log("Destroying one");
                Destroy(wallFillers[i].gameObject);
                wallFillers.RemoveAt(i);
            }
        }
    }
}
