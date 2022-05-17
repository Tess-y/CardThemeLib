using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardThemeLib
{
    // Declares our Mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]
    // The game our Mod Is associated with
    [BepInProcess("Rounds.exe")]
    public class CardThemeLib : BaseUnityPlugin
    {
        private const string ModId = "root.cardtheme.lib";
        private const string ModName = "Card Theme Extention Library";
        public const string Version = "1.0.0";
        internal Dictionary<string, CardThemeColor> themes = new Dictionary<string, CardThemeColor>();
        public IReadOnlyDictionary<string, CardThemeColor> Themes { get { return themes; } }

        public static CardThemeLib instance { get; private set; }

        void Awake()
        {
            instance = this;
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
            List<CardThemeColor> cardThemeColors = CardChoice.instance.cardThemes.ToList();
            cardThemeColors.ForEach(theme => {
                CreateOrGetType(theme.themeType.ToString(), theme);
            });

        }

        public CardThemeColor.CardThemeColorType CreateOrGetType(string name, CardThemeColor themeColor = null)
        {
            if (themes.ContainsKey(name)) return themes[name].themeType;
            else
            {
                CardThemeColor.CardThemeColorType themeType = (CardThemeColor.CardThemeColorType)themes.Count;
                themeColor.themeType = themeType;
                themes.Add(name, themeColor);
                CardChoice.instance.cardThemes = themes.Values.ToArray();
                return themeType;
            }
        }

    }
}
