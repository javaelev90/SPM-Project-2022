using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounter : MonoBehaviour
{
    private Text enemyCountText;
    private float updateCooldown = 0.2f;
    private float lastUpdateTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        enemyCountText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > lastUpdateTime + updateCooldown)
        {
            lastUpdateTime = Time.time;
            enemyCountText.text = "Number of enemies: " + GameObject.FindGameObjectsWithTag("Enemy").Length;
        }
    }
}
