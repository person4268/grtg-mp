using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace grtg_mp
{
    static class Assets
    {
        private static AssetBundle assets;
        private static Dictionary<string, GameObject> assetCache = new Dictionary<string, GameObject>();

        public static GameObject netDummy;
        public static GameObject netPlayer;


        public static GameObject LoadAsset(string name)
        {
            var retVal = assets.LoadAsset<GameObject>(name);
            if (retVal == null)
            {
                Debug.LogError("Failed to load asset " + name);
            }
            return retVal;
        }

        // Loads assetbundle into assets variable
        public static void SetupAssets(string assetPath)
        {
            Log.Debug("Asset path is " + assetPath);

            assets = AssetBundle.LoadFromFile(assetPath);
            if (assets == null)
            {
                Log.Error("Failed to load assets!");
            }

            // Load some assets

            netDummy = LoadAsset("NetDummy");
            netPlayer = LoadAsset("NetPlayer");
        }
    }
}
