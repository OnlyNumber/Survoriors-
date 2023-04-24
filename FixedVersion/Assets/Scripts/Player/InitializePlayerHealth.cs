using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePlayerHealth : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Initialized");

        GetComponent<HealthHandler>().onDeath += FindObjectOfType<EnemySpawner>().CheckAllPlayer;
    }
}
