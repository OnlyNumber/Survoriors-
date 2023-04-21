using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class DisconnectManager : NetworkBehaviour
{
    private PlayerRef playerRef;

    public DisconnectManager hostPlayer;

    public NetworkRunner networkRunner;

    private void Start()
    {
        playerRef = GetComponent<NetworkPlayer>().checkRef;

        Debug.Log("Transfer serv" + networkRunner.IsServer);

    }

    [ContextMenu("GetOut")]
    public void GetOut()
    {



        ///Debug.Log(Runner.LocalPlayer.PlayerId);

        /* foreach (var item in Runner.ActivePlayers)
         {
             if(item.PlayerId == checkRef.PlayerId)
             {
                 Runner.Disconnect(checkRef);
             }
         }*/
        Runner.Shutdown();

        //networkRunner.Disconnect(playerRef);
        
        //Runner.IsServer
        
        //Runner.Disconnect(refer);
    }

}
