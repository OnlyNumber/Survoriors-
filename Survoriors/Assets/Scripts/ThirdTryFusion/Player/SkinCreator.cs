using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SkinCreator : NetworkBehaviour
{
    [SerializeField]
    NetworkBehaviour[] skins;

    NetworkRunner runner;

    private void Awake()
    {
        runner = FindObjectOfType<NetworkRunner>();
    }

    private void Start()
    {
        NetworkBehaviour skin = runner.Spawn(skins[1]);

        if(skin == null)
        {
            Debug.Log("SADADADS");
        }
        transform.SetParent(skin.transform);
    }

}
