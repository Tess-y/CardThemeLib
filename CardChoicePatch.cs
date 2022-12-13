using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardThemeLib
{
    [Serializable]
    [HarmonyPatch(typeof(CardChoice), "Start")]
    internal class CardChoicePatch
    {
        public static void Postfix()
        {
            CardThemeLib.instance.SetUpThemes();
        }
    }
}
