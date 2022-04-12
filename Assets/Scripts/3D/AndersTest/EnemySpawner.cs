using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawner : MonoBehaviourPunCallbacks
{

    [SerializeField] GameObject enemyToSpawn;
    [Range(1, 100)]
    [SerializeField] int numberOfEnemies;
    [Tooltip("Delay between enemy spawns in seconds")]
    [SerializeField] float delayTime = 0.5f;
    Coroutine enemySpawner;

    public void StartSpawningEnemies()
    {
        enemySpawner = StartCoroutine(SpawnEnemies());
    }

    public void StopSpawningEnemies()
    {
        if (enemySpawner != null) StopCoroutine(enemySpawner);
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            PhotonNetwork.InstantiateRoomObject(enemyToSpawn.name, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(delayTime);
        }
        yield return null;
    }

}
