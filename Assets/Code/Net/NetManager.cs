using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.Net
{
    public class NetManager : NetworkBehaviour
    { 
        public static NetManager Instance;

        public Vector3 GetSpawnLocation()
        {
            return new Vector3(0, 1, 0);
        }

        public void Awake()
        {
            Instance = this;

        }

        public void Start()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += OnLoadCompleted;
                NetworkManager.Singleton.SceneManager.OnLoadComplete += OnLoadComplete;
                SpawnServerPlayer();
            }

            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
        }

        private void OnClientDisconnected(ulong clientId)
        {
            if(clientId == NetworkManager.ServerClientId)
            {
                DisconnectedFromServer();
            }
            Debug.Log("client " + clientId + " was disconnected");
        }

        private void DisconnectedFromServer()
        {
            NetworkManager.Singleton.Shutdown();
            SceneManager.LoadScene(Constants.TITLESCREEN_BUILD_INDEX);
        }

        private void OnLoadComplete(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
        {
            if (sceneName == Constants.GAME_SCENE_NAME || sceneName.ToLower() == Constants.GAME_SCENE_NAME.ToLower())
            {
                if (clientId != NetworkManager.Singleton.LocalClientId)
                {
                    OnClientConnected(clientId);
                }
            }
        }

        private void OnLoadCompleted(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
        {

        }


        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
        }


        private void LeaveGame()
        {
            NetworkManager.Shutdown();
            SceneManager.LoadScene(Constants.TITLESCREEN_BUILD_INDEX);
        }


        public void Update()
        {
        }


        private void OnClientConnected(ulong clientId)
        {
            GameObject client = Instantiate(PrefabManager.Instance.PlayerPrefab, GetSpawnLocation(), Quaternion.identity);
            client.GetComponent<NetworkObject>().SpawnWithOwnership(clientId, true);
        }

        private void SpawnServerPlayer()
        {
            GameObject serverClient = Instantiate(PrefabManager.Instance.PlayerPrefab, GetSpawnLocation(), Quaternion.identity);
            serverClient.GetComponent<NetworkObject>().Spawn(true);
        }

    }
}
