using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    public float hp = 15f;
    public Color destroyedColor;


    public Text hpCounterText;

    void Start()
    {
        hpCounterText.text = hp.ToString();
    }
    public void Damage(float points)
    {
        hp -= points;
        if(hp <= 0.001f)
        {
            Debug.Log("Took damage: " + points);
            GetComponent<Renderer>().material.color = destroyedColor;
        }
        hpCounterText.text = hp.ToString();
    }

}
