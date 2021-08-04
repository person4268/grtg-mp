using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Mirror;

namespace grtg_mp.Patches
{
    class InstantiateHook
    {
        // Called by all Harmony patches that override Instantiate
        public static GameObject Hook(GameObject original)
        {
            var netDummy = UnityEngine.Object.Instantiate(Assets.netDummy).GetComponent<NetDummy>();
            netDummy.gameObjectName = original.name;
            NetworkServer.Spawn(netDummy.gameObject);

            netDummy.Init();


            return netDummy.realGameObject; 
        }
    }
}
