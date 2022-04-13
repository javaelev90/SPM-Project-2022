using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenGooPickup : MonoBehaviour
{
    public Handler handler;
    // Start is called before the first frame update
    void Start()
    {
        handler = GameObject.Find("Canvas").GetComponent<Handler>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(){
        handler.goo++;
    }
}
