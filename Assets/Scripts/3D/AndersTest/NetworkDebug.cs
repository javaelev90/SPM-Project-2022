using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class NetworkDebug : MonoBehaviourPunCallbacks
{

    [SerializeField] Text isMasterText;

    private void Update() {
        isMasterText.text = "Is master: " + PhotonNetwork.IsMasterClient;
    }
}
