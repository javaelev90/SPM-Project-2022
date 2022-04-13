using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Handler : MonoBehaviour
{
    public Text greenGoo;
    public int goo;
    public Text healthText;
    public int health;
    public Text metalText;
    public int metal;
    public Inventory inventory {get; set;}
    // Start is called before the first frame update
    void Start()
    {

    }
    IEnumerator FindInvetory(){
        while(!GameObject.FindGameObjectWithTag("Player") && !GameObject.FindGameObjectWithTag("Player").GetComponent<PhotonView>().IsMine){
            
            yield return new WaitForSeconds(2);
        }
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        if(inventory){
            greenGoo.text = "Green Goo : " + inventory.GreenGoo;
            healthText.text = "Health : " + inventory.AlienMeat;
            metalText.text = "Metal : " + inventory.Metal;
        }

    }
}
