using BepInEx;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        public const string Version = "1.1.2";
        internal Dictionary<string, CardThemeColor> themes = new Dictionary<string, CardThemeColor>();
        internal bool firstRun = true;
        public IReadOnlyDictionary<string, CardThemeColor> Themes { get { return themes; } }

        public static CardThemeLib instance { get; private set; }

        void Awake()
        {
            instance = this;
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
            
        }
        public IEnumerator SetUpThemes()
        {
            for(int _ = 0; _<5; _++) yield return null;
            List<CardThemeColor> cardThemeColors;
            if (firstRun)
            {
                cardThemeColors = CardChoice.instance.cardThemes.ToList();
                cardThemeColors.ForEach(theme =>
                {
                    themes.Add(theme.themeType.ToString(), theme);
                });

                var allObjects = (ThemeAdder[]) Resources.FindObjectsOfTypeAll(typeof(ThemeAdder));
                foreach (var o in allObjects)
                {
                    if(o.enabled)
                        o.SetUp();
                }
               
                firstRun = false;
            }
            cardThemeColors = themes.Values.ToList();
            cardThemeColors.Sort((t1, t2) => t1.themeType.CompareTo(t2.themeType));
            CardChoice.instance.cardThemes = cardThemeColors.ToArray();
        }

        public CardThemeColor.CardThemeColorType CreateOrGetType(string name, CardThemeColor themeColor = null)
        {
            name = name.Replace(" ","");
            if (themes.ContainsKey(name)) return themes[name].themeType;
            else
            {
                CardThemeColor.CardThemeColorType themeType = (CardThemeColor.CardThemeColorType)themes.Count+9;
                themeColor.themeType = themeType;
                themes.Add(name, themeColor);
                return themeType;
            }
        }

    }
}
