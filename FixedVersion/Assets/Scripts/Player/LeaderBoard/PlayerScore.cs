using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class PlayerScore : NetworkBehaviour
{
    
    public int score { get; private set; }

    public int kills { get; private set; }

    private TMP_Text _killsText;

    private void Start()
    {
        _killsText = GameObject.Find("Kills Indicator").GetComponent<TMP_Text>();
    }

    public void IncreaseScore(int addScore)
    {
        if (addScore > 0)
        {
            score += addScore;
        }
    }

    public void IncreaseKills(int kill)
    {
        if (kill > 0)
        {
            kills += kill;
            ChangeKillScore();
        }
    }

    private void ChangeKillScore()
    {
        _killsText.text = $"{kills}";
    }

    [ContextMenu("Show data debug")]
    public void ShowDebug()
    {
        Debug.Log($"damage: {score} /kill: {kills} ");
    }


}
