using Mirror;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grtg_mp
{
    class NetDummy: NetworkBehaviour
    {
        [SyncVar]
        public string gameObjectName;

        public GameObject realGameObject;

        // Called by whatever instantiated us, or ourselves if just on the client
        public void Init()
        {
            Log.Warning("NetDummy.Init(): " + gameObjectName);
            realGameObject = Instantiate(PrefabList.warehousePrefabs[gameObjectName]);
        }

        public override void OnStartClient()
        {
            if (isClientOnly) Init();
        }

        void Update()
        {
            // If real game object changed position...
            if(realGameObject.transform.hasChanged)
            {
                // Clear the hasChanged flag then...
                transform.hasChanged = false;

                // if we're on the server...
                if (isServer)
                {
                    // Update the NetDummy transform to match!
                    transform.position = realGameObject.transform.position;
                    transform.rotation = realGameObject.transform.rotation;
                }
                // If we're on the client...
                if(isClientOnly)
                {
                    // TODO: Use commands to sync position from client->server
                }
            }

            // If NetDummy changed position...
            if(transform.hasChanged)
            {
                // Clear the hasChanged flag then...
                transform.hasChanged = false;

                // If we're on the server...
                if (isServer)
                {
                    // Update the GameObject transform to match
                    realGameObject.transform.position = transform.position;
                    realGameObject.transform.rotation = transform.rotation;
                }
                // If we're on the client...
                if (isClientOnly)
                {
                    // TODO: Use commands to sync position from client->server
                }
            }
        }

    }
}
