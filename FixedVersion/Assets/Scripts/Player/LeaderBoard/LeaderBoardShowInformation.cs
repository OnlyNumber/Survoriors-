using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class LeaderBoardShowInformation : NetworkBehaviour
{

    public struct PlayerScore : INetworkStruct
    {
        public int skin;
        public int score;
        public int kills;


    }

    [SerializeField]
    private GameObject leaderBoardPanel;

    private PlayerPanel[] playersTable;

    [SerializeField]
    private GameObject moveJoystick;

    [SerializeField]
    private GameObject weaponJoystick;


    private void Start()
    {
        playersTable = GetComponentsInChildren<PlayerPanel>();

        leaderBoardPanel.SetActive(false);
    }

    [Rpc]
    public void Rpc_RequestShowTable(PlayerScore[] playerScores )
    {
        for (int i = 0; i < playerScores.Length; i++)
        {
            playersTable[i].Txt_Score.text = "Score: " + playerScores[i].score;
            playersTable[i].Txt_Kills.text = "Kills: " + playerScores[i].kills;
        }

        if(playerScores.Length < 2)
        {
            playersTable[1].Image_PlayerSkin.color = new Color(255, 255, 255, 0);
            playersTable[1].Txt_Score.text = "No player";
            playersTable[1].Txt_Kills.text = "";
        }


        leaderBoardPanel.SetActive(true);
        weaponJoystick.SetActive(false);
        moveJoystick.SetActive(false);

    }

    [ContextMenu("Show table")]
    public void ShowTable()
    {
        SpawnerPlayer pointer = FindObjectOfType<SpawnerPlayer>();

        List<PlayerScore> playerInfo = new List<PlayerScore>();

        PlayerScore transfer;

        foreach (var item in pointer.GetSpawnedPlayers().Values)
        {
            transfer.skin = 0;

            transfer.kills = item.GetComponent<global::PlayerScore>().Kill;

            transfer.score = item.GetComponent<global::PlayerScore>().score;

            playerInfo.Add(transfer);
        }

        PlayerScore[] scores = new PlayerScore[playerInfo.Count];

        Rpc_RequestShowTable(scores);
    }



}
