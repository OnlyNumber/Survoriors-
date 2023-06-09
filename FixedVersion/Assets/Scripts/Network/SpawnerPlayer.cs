using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using UnityEngine.SceneManagement;

public class SpawnerPlayer : MonoBehaviour, INetworkRunnerCallbacks
{
    public NetworkPlayer playerPrefab;

    private Dictionary<int, NetworkPlayer> mapTokenIdWithNetworkPlayer;

    private PlayerInputHandler characterInputHandler;

    private SessionListUIHandler sessionListUIHandler;

    private Dictionary<PlayerRef, NetworkPlayer> _spawnedCharacters = new Dictionary<PlayerRef, NetworkPlayer>();

    [ContextMenu("Find players")]
    public void ShowDictionary()
    {
        Debug.Log("Start show players");

        foreach (var item in _spawnedCharacters)
        {
            Debug.Log("pla");
            Debug.Log(item.Key.PlayerId);
        }
    }

    public Dictionary<PlayerRef, NetworkPlayer> GetSpawnedPlayers()
    {
        Dictionary<PlayerRef, NetworkPlayer> spawnedCharactersCopy = new Dictionary<PlayerRef, NetworkPlayer>();

        foreach(var item in _spawnedCharacters)
        {
            spawnedCharactersCopy.Add(item.Key, item.Value);
        }

        return spawnedCharactersCopy;
    }

    private void Awake()
    {
        mapTokenIdWithNetworkPlayer = new Dictionary<int, NetworkPlayer>();
        
        sessionListUIHandler = FindObjectOfType<SessionListUIHandler>(true);
    }

    private void Start()
    {
        
    }

    int GetPlayerToken(NetworkRunner runner, PlayerRef player)
    {
        if(runner.LocalPlayer == player)
        {
            return ConnectionTokenUtils.HashToken(GameManager.instance.GetConnectionToken());
        }
        else
        {
            var token = runner.GetPlayerConnectionToken();

            if(token != null)
            {
                return ConnectionTokenUtils.HashToken(token);
            }

            Debug.LogError($"GetPlayerToken returned invalid token");

                return 0;

        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {

        if (characterInputHandler == null && NetworkPlayer.local)
        {
            characterInputHandler = NetworkPlayer.local.GetComponent<PlayerInputHandler>();
        }

        if(characterInputHandler != null)
        {
            input.Set(characterInputHandler.GetNetworkInput());
        }


    }

    public void SetConnectionTokenMapping(int token, NetworkPlayer networkPlayer)
    {
        mapTokenIdWithNetworkPlayer.Add(token, networkPlayer);
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if(runner.IsServer)
        {
            int playerToken = GetPlayerToken(runner, player);

            Debug.Log("OnPlayerJoined We are server Spawn player");

            if(mapTokenIdWithNetworkPlayer.TryGetValue(playerToken, out NetworkPlayer networkPlayer))
            {
                Debug.Log($"Found old connection token for {playerToken}, Assigning controls to start player");

                networkPlayer.GetComponent<NetworkObject>().AssignInputAuthority(player);
            }
            else
            {
                Debug.Log($"Spawning new player for connection token {playerToken}");

                NetworkPlayer spawnedNetworkPlayer = runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity, player);

                spawnedNetworkPlayer.token = playerToken;

                _spawnedCharacters.Add(player, spawnedNetworkPlayer);

                
            }
        }
        
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("OnConnectedToServer");
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        Debug.Log("OnConnectFailed");
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        Debug.Log("OnConnectRequest");
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        Debug.Log("OnDisconnectedFromServer");
    }

    public async void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        Debug.Log("Host migration");

        await runner.Shutdown();

    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {

        if (_spawnedCharacters.TryGetValue(player, out NetworkPlayer networkObject))
        {
            _spawnedCharacters.Remove(player);

            if(networkObject != null)
            runner.Despawn(networkObject.GetComponent<NetworkObject>());
        }

    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        SessionInfo sessionInfoTransfer;

        if(sessionListUIHandler == null)
        {
            return;
        }

        if(sessionList.Count == 0)
        {
            Debug.Log("Joined Lobby no sessions found");

            sessionListUIHandler.OnNoSessionFound();
        }
        else
        {
            sessionListUIHandler.ClearList();

            foreach (SessionInfo sessionInfo in sessionList)
            {
                sessionInfoTransfer = sessionInfo;

                sessionListUIHandler.AddToList(sessionInfo);

                Debug.Log($"Found session {sessionInfo.Name} playersCount {sessionInfo.PlayerCount}");
            }
        }
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        SceneManager.LoadScene("MenuScene");

        Debug.Log("OnShutdown");
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {

    }
}
