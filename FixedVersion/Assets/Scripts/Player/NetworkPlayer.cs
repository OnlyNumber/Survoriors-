using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public static NetworkPlayer local { get; set; }

    public PlayerRef checkRef;

    private Button exitButton;

    [Networked] public int token { get; set; }

    private void Start()
    {
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();

        exitButton.onClick.AddListener(GetOut);
    }


    public void PlayerLeft(PlayerRef player)
    {
        //Debug.Log(player.PlayerId);
    }

    public override void Spawned()
    {
        if(Object.HasInputAuthority)
        {
            local = this;

            Debug.Log("Spawn object");
        }
        

    }

    public void GetOut()
    {
            Runner.Shutdown();
    }


}
