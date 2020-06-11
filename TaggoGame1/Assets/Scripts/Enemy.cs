﻿using System.Collections;
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

    [SerializeField] private GameObject target;
    [SerializeField] private NavMeshAgent m_NavMeshAgent;

    void Start()
    {
        UpdateTarget();
        
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= 4f)
        {
            Debug.Log("Reached destination");
            target.GetComponent<Home>().Damage(strength);
            RemoveEnemy();
            return;
        }

        //m_NavMeshAgent.Warp(transform.position);
        m_NavMeshAgent.SetDestination(target.transform.position);
        /*
        Vector3 dir = target.transform.position + offset - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);*/
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
            target = nearestTarget;
        }
        else
        {
            target = null;
        }
    }

    public void RemoveEnemy()
    {
        GameMaster.instance.ReportEnemyDeath();
        Destroy(gameObject);
        destroyed = true;
        return;
    }

    public void Damage(float points)
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
