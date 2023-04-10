using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class EnemyInvoker : NetworkBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    [Networked] private TickTimer _spawnDelay { get; set; }

    TickTimer spawnDelay; 

    private void Start()
    {

        spawnDelay = TickTimer.CreateFromSeconds(Runner, 2);

        //StartCoroutine(SpawnEnemies());
        //Runner.Spawn(enemyPrefab, new Vector3(Random.Range(0, 20), Random.Range(0, 20), 0), Quaternion.identity, PlayerRef.None);
    }


    public override void FixedUpdateNetwork()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            spawnDelay = TickTimer.CreateFromSeconds(Runner, 2);
        }


        if (spawnDelay.Expired(Runner) == true)
        {
            if (HasStateAuthority)
            {

                Runner.Spawn(enemyPrefab, new Vector3(Random.Range(-27, 27), Random.Range(-16, 16), 0), Quaternion.identity, PlayerRef.None);
                spawnDelay = TickTimer.CreateFromSeconds(Runner, 2);
            }
        }
    }

    /*private void Update()
    {
        Runner.Spawn(enemyPrefab, new Vector3(Random.Range(-27, 27), Random.Range(-16, 16), 0), Quaternion.identity, PlayerRef.None);
    }*/

    /*private IEnumerator SpawnEnemies()
    {
        while(true)
        {

            yield return new WaitForSeconds(2);

            Runner.Spawn(enemyPrefab, new Vector3(Random.Range(0, 20), Random.Range(0, 20), 0), Quaternion.identity,PlayerRef.None);
            
            //Instantiate(enemyPrefab, new Vector3(Random.Range(0, 20), Random.Range(0, 20), 0), Quaternion.identity);
        }
    }*/

}
