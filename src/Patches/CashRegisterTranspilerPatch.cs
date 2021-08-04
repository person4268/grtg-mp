using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace grtg_mp.Patches
{
    [HarmonyPatch]
    class CashRegisterTranspilerPatch
    {
        static MethodInfo hookMethodInfo = Assembly.GetExecutingAssembly().GetType("grtg_mp.Patches.InstantiateHook").GetMethod("Hook");

        public static MethodBase TargetMethod()
        {
            // CashRegisterGUI.<BuyDelayed>d__64.MoveNext is the real method because IEnumerables are werid
            var type = typeof(CashRegisterGUI);
            if (type == null) { Log.Error("CashRegisterTranspilerPatch: CashRegisterGUI not found"); } // <- It would be weird if this error printed, honestly

            var compilerGeneratedClass = type.GetNestedType("<BuyDelayed>d__64", BindingFlags.Public | BindingFlags.NonPublic); // Flags needed because compiler generated type is not public, and it defaults to public types only. 
            if (compilerGeneratedClass == null) { Log.Error("CashRegisterTranspilerPatch: <BuyDelayed>d__64 not found"); }

            var moveNext = compilerGeneratedClass.GetMethod("MoveNext", BindingFlags.NonPublic | BindingFlags.Instance);
            if (moveNext == null) { Log.Error("CashRegisterTranspilerPatch: MoveNext not found"); }

            return moveNext;
        }

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            bool foundCall = false;
            foreach (var i in instructions)
            {
                // Check if instruction calls UnityEngine.Object.Instantiate()
                if(i.opcode == OpCodes.Call && ((MethodInfo)i.operand).Name.Contains("Instantiate"))
                {
                    foundCall = true;
                    i.operand = hookMethodInfo;
                }
                yield return i;
            }

            // If we didn't find our call to UnityEngine.Object.Instantiate()
            if(foundCall == false)
            {
                Log.Error("CashRegisterTranspilerPatch: Transpiler didn't find call");
            }
        }
    }
}
