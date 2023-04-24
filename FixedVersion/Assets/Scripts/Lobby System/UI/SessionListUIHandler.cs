using UnityEngine;
using UnityEngine.UI;
using Fusion;
using TMPro;

public class SessionListUIHandler : MonoBehaviour
{
    public TextMeshProUGUI statusText;
    public GameObject sesionItemListPrefab;
    public VerticalLayoutGroup verticalLayoutGroup;

    private void Awake()
    {
        ClearList();
    }

    public void ClearList()
    {
        foreach(Transform child in verticalLayoutGroup.transform)
        {
            Destroy(child.gameObject);
        }

        statusText.gameObject.SetActive(false);

    }

    public void AddToList(SessionInfo sessionInfo)
    {
        SessionInfoListUIItem addedItem = Instantiate(sesionItemListPrefab, verticalLayoutGroup.transform).GetComponent<SessionInfoListUIItem>();

        addedItem.SetSessionInfo(sessionInfo);

        addedItem.OnJoinSession += AddedSessionInfoListUIItem_OnJoinSession;
    }

    private void AddedSessionInfoListUIItem_OnJoinSession(SessionInfo sessionInfo)
    {
        NetworkRunnerHandler networkRunnerHandler = FindObjectOfType<NetworkRunnerHandler>();

        networkRunnerHandler.JoinGame(sessionInfo);

        MainMenuUIHandler mainMenuUIHandler = FindObjectOfType<MainMenuUIHandler>();
        mainMenuUIHandler.OnJoiningServer();
    
    }

    public void OnNoSessionFound()
    {
        ClearList();

        statusText.text = "No session game found";
        statusText.gameObject.SetActive(true);


    }

    public void OnLookingForGameSession()
    {
        ClearList();

        statusText.text = "Session for game ";
        statusText.gameObject.SetActive(true);
    }


}
