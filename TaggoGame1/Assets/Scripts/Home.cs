using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public float hp = 1f;
    public Color destroyedColor;

    public void Damage(float points)
    {
        hp -= points;
        if(hp <= 0.001f)
        {
            Debug.Log("Took damage: " + points);
            GetComponent<Renderer>().material.color = destroyedColor;
        }
    }

}
