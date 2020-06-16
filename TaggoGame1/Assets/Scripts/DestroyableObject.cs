using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    public float hp;
    public float strength;

    private bool destroyed = false;


    public void SetAttributes(float _hp, float _strength)
    {
        hp = _hp;
        strength = _strength;
    }
    public void RemoveObject()
    {
        destroyed = true;
        Destroy(gameObject);
        return;
    }

    public void TakeDamage(float points)
    {
        hp -= points;
        if (hp <= 0f && !destroyed)
        {
            RemoveObject();
            return;
        }
    }

    public void DoDamage(DestroyableObject target)
    {
        target.TakeDamage(strength);
        return;
    }
}
