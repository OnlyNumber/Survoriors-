using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIChangerScore : MonoBehaviour
{

    public TMP_Text killsText;

    private PlayerScrV3 playerShoots;

    private void Start()
    {
        killsText = GameObject.Find("Kills Indicator").GetComponent<TMP_Text>();

        playerShoots = GetComponent<PlayerScrV3>();

        playerShoots.onKillChange += ChangeKillScore;
    }

    private void ChangeKillScore()
    {
        if(playerShoots.HasInputAuthority)
        killsText.text = $"{playerShoots.Kill}";
    }
}
