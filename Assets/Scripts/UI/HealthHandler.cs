using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HealthHandler : MonoBehaviour
{
    public Text healthText;
    public int healthMeter;
    // Start is called before the first frame update
    public HealthState hs {get; set;}

    void Start()
    {
        //StartCoroutine(FindHealthState());
        

    }
    IEnumerator FindHealthState(){
        while(!GameObject.FindGameObjectWithTag("Player") && !GameObject.FindGameObjectWithTag("Player").GetComponent<PhotonView>().IsMine){
            
            yield return new WaitForSeconds(2);
        }
        hs = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthState>();
        yield return null;
    }

    void Update()
    {
        if(hs){
            healthText.text = "Health : " + hs.Health;
        }

    }
}
