using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Mirror;

namespace grtg_mp
{
    static partial class State
    {
        public static bool isServer = false;
        public static bool isClient = false;

        public static bool isServerOnly = false;
        public static bool isClientOnly = false;
    }

    class NetPlayer: NetworkBehaviour
    {
        GameObject player;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if(player == null)
            {
                Log.Error("Player not found, setting fake player");
                player = new GameObject();
            }

            Log.Info("NetPlayer: Do we have authority for this player: " + gameObject.GetComponent<NetworkTransform>().hasAuthority);
            Log.Info("NetPlayer: NetworkBehaviour.hasAuthority: " + hasAuthority);
        }

        void Update()
        {
            if (isLocalPlayer)
            {
                transform.position = player.transform.position;
                transform.rotation = player.transform.rotation;
            }

            State.isServer = isServer;
            State.isClient = isClient;
            State.isServerOnly = isServerOnly;
            State.isClientOnly = isClientOnly;
        }
    }
}
