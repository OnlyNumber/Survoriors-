using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerScrV3 : NetworkBehaviour
{
    private int shoots = 0;

    private int score;

    private int kills;

    public delegate void OnKillChange();

    public OnKillChange onKillChange;

    public int Kill
    {
        get
        {
            return kills;
        }
        private set
        {
            kills = value;
            onKillChange?.Invoke();
        }

    }

    public int Shoots
    {
        get 
        {
            return shoots;
        }
        set
        {
            shoots = value;
            Debug.Log("Work");
            //onShootChange?.Invoke();

        }

    }

    [Rpc]
    public void Rpc_RequestChangeKill(int kill)
    {
        this.Kill += kill;
    }

    [Rpc]
    public void Rpc_RequestChangeScore(int score)
    {
        this.score += score;
    }



    [Rpc]
    public void Rpc_RequestSaveShoots(int saveShoots, RpcInfo info = default)
    {
        Debug.Log("RPC");
        Shoots += saveShoots;
    }
}
