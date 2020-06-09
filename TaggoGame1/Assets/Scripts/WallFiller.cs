using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFiller : MonoBehaviour
{
    private Wall wall1;
    private Wall wall2;

    public void Set(Wall w1, Wall w2)
    {
        wall1 = w1;
        wall2 = w2;
    }

    public bool EqualWalls(Wall w1, Wall w2)
    {
        if(wall1 == w1 || wall1 == w2)
        {
            if (wall2 == w1 || wall2 == w2)
            {
                return true;
            }
        }
        return false;
    }

    public bool ContainsWall(Wall w1)
    {
        if (wall1 == w1 || wall2 == w1)
        {
            return true;
        }
        return false;
    }
}
