using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform spawnPosition;
    [SerializeField] private PhotonView playerPrefab;
    [SerializeField] private ObjectInstantiater objectInstantiater;
    private string pathPlayer = "Prefab/Player/PlayerwWeapon";
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.GameVersion = "0.1";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions roomOptions = new RoomOptions();
        print("Connected");
        PhotonNetwork.JoinOrCreateRoom("Room1", roomOptions, null, null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room successfully!");
        //PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        PhotonNetwork.Instantiate(pathPlayer, spawnPosition.position, Quaternion.identity);
        if (PhotonNetwork.IsMasterClient)
        {
            objectInstantiater.InitializeWorld();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected " + cause.ToString());
    }
}
