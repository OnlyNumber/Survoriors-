using UnityEngine;
using TMPro;

public class UIChangerScore : MonoBehaviour
{

    public TMP_Text killsText;

    private PlayerScore playerShoots;

    private void Start()
    {
        killsText = GameObject.Find("Kills Indicator").GetComponent<TMP_Text>();

        playerShoots = GetComponent<PlayerScore>();

        playerShoots.onKillChange += ChangeKillScore;
    }

    private void ChangeKillScore()
    {
        if(playerShoots.HasInputAuthority)
        killsText.text = $"{playerShoots.Kill}";
    }
}
