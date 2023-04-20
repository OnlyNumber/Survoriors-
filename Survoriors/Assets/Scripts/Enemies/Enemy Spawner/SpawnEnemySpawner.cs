using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;


public class SpawnEnemySpawner : NetworkBehaviour
{
    [SerializeField]
    private EnemySpawner enemySpawner;

    [SerializeField]
    private Button startButton; 

    public override void FixedUpdateNetwork()
    {

        if (HasStateAuthority && Runner != null)
        {
            startButton.gameObject.SetActive(true);
            startButton.onClick.AddListener(enemySpawner.StartGame);

            Runner.Despawn(GetComponent<NetworkObject>());
        }
        else if(!HasStateAuthority && Runner != null)
        {
            Runner.Despawn(GetComponent<NetworkObject>());
        }

    }
}
