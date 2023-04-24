using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;

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

        //Debug.Log(playersTable.Length);

        leaderBoardPanel.SetActive(false);
    }

   // [ContextMenu("Show table")]
    [Rpc]
    public void Rpc_RequestShowTable(PlayerScore[] playerScores )
    {
        /*List<PlayerScrV3> playerInfo = new List<PlayerScrV3>();

        foreach(var item in FindObjectOfType<SpawnerPlayer>().GetSpawnedPlayers().Values)
        {
            playerInfo.Add(item.GetComponent<PlayerScrV3>());
        }*/

        for (int i = 0; i < playerScores.Length; i++)
        {
            //playersTable[i].Image_PlayerSkin.sprite = playerInfo[i].GetComponentInChildren<SpriteRenderer>().sprite;
            playersTable[i].Txt_Score.text = "Score: " + playerScores[i].score;//playerInfo[i].score.ToString();
            playersTable[i].Txt_Kills.text = "Kills: " + playerScores[i].kills;//playerInfo[i].Kill.ToString();
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

            transfer.kills = item.GetComponent<PlayerScrV3>().Kill;

            transfer.score = item.GetComponent<PlayerScrV3>().score;

            playerInfo.Add(transfer); //playerInfo.Add(item.GetComponent<PlayerScrV3>());
        }

        PlayerScore[] scores = new PlayerScore[playerInfo.Count];

        Rpc_RequestShowTable(scores);
    }



}
