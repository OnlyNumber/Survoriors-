using UnityEngine;

public class InitializePlayerHealth : MonoBehaviour
{
    private void Start()
    {
        GetComponent<HealthHandler>().onDeath += FindObjectOfType<EnemySpawner>().CheckAllPlayer;
    }
}
