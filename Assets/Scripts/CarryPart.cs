using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryPart : MonoBehaviour
{
    /// <summary>
    /// PLAYER NEED A CHILD OBJECT CALLED CarryPos
    /// </summary>
    [SerializeField] private Transform destination;
    private Transform player;
    [SerializeField] private float radius = 3f;

    void Start()
    {
        StartCoroutine(Wait(5));
        //Wait(5);
    }

    /*
    private void OnMouseDown()
    {
        if(player != null)
        {
            destination = player.transform.Find("CarryPos");
            //GetComponent<Rigidbody>().useGravity = false;
            this.transform.position = destination.position;
            this.transform.parent = GameObject.Find("CarryPos").transform;
        }

    }
    */

    private void Update()
    {
        if(player != null)
        {
            Collider[] colliderHits = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider col in colliderHits)
            {
                if (col.tag == ("Player") && Input.GetKeyDown(KeyCode.K))
                {
                    Debug.Log("Inside");
                    destination = player.transform.Find("CarryPos");
                    //GetComponent<Rigidbody>().useGravity = false;
                    this.transform.position = destination.position;
                    this.transform.parent = GameObject.Find("CarryPos").transform;
                }
                if (Input.GetKeyDown(KeyCode.J))
                {
                    this.transform.parent = null;
                    //GetComponent<Rigidbody>().useGravity = true;
                }
                else
                {
                    //Debug.Log("Outside");
                }
            }

        }
       


        

        /*
        // Replaced with when entering range of ship
        if (Input.GetKeyDown(KeyCode.O))
        {
            Destroy(this.gameObject);
            Debug.Log("You've collected a part for the ship");
        }
        */
    }

    /*
    private void OnMouseUp()
    {
        this.transform.parent = null;
        //GetComponent<Rigidbody>().useGravity = true;
    }
    */

    IEnumerator Wait(float sec)
    {
        while (player == null)
        {
            yield return new WaitForSeconds(sec);
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

    }

}
