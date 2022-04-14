using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LookAtPlayer : MonoBehaviourPunCallbacks
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FindPlayer());
    }

    IEnumerator FindPlayer()
    {
        while (!GameObject.FindGameObjectWithTag("Player") && !GameObject.FindGameObjectWithTag("Player").GetComponent<PhotonView>().IsMine)
        {

            yield return new WaitForSeconds(2);
        }
        player = GameObject.FindGameObjectWithTag("Player");
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        if(player)
        {
            transform.LookAt(player.transform);
        }
    }
}
