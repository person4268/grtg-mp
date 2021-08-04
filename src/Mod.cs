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

            // Step 2: Setup Mirror readers and writers
            var ass = Assembly.GetExecutingAssembly();
            var type = ass.GetType("Mirror.GeneratedNetworkCode");
            var meth = type.GetMethod("InitReadWriters");
            meth.Invoke(null, null);

            // Step 3: Load Mirror assemblies for Unity
            string basePath = Path.GetDirectoryName(Info.Location);
            Assembly.LoadFile(Path.Combine(new string[] { basePath, "Mirror.dll" }));
            Assembly.LoadFile(Path.Combine(new string[] { basePath, "Mirror.Components.dll" }));
            Assembly.LoadFile(Path.Combine(new string[] { basePath, "Mirror.Authenticators.dll" }));
            Assembly.LoadFile(Path.Combine(new string[] { basePath, "Mirror.Cloud.dll" }));


            // Step 4: Load Assets
            string assetPath = Path.Combine(new string[] { basePath , "mpassets" });
            Assets.SetupAssets(assetPath);

            // Step 4.5: Add script components
            Assets.netDummy.AddComponent<NetDummy>();
            Assets.netPlayer.AddComponent<NetPlayer>();


            // Step 5: Setup Mirror and NetworkManager stuff (FUTURE NOTE: MAKE SURE THAT THIS ALL GETS RAN ONLY IN MAIN GAME SCENE OR IMPLEMENT SWITCHING
            new GameObject().AddComponent<MultiplayerCore>();

        }
    }
}
