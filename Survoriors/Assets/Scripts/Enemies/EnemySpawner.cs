using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;

    NetworkRunner networkRunner;

    //NetworkObject netObj;


    private void Start()
    {
        if (networkRunner == null)
        {
            networkRunner = FindObjectOfType<NetworkRunner>();
        }

        
        StartCoroutine(SpawnEnemy());

    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(1);

        networkRunner = FindObjectOfType<NetworkRunner>();

        networkRunner.Spawn(enemy, null, null, null/*(runner, netObj) => { enemy.GetComponent<EnemyNetwork>().InitializeNetwork(networkRunner); }*/);

    }


}
