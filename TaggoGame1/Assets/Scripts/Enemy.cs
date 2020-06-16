using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : DestroyableObject
{
    public float speed = 10f;

    public string homeTag = "Home";
    public Vector3 offset;

    private bool destroyed = false;

    [SerializeField] private DestroyableObject _target;
    [SerializeField] private GameObject _destination;
    [SerializeField] private NavMeshAgent m_NavMeshAgent;

    void Start()
    {
        SetAttributes(2f, 1f);
        FindDestination();
    }

    void FindPath()
    {
        m_NavMeshAgent.SetDestination(_destination.transform.position);
        if(m_NavMeshAgent.hasPath)
        {
            return;
        }
        else
        {
            //m_NavMeshAgent.
            return;
        }
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, _destination.transform.position) <= 3f)
        {
            Debug.Log("Reached destination");
            _destination.GetComponent<Home>().Damage(strength);
            RemoveEnemy();
            return;
        }
        FindPath();
    }

    public void FindDestination()
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
        destroyed = true;
        EnemyManager.instance.ReportEnemyDeath();
        Destroy(gameObject);
        return;
    }

    public void Damage()
    {
        _target.TakeDamage(strength);
    }

    public void TakeDamage(float points)
    {
        hp -= points;
        if(hp <= 0f)
        {
            if (!destroyed)
            {
                RemoveEnemy();
                return;
            }
        }
    }
}
