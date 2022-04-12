using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text uiTimer;
    [SerializeField] private bool isRunning;
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            timer += Time.deltaTime;
            uiTimer.text = (timer / 60).ToString();
        }
    }
}
