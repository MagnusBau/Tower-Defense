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
    private float cooldown = 0f;
    public float fireRate = 1f;
    public float fireRadius = 1f;

    [SerializeField] private DestroyableObject _target;
    [SerializeField] private GameObject _destination;
    [SerializeField] private NavMeshAgent m_NavMeshAgent;


    void Start()
    {
        m_NavMeshAgent.autoTraverseOffMeshLink = false;
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
        cooldown -= Time.deltaTime;
        if (Vector3.Distance(transform.position, _destination.transform.position) <= 3f)
        {
            Debug.Log("Reached destination");
            _destination.GetComponent<Home>().Damage(strength);
            RemoveEnemy();
            return;
        }
        FindPath();
        if(cooldown <= 0)
        {
            if(m_NavMeshAgent.isOnOffMeshLink)
            {
                OffMeshLink currentOffMeshLink = m_NavMeshAgent.currentOffMeshLinkData.offMeshLink;
                if(currentOffMeshLink != null)
                {
                    WallFiller wallFiller = currentOffMeshLink.gameObject.transform.parent.GetComponent<WallFiller>();
                    if(wallFiller != null)
                    {
                        wallFiller.TakeDamage(strength);
                        cooldown = 1 / fireRate;
                    }
                }
                else
                {
                    m_NavMeshAgent.CompleteOffMeshLink();
                }
            }
            else if(m_NavMeshAgent.nextOffMeshLinkData.valid)
            {
                OffMeshLink nextOffMeshLink = m_NavMeshAgent.nextOffMeshLinkData.offMeshLink;
                float distanceToNextOML = Vector3.Distance(transform.position, nextOffMeshLink.transform.position);
                if(nextOffMeshLink != null && distanceToNextOML <= fireRadius)
                {
                    WallFiller wallFiller = nextOffMeshLink.gameObject.transform.parent.GetComponent<WallFiller>();
                    if(wallFiller != null)
                    {
                        wallFiller.TakeDamage(strength);
                        cooldown = 1 / fireRate;
                    }
                }
            }
        }
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
