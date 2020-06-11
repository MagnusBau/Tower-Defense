using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public float hp = 1f;
    public float strength = 1f;

    public string homeTag = "Home";
    public Vector3 offset;

    private bool destroyed = false;

    [SerializeField] private GameObject _destination;
    [SerializeField] private NavMeshAgent m_NavMeshAgent;

    void Start()
    {
        UpdateTarget();
        
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, _destination.transform.position) <= 4f)
        {
            Debug.Log("Reached destination");
            _destination.GetComponent<Home>().Damage(strength);
            RemoveEnemy();
            return;
        }

        m_NavMeshAgent.SetDestination(_destination.transform.position);
    }

    void UpdateTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(homeTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestTarget = null;

        foreach (GameObject target in targets)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                nearestTarget = target;
                shortestDistance = distanceToEnemy;
            }

        }
        if (nearestTarget != null)
        {
            _destination = nearestTarget;
        }
        else
        {
            _destination = null;
        }
    }

    public void RemoveEnemy()
    {
        EnemyManager.instance.ReportEnemyDeath();
        Destroy(gameObject);
        return;
    }

    public void Damage()
    {

    }

    public void TakeDamage(float points)
    {
        hp -= points;
        if(hp <= 0f)
        {
            if (!destroyed)
            {
                destroyed = true;
                RemoveEnemy();
                return;
            }
        }
    }
}
