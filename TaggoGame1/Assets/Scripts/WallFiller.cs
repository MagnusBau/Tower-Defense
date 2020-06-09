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
}
