using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class PlayerScore : NetworkBehaviour
{
    public int score { get; private set; }

    public int kills { get; private set; }

    public delegate void OnKillChange();

    public OnKillChange onKillChange;

    private void Start()
    {
        //_killsText = FindObjectOfType<UIManager>().killsText;
    }


    public void IncreaseScore(int addScore)
    {
        if (addScore > 0)
        {
            Rpc_RequestChangeScore(addScore, 0);
        }
    }

    public void IncreaseKills(int kill)
    {
        if (kill > 0)
        {
            Rpc_RequestChangeScore(0, kill);

            onKillChange?.Invoke();
            //ChangeKillScore();
        }
    }

    [ContextMenu("Show data debug")]
    public void ShowDebug()
    {
        Debug.Log($"damage: {score} /kill: {kills} ");
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    private void Rpc_RequestChangeScore(int score, int kills, RpcInfo info = default)
    {
        this.score += score;

        this.kills += kills;
    }


}
