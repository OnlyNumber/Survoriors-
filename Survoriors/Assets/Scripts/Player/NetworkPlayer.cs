using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public static NetworkPlayer local { get; set; }

    [Networked] public int token { get; set; }

    public void PlayerLeft(PlayerRef player)
    {
        Debug.Log(player.PlayerId);

        /*if(player = Object.InputAuthority)
        {
            Debug.Log(Object.name);

            Runner.Despawn(Object);
        }*/
    }

    public override void Spawned()
    {
        if(Object.HasInputAuthority)
        {
            local = this;

            Debug.Log("Spawn object");
        }
        else
        {
            Debug.Log("Spawn other object");
        }    

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }



}
