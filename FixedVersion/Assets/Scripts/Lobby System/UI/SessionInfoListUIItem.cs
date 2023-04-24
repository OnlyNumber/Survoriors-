using UnityEngine;
using UnityEngine.UI;
using Fusion;
using System;
using TMPro;

public class SessionInfoListUIItem : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI sessionNameText;

    [SerializeField]
    public TextMeshProUGUI sessionCountPlayers;

    [SerializeField]
    private Button joinButton;

    SessionInfo sessionInfo;

    public event Action<SessionInfo> OnJoinSession;

    public void SetSessionInfo(SessionInfo session)
    {
        sessionInfo = session;

        sessionNameText.text = session.Name;
        sessionCountPlayers.text = $"{sessionInfo.PlayerCount.ToString()}/{sessionInfo.MaxPlayers.ToString()}";

        bool isJoinButtonActive = true;

        if(sessionInfo.PlayerCount >= sessionInfo.MaxPlayers)
        {
            isJoinButtonActive = false;
        }

        joinButton.gameObject.SetActive(isJoinButtonActive);

    }

    public void OnClick()
    {
        OnJoinSession?.Invoke(sessionInfo);
    }

}
