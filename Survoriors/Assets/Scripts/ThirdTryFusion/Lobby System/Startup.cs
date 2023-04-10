using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup 
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InstantiatePrefab()
    {
        Debug.Log("-- Ins obj --");

        GameObject[] prefabToInstatiate = Resources.LoadAll<GameObject>("InstatiateOnLoad");

        foreach (GameObject prefab in prefabToInstatiate)
        {
            Debug.Log($"Creating {prefab.name}");

            GameObject.Instantiate(prefab);

        }

        Debug.Log("-- Instatiating obj done--");

    }


    
}
