using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;

    NetworkRunner networkRunner;

    private bool isWaveNow;

    private bool isGame;

    private Button startButton;

    //[SerializeField]
    //private List<GameObject> _enemyWaveGameObjects = new List<GameObject>();

    [SerializeField]
    private List<EnemyWave> _enemyWaves = new List<EnemyWave>();

    private List<Transform> _spawnPoints;

    //NetworkObject netObj;
    private int _numberOfWave;

    private TimerControl timer;

    private void Start()
    {
        _numberOfWave = 0;

        timer = GameObject.Find("Timer Indicator").GetComponent<TimerControl>();

        startButton = GameObject.Find("StartGame Button").GetComponent<Button>();

        startButton.onClick.AddListener(StartGame);

        /*foreach(var wave in _enemyWaveGameObjects)
        {
            _enemyWaves.Add(wave.GetComponent<EnemyWave>());
        }*/

        if (networkRunner == null)
        {
            networkRunner = FindObjectOfType<NetworkRunner>();
        }

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

        timer.SetTimer((int)_enemyWaves[_numberOfWave].waveDuration);

        yield return new WaitForSecondsRealtime(_enemyWaves[_numberOfWave].waveDuration);
        
        isWaveNow = false;
        _numberOfWave++;

        StopCoroutine(SpawnWave());

        StopCoroutine(SpawnItems());

        StartCoroutine(WaveTimer());
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
                    networkRunner.Spawn(_enemyWaves[_numberOfWave].enemyList[Random.Range(0, _enemyWaves[_numberOfWave].enemyList.Count)], Vector2.zero/*_spawnPoints[Random.Range(0, _spawnPoints.Count)].position*/, Quaternion.identity);
            }
        }
    }

    IEnumerator SpawnItems()
    {
        while (isWaveNow)
        {
            yield return new WaitForSeconds(_enemyWaves[_numberOfWave].timeBetweenItem);

            if(_enemyWaves[_numberOfWave].pickupItemsList.Count != 0)
            networkRunner.Spawn(_enemyWaves[_numberOfWave].pickupItemsList[Random.Range(0, _enemyWaves[_numberOfWave].pickupItemsList.Count)], new Vector3(Random.Range(-10, 10), Random.Range(-10, 10)), Quaternion.identity);
        }

    }




}
