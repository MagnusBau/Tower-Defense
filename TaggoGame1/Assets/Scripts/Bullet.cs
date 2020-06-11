using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 1f;
    public float speed = 70f;
    private GameObject target;

    public void SetTarget(GameObject _target)
    {
        target = _target;
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
                target.GetComponent<Enemy>().TakeDamage(damage);
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

    private void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        Move();
    }
}
