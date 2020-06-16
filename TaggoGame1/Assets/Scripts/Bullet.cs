using UnityEngine;

public class Bullet : DestroyableObject
{
    public float damage = 1f;
    public float speed = 70f;
    private DestroyableObject target;

    void Start()
    {
        SetAttributes(1f, 1f);
    }
    public void SetTarget(GameObject _target)
    {
        target = _target.GetComponent<DestroyableObject>();
    }

    private void Move()
    {
        float distancePerFrame = speed * Time.deltaTime;
        if(target != null)
        {
            Vector3 dir = (target.transform.position - transform.position).normalized;
            transform.Translate(dir * distancePerFrame, Space.World);
            if(Vector3.Distance(transform.position, target.transform.position) < 0.4f)
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
                return;
            }
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        Move();
    }
}
