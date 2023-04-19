using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class EnemyWave : MonoBehaviour
{
    public List<NetworkObject> pickupItemsList;

    public List<EnemyNetwork> enemyList;

    public int enemiesCount;

    public float timeBetweenWaveEnemy;

    public float timeBetweenItem;

    public float waveDuration;

}