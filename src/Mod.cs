using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using Mirror;
using UnityEngine;
using HarmonyLib;
using System.Reflection;

namespace grtg_mp
{

    static partial class State
    {
        public static GameObject netManager;
        public static kcp2k.KcpTransport transport;
        public static NetworkManager manager;
        public static NetworkManagerHUD managerHUD;

        public static GameObject netPlayer;
    }


    [BepInPlugin("com.person4268.grtg-mp", "grtg-mp", "1.0.0.0")]
    public class Mod: BaseUnityPlugin
    {

        Harmony harmony;

        void Awake()
        {
            // Step 0: Setup logger
            Log.logger = Logger;


            // Step 1: Setup Harmony
            harmony = new Harmony("com.person4268.grtg-mp");
            harmony.PatchAll();


            // Step 2: Load Assets
            string basePath = Path.GetDirectoryName(Info.Location);

            string assetPath = Path.Combine(new string[] { basePath , "mpassets" });
            Log.Debug("Asset path is " + assetPath);

            Assets.assets = AssetBundle.LoadFromFile(assetPath);
            if(Assets.assets == null)
            {
                Log.Error("Failed to load assets!");
            }

            Assembly.LoadFile(Path.Combine(new string[] { basePath, "Mirror.dll" }));
            Assembly.LoadFile(Path.Combine(new string[] { basePath, "Mirror.Components.dll" }));
            Assembly.LoadFile(Path.Combine(new string[] { basePath, "Telepathy.dll" }));
            Assembly.LoadFile(Path.Combine(new string[] { basePath, "kcp2k.dll" }));
            Assembly.LoadFile(Path.Combine(new string[] { basePath, "Mirror.Authenticators.dll" }));
            Assembly.LoadFile(Path.Combine(new string[] { basePath, "Mirror.Cloud.dll" }));
            Assembly.LoadFile(Path.Combine(new string[] { basePath, "SimpleWebTransport.dll" }));

            // Step 3: Setup Mirror readers and writers
            var ass = Assembly.GetExecutingAssembly();
            var type = ass.GetType("Mirror.GeneratedNetworkCode");
            var meth = type.GetMethod("InitReadWriters");
            meth.Invoke(null, null);


            // Step 3: Setup Mirror and NetworkManager stuff (FUTURE NOTE: MAKE SURE THAT THIS ALL GETS RAN ONLY IN MAIN GAME SCENE OR IMPLEMENT SWITCHING
            State.netManager = new GameObject("NetManager");
            State.transport = State.netManager.AddComponent<kcp2k.KcpTransport>();
            State.manager = State.netManager.AddComponent<NetworkManager>();
            State.managerHUD = State.netManager.AddComponent<NetworkManagerHUD>();

            State.netPlayer = Assets.LoadAsset("NetPlayer");
            State.netPlayer.AddComponent<NetPlayer>();

            if(State.netPlayer.GetComponent<NetworkIdentity>() == null)
            {
                Log.Error("NetPlayer doesn't have NetworkIdentity!");
            }

            Transport.activeTransport = State.transport;
            State.manager.playerPrefab = State.netPlayer;

            State.manager.showDebugMessages = true;
            State.transport.debugGUI = true;

        }
    }
}
