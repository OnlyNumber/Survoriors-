using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class EnemySpawner : NetworkBehaviour
{
    private bool isWaveNow;

    [SerializeField]
    private List<EnemyWave> _enemyWaves = new List<EnemyWave>();

    private int _numberOfWave;

    private TimerControl timer;

    [SerializeField]
    private LeaderBoardShowInformation leaderBoardShowInformation;

    [SerializeField]
    private List<Transform> _spawnPoints;

    SpawnerPlayer spawnerPlayer;


    private void Start()
    {
        _numberOfWave = 0;

        timer = GameObject.Find("Timer Indicator").GetComponent<TimerControl>();

    }

    public void StartGame()
    {
        StartCoroutine(WaveTimer());
    }

    private IEnumerator WaveTimer()
    {
        isWaveNow = true;

        StartCoroutine(SpawnWave());

        StartCoroutine(SpawnItems());

        Debug.Log((int)_enemyWaves[_numberOfWave].waveDuration);

        Rpc_RequestChangeTimer();

        yield return new WaitForSecondsRealtime(_enemyWaves[_numberOfWave].waveDuration);

        isWaveNow = false;
        Rpc_RequestChangeWave();

        StopAllCoroutines();

        if (_numberOfWave < _enemyWaves.Count)
        {
            StartCoroutine(WaveTimer());
        }
        else
        {
            ShowLeaderBoard();
        }
    }



    IEnumerator SpawnWave()
    {
        while(isWaveNow)
        { 
            yield return new WaitForSeconds(_enemyWaves[_numberOfWave].timeBetweenWaveEnemy);

            //Debug.Log(_enemyWaves[_numberOfWave].enemiesCount);

            for (int i = 0; i < _enemyWaves[_numberOfWave].enemiesCount; i++)
            {
                Debug.Log("Spawn");
                if (_enemyWaves[_numberOfWave].enemyList.Count != 0)
                    Runner.Spawn(_enemyWaves[_numberOfWave].enemyList[Random.Range(0, _enemyWaves[_numberOfWave].enemyList.Count)], _spawnPoints[Random.Range(0, _spawnPoints.Count)].position, Quaternion.identity);
            }
        }
    }

    IEnumerator SpawnItems()
    {
        while (isWaveNow)
        {
            yield return new WaitForSeconds(_enemyWaves[_numberOfWave].timeBetweenItem);

            if(_enemyWaves[_numberOfWave].pickupItemsList.Count != 0)
                Runner.Spawn(_enemyWaves[_numberOfWave].pickupItemsList[Random.Range(0, _enemyWaves[_numberOfWave].pickupItemsList.Count)], new Vector3(Random.Range(-28, 28), Random.Range(-15, 15)), Quaternion.identity);
        }

    }

    [Rpc]
    private void Rpc_RequestChangeTimer()
    {
        timer.SetTimer((int)_enemyWaves[_numberOfWave].waveDuration);
    }

    [Rpc]
    private void Rpc_RequestChangeWave()
    {
        _numberOfWave++;
    }

    private void ShowLeaderBoard()
    {
        if(HasStateAuthority)
        leaderBoardShowInformation.ShowTable();
    }
    public void CheckAllPlayer()
    {
        if(CheckAlivePlayers())
        {
            return;
        }

        StopAllCoroutines();
        ShowLeaderBoard();
    }
    
    public bool CheckAlivePlayers()
    {
        if (spawnerPlayer == null)
        {
            spawnerPlayer = FindObjectOfType<SpawnerPlayer>();
        }

        int i = 0;

        foreach (var item in spawnerPlayer.GetSpawnedPlayers().Values)
        {
            if (item.gameObject.activeInHierarchy == false)
            {
                i++;
            }
        }

        

        if (i == spawnerPlayer.GetSpawnedPlayers().Count )
        {
            return false;
        }

        return true;
    }



}
