using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public static WallManager instance;

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
        Vector3 dir = w1.transform.position - w2.transform.position;
        Vector3 pos = (w1.transform.position - dir / 2f);
        Quaternion rot = Quaternion.LookRotation(dir);
        WallFiller newWallFiller = Instantiate(wallFiller, pos, rot);
        newWallFiller.Set(w1, w2);


    }
}
