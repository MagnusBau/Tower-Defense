using UnityEngine;

public class Turret : DestroyableObject
{
    [Header("Attributes")]
    public float range = 15f;
    public float turnSpeed = 10f;
    public float fireRate = 1f;

    [Header("Unity setup parameters")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public GameObject bulletPrefab;
    public Transform shootPoint;

    private float cooldown = 0f;
    private GameObject target;

    void Start()
    {
        SetAttributes(10f, 0f);
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    private void UpdateTarget()
    {
        float shortestDistance = Mathf.Infinity;
        GameObject closestenemy = null;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        foreach (var enemy in enemies)
        {
            float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (enemyDistance < shortestDistance && enemyDistance <= range)
            {
                shortestDistance = enemyDistance;
                closestenemy = enemy;
            }
        }

        target = closestenemy;
    }

    private void Rotate()
    {
        Vector3 dir = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.SetTarget(target);
        }
        cooldown = 1 / fireRate;
    }

    private void Update()
    {
        cooldown -= Time.deltaTime;
        if (target == null)
        {
            return;
        }
        Rotate();
        if (cooldown <= 0f)
        {
            Shoot();
        }
    }
}