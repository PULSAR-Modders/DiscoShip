using HarmonyLib;
using PulsarModLoader.Patches;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

namespace DiscoShip
{
    [HarmonyPatch(typeof(PLShipInfo), "Update")]
    class ShipInfo
    {
        public static int light = 0;
        public static int time = 0;
        public static int interval;

        public static Color GetRainbow(Color white)
        {
            if (Mod.Instance.IsEnabled())
            {
                int lights = PLNetworkManager.Instance.LocalPlayer.GetPawn().CurrentShip.InteriorShipLights.Count;
                light++;
                if (light >= lights)
                {
                    time++;
                    light = 0;
                }
                int indexInt = (time / interval + (light * 2)) % 6;
                float indexFloat = Mathf.Clamp((time / (float)interval) % 1, 0.2f, 1);
                switch (indexInt)
                {
                    case 0:
                        return new Color(1, indexFloat, 0.2f);
                    case 1:
                        return new Color(1 - indexFloat, 1, 0.2f);
                    case 2:
                        return new Color(0.2f, 1, indexFloat);
                    case 3:
                        return new Color(0.2f, 1 - indexFloat , 1);
                    case 4:
                        return new Color(indexFloat, 0.2f, 1);
                    case 5:
                        return new Color(1, 0.2f, 1 - indexFloat);
                }
            }
            return white;
        }

        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            //Color.Lerp(this.AllInteriorShipLightColors[num25], b4, num19);
            List<CodeInstruction> targetSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldloc_S),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld),
                new CodeInstruction(OpCodes.Ldloc_S),
                new CodeInstruction(OpCodes.Ldelem),
                new CodeInstruction(OpCodes.Ldloc_S),
                new CodeInstruction(OpCodes.Ldloc_S),
                new CodeInstruction(OpCodes.Call)
            };

            List<CodeInstruction> patchSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(ShipInfo), "GetRainbow"))
            };

            return HarmonyHelpers.PatchBySequence(instructions, targetSequence, patchSequence, HarmonyHelpers.PatchMode.AFTER, HarmonyHelpers.CheckMode.NONNULL);
        }
    }
}
