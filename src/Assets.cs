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
        public static AssetBundle assets;

        public static GameObject LoadAsset(string name)
        {
            var retVal = assets.LoadAsset<GameObject>(name);
            if (retVal == null)
            {
                Debug.LogError("Failed to load asset " + name);
            }
            return retVal;
        }
    }
}
