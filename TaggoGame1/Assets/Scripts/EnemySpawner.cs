using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Vector3 offset;
    public void SpawnEnemy(Transform enemy)
    {
        Instantiate(enemy, transform.position + offset, transform.rotation);
    }
}
