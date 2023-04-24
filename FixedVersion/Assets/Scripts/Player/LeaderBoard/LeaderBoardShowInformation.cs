using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;

public class LeaderBoardShowInformation : NetworkBehaviour
{
    [SerializeField]
    private GameObject leaderBoardPanel;

    private PlayerPanel[] playersTable;

    private void Start()
    {
        playersTable = GetComponentsInChildren<PlayerPanel>();

        Debug.Log(playersTable.Length);
    }

    public void ShowTable()
    {
        leaderBoardPanel.SetActive(true);
    }



}
