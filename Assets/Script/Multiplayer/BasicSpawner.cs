using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BasicSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    private NetworkRunner _runner;


    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }

    async void StartGame(GameMode mode)
    {
        // Create the Fusion runner and let it know that we will be providing user input
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;

        // Start or join (depends on gamemode) a session with a specific name
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = "TestRoom",
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    /*private void OnGUI()
    {
        if (_runner == null)
        {
            if (GUI.Button(new Rect(0, 0, 200, 40), "Host"))
            {
                Debug.Log("host mode");
                
            }
            if (GUI.Button(new Rect(0, 40, 200, 40), "Join"))
            {
                Debug.Log("client mode");
                StartGame(GameMode.Client);
            }
        }
    }*/

    public void startHostMode()
    {
        if (_runner == null)
        {
            StartGame(GameMode.Host);
        }   
    }

    public void startJoinMode()
    {
        if (_runner == null)
        {
            StartGame(GameMode.Client);
        }
    }

    [SerializeField] private NetworkPrefabRef _playerPrefab; // Character to spawn for a joining player
    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("runner.name : " + runner.name);
        Debug.Log("runner.IsClient : " + runner.IsClient);
        Debug.Log("runner.IsStarting : " + runner.IsStarting);
        Debug.Log("runner.IsServer : " + runner.IsServer);

        if (runner.IsServer)
        {

            //Vector3 spawnPosition = new Vector3((player.RawEncoded % runner.Config.Simulation.DefaultPlayers) * 3, 1, 0);
            Vector3 spawnPosition = new Vector3(0, 0, 0);
            // Vector2 spawnPosition = new Vector2(0, 5);
            NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
            _spawnedCharacters.Add(player, networkPlayerObject);
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkInputData();

        /*if (Input.GetKey(KeyCode.A))
            data.movement += Vector2.left;

        if (Input.GetKey(KeyCode.D))
            data.movement += Vector2.right;*/

        if (Input.GetMouseButtonDown(0))
            data.movement += Vector2.up * 9;

        input.Set(data);
    }

}