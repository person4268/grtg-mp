using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace grtg_mp
{
    static class PrefabList
    {
        public static void PopulatePrefabLists()
        {
            PopulateWarehousePrefabList();
        }


        public static Dictionary<string, GameObject> warehousePrefabs = new Dictionary<string, GameObject>();
        public static void PopulateWarehousePrefabList()
        {
            var objectsToBuy = GameObject.Find("/GamePlayObjects/) Town Gameplay/PartShop/ObjectsToBuy/");
            if (objectsToBuy == null) { Log.Error("PopulateWarehousePrefabList: Failed to find ObjectsToBuy"); }

            int prefabCount = 0;
            foreach (Transform child in objectsToBuy.transform)
            {
                var buyableObject = child.gameObject.GetComponent<BuyableObject>();
                if (buyableObject == null) { Log.Warning("PopulateWarehousePrefabList: " + child.gameObject.name + " has no BuyableObject"); continue; }

                var prefab = buyableObject.SellPrefab.MyDescriptor.ItemPrefab;
                if (warehousePrefabs.ContainsKey(prefab.name)) continue; // There are duplicates

                warehousePrefabs.Add(prefab.name, prefab);
                prefabCount++;

            }

            Log.Info("Registered " + prefabCount + " prefabs");
        }

    }
}
