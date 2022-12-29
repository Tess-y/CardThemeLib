using HarmonyLib;
using System;

namespace CardThemeLib
{
    [Serializable]
    [HarmonyPatch(typeof(CardChoice), "Awake")]
    internal class CardChoicePatch
    {
        public static void Postfix()
        {
            CardThemeLib.instance.StartCoroutine(CardThemeLib.instance.SetUpThemes());
        }
    }
}
