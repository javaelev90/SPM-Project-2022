using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Handler : MonoBehaviour
{
    public Text greenGoo;
    public int goo;
    public Text healthText;
    public int health;
    public Text metalText;
    public int metal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        greenGoo.text = "Green Goo : " + goo;
        healthText.text = "Health : " + health;
        metalText.text = "Metal : " + metal;
    }
}
