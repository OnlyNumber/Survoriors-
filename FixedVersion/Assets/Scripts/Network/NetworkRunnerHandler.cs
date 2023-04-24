using UnityEngine;
using System;
using Fusion;
using Fusion.Sockets;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class NetworkRunnerHandler : MonoBehaviour
{
    private int maxPlayers = 2;

    public NetworkRunner networkRunnerPrefab;

    private NetworkRunner networkRunner;

    private const string MENU_SCENE = "MenuScene";

    private void Awake()
    {
        NetworkRunner networkRunnerScene = FindObjectOfType<NetworkRunner>();

        if(networkRunnerScene != null)
        {
            networkRunner = networkRunnerScene;
        }
    }

    private void Start()
    {

        if (networkRunner == null)
        {
            networkRunner = Instantiate(networkRunnerPrefab);

            networkRunner.name = "Network runner";

            if (SceneManager.GetActiveScene().name != MENU_SCENE)
            {
                var clientTask = InitializeNetworkRunner(networkRunner, GameMode.AutoHostOrClient, "TestSessionName", GameManager.instance.GetConnectionToken(), NetAddress.Any(), SceneManager.GetActiveScene().buildIndex, null);
            }

            Debug.Log("Server NetworkRunner started");
        }
    }

    public void StartHostMigration(HostMigrationToken hostMigrationToken)
    {

        networkRunner = Instantiate(networkRunnerPrefab);

        networkRunner.name = "Network runner Migrated" ;

        var clientTask = InitializeNetworkRunnerMigration(networkRunner,  hostMigrationToken);
        
        Debug.Log("Server NetworkRunner started");

    }

    protected virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode gameMode, string sessionName, byte[] connectionToken, NetAddress addres, SceneRef scene, Action<NetworkRunner> initialized)
    {
        var sceneManager = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();

        if(sceneManager == null)
        {
            sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        return runner.StartGame(new StartGameArgs
        {
            PlayerCount = maxPlayers,
            GameMode = gameMode,
            Address = addres,
            Scene = scene,
            SessionName = sessionName,
            CustomLobbyName = "OurLobbyID",
            Initialized = initialized,
            SceneManager = sceneManager,
            ConnectionToken = connectionToken
        });
    }

    protected virtual Task InitializeNetworkRunnerMigration(NetworkRunner runner,HostMigrationToken hostMigrationToken)
    {
        var sceneManager = GetSceneManager(runner);

        if (sceneManager == null)
        {
            sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        return runner.StartGame(new StartGameArgs
        {
            SceneManager = sceneManager,
            HostMigrationToken = hostMigrationToken,
            HostMigrationResume = HostMigrationResume,
            ConnectionToken = GameManager.instance.GetConnectionToken()
        });
    }

    void HostMigrationResume(NetworkRunner runner)
    {
        Debug.Log($"HostMigration started");

        foreach(var ResumeNetworkObjects in runner.GetResumeSnapshotNetworkObjects())
        {
            if(ResumeNetworkObjects.TryGetBehaviour<NetworkPlayerController>(out var characterController))
            {
                //TODO: доделать HostMigration;
                runner.Spawn(ResumeNetworkObjects, position: Vector2.zero, rotation: Quaternion.identity, onBeforeSpawned: (runner, newNetworkObject) =>
                {
                    newNetworkObject.CopyStateFrom(ResumeNetworkObjects);

                    if(ResumeNetworkObjects.TryGetBehaviour<NetworkPlayer>(out var oldNetwokPlayer))
                    {
                        FindObjectOfType<SpawnerPlayer>().SetConnectionTokenMapping(oldNetwokPlayer.token, newNetworkObject.GetComponent<NetworkPlayer>());
                    }

                });
            }
        }

        Debug.Log($"HostMigration completed");
    }

    public void OnJoinLobby()
    {
        var clientTask = JoinLobby();
    }

    private async Task JoinLobby()
    {
        Debug.Log("JoinLobbyStarted");

        string lobbyID = "OurLobbyID";

        var result = await networkRunner.JoinSessionLobby(SessionLobby.Custom, lobbyID);

        if (!result.Ok)
        {
            Debug.LogError($"Unable to join lobby {lobbyID}");
        }
        else
        {
            Debug.Log("JoinLobby ok");
        }
    }

    public void CreateGame(string sessionName, string sceneName)
    {
        Debug.Log($"Create session {sessionName} scene {sceneName} build Index {SceneUtility.GetBuildIndexByScenePath($"scenes/{sceneName}")}");

        var clientTask = InitializeNetworkRunner(networkRunner, GameMode.Host, sessionName, GameManager.instance.GetConnectionToken(), NetAddress.Any(), SceneUtility.GetBuildIndexByScenePath($"scenes/{sceneName}"), null);
    }

    public void JoinGame(SessionInfo sessionName)
    {
        Debug.Log($"Create session {sessionName.Name}");

        var clientTask = InitializeNetworkRunner(networkRunner, GameMode.Client, sessionName.Name, GameManager.instance.GetConnectionToken(), NetAddress.Any(), SceneManager.GetActiveScene().buildIndex, null);
    }

    INetworkSceneManager GetSceneManager(NetworkRunner runner)
    {
        var sceneManager = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();

        if(sceneManager == null)
        {
            sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        return sceneManager;

    }


}
