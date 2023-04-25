using UnityEngine;
using Fusion;

public class PlayerScore : NetworkBehaviour
{
    public int score { get; private set; }

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



    
}
