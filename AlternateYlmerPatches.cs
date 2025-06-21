using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;
// using static Obeliskial_Essentials.Essentials;
using System;
using static AlternateYlmer.Plugin;
using static AlternateYlmer.CustomFunctions;
using static AlternateYlmer.AlternateYlmerFunctions;
using System.Collections.Generic;
using static Functions;
using UnityEngine;
// using Photon.Pun;
using TMPro;
using System.Linq;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Reflection;
using UnityEngine.UIElements;
// using Unity.TextMeshPro;

// Make sure your namespace is the same everywhere
namespace AlternateYlmer
{

    [HarmonyPatch] // DO NOT REMOVE/CHANGE - This tells your plugin that this is part of the mod

    public class AlternateYlmerPatches
    {

        [HarmonyPrefix]
        [HarmonyPatch(typeof(MapManager), "DoCombat")]
        public static void DoCombatPrefix(MapManager __instance, ref CombatData _combatData)
        {

            // LogDebug($"DoCombatPrefix: {string.Join(", ", AtOManager.Instance.bossesKilledName ?? new List<string>())}");
            // bool killedLordMont = AtOManager.Instance.bossesKilledName != null && AtOManager.Instance.bossesKilledName.Any<string>((Func<string, bool>)(s => s.StartsWith("lordmontimus", StringComparison.OrdinalIgnoreCase)));
            LogDebug($"Loading Combat - {_combatData?.CombatId ?? "null combat"}");
            if (_combatData.CombatId == "esen_33random")
            {
                LogDebug("DoCombatPrefix - Getting Combat Data for Ylmer - Random");
                try
                {
                    List<string> combats = ["esen_33a", "esen_33a_vile", "esen_33a_warded"];
                    int randInd = MapManager.Instance.GetRandomIntRange(0, combats.Count);
                    string randomCombat = combats[randInd];
                    _combatData = Globals.Instance.GetCombatData(randomCombat);
                    LogDebug("DoCombatPrefix - Random Combat Loading: " + randomCombat);
                }
                catch (Exception e)
                {
                    LogError($"DoCombatPrefix - Error getting combat data: {e.Message}");
                    return; // Prevent further execution if combat data cannot be retrieved
                }
            }
            // else if (AtOManager.Instance.bossesKilledName != null && AtOManager.Instance.bossesKilledName.Any<string>((Func<string, bool>)(s => s.StartsWith("lordmontimus", StringComparison.OrdinalIgnoreCase))))
            // {
            //     LogDebug("DoCombatPrefix - Killed Alternate Ylmer");
            //     if (!AtOManager.Instance.bossesKilledName.Any<string>((Func<string, bool>)(s => s.StartsWith("archonmont", StringComparison.OrdinalIgnoreCase))))
            //     {
            //         LogDebug("DoCombatPrefix - Archon Mont Combat starting");
            //         AtOManager.Instance.SetCombatData(Globals.Instance.GetCombatData("evoidhigh_13archonmont"));
            //         DoCombat(__instance, AtOManager.Instance.GetCurrentCombatData());
            //         return false;
            //     }
            //     AtOManager.Instance.FinishGame();
            //     return false;
            // }
            // return true;
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(AtOManager), "GlobalAuraCurseModificationByTraitsAndItems")]
        public static void GlobalAuraCurseModificationByTraitsAndItemsPostfix(ref AtOManager __instance, ref AuraCurseData __result, string _type, string _acId, Character _characterCaster, Character _characterTarget)
        {
            // LogInfo($"GACM MoreMadness");
            Character characterOfInterest = _type == "set" ? _characterTarget : _characterCaster;
            // bool gainsPerksNPC = IsLivingNPC(characterOfInterest) && difficultyLevelInt >= (int)DifficultyLevelEnum.Hard && HasCorruptor(Corruptors.Decadence);
            // bool gainsPerksHero = IsLivingHero(characterOfInterest) && difficultyLevelInt >= (int)DifficultyLevelEnum.Hard && HasCorruptor(Corruptors.Decadence);
            // string enchantId;
            if (!IsLivingNPC(characterOfInterest))
            {
                return;
            }
            switch (_acId)
            {
                case "evasion":
                    break;
                case "fast":
                    break;
                case "buffer":
                    break;
                case "zeal":
                    break;
                case "sharp":
                    // enchantId = "montluxuriouscoat";
                    // if (NpcHaveEnchant(characterOfInterest, enchantId))
                    // {
                    //     __result.AuraDamageType3 = Enums.DamageType.Mind;
                    //     __result.AuraDamageIncreasedPerStack3 = 1;
                    // }
                    break;
            }
        }


    }
}