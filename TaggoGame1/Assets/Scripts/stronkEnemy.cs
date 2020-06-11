using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stronkEnemy : Enemy
{
    void Start()
    {
        SetAttributes(4f, 2f);
        FindDestination();
    }
}
