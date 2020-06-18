using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StronkEnemy : Enemy
{
    void Start()
    {
        SetAttributes(4f, 2f);
        FindDestination();
    }
}
