using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grtg_mp
{
    class MultiplayerCore: MonoBehaviour
    {
        void Awake()
        {
            SceneManager.activeSceneChanged += (prev,next) => {
                if (next.name == "Build_Stream_Main") Setup();
            };
        }

        void Setup()
        {
            PrefabList.PopulatePrefabLists();

            // Create components
            State.netManager = new GameObject("NetManager");
            State.transport = State.netManager.AddComponent<kcp2k.KcpTransport>();
            State.manager = State.netManager.AddComponent<NetworkManager>();
            State.managerHUD = State.netManager.AddComponent<NetworkManagerHUD>();


            if (Assets.netPlayer.GetComponent<NetworkIdentity>() == null)
            {
                Log.Error("NetPlayer doesn't have NetworkIdentity!");
            }

            // Set our transport and player prefab
            Transport.activeTransport = State.transport;
            State.manager.playerPrefab = Assets.netPlayer;

            // Debug
            //State.manager.showDebugMessages = true;
            //Mirror.LogFilter.Debug = true;
            //State.transport.debugGUI = true;

            // Make NetDummy Spawnable
            State.manager.spawnPrefabs.Add(Assets.netDummy);
        }
    }
}
