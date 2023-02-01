using System;
using HarmonyLib;
using BepInEx;
using BepInEx.Logging;
using System.IO;
using System.Collections.Generic;
using BepInEx.Configuration;

namespace CustomLoadScreen
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    public class CLS_Main : BaseUnityPlugin
    {
        // Mod info
        private const string myGUID = "com.indeterminance.customloadscreen";
        private const string pluginName = "Custom Load Screens";
        private const string versionString = "1.3.0";

        // Harmony stuff
        public static string screens_directory = Environment.CurrentDirectory + "\\loading_screens";
        private static readonly Harmony harmony = new Harmony(myGUID);
        public static ManualLogSource logger = new ManualLogSource(pluginName);

        // Loading screen data
        public static System.Random rand = new System.Random();
        public static int currentImageChoice;
        public static List<string> images;
        public static ConfigEntry<bool> selectRandomImages;
        public static ConfigEntry<bool> includeVanillaScreenInRandom;
        public static ConfigEntry<string> nonRandomImageChoice;

        private void Awake()
        {

            selectRandomImages = Config.Bind("General", "SelectRandomLoadingScreens", false, "Enable loading screen randomization.");
            includeVanillaScreenInRandom = Config.Bind("General", "VanillaScreenInRandom", false, "Include the default vanilla loading screen in random selections.");
            nonRandomImageChoice = Config.Bind("General", "NonRandomLoadingScreen", "", "Filename of loading screen to use. Only used if random loading screens are disabled. Leave blank for the vanilla loading screen.");
            CollectLoadingScreens();

            harmony.PatchAll();
            logger = Logger;

            if (!Directory.Exists(screens_directory)) Directory.CreateDirectory(screens_directory);
        }

        public void CollectLoadingScreens()
        {
            // Gather all files in /loading_screens, filter .png ang .jpg images, and dump to image list
            List<string> files = new List<string>();
            files.AddRange(Directory.GetFiles(screens_directory));
            logger.Log(LogLevel.Info, $"Found {files.Count} files. Filtering images...");
            images = new List<string>();
            foreach (string file in files)
            {
                if (file.EndsWith(".jpg") | file.EndsWith(".png"))
                {
                    logger.Log(LogLevel.Debug, "Is image!!!!");
                    images.Add(file);
                }
            }
            logger.Log(LogLevel.Info, $"Found {images.Count} images:");
            foreach (string image in images)
            {
                logger.Log(LogLevel.Info, image);
            }

            // Initialize our first random image, because why not
            if (selectRandomImages.Value)
            {
                if (includeVanillaScreenInRandom.Value) currentImageChoice = rand.Next(-1, images.Count);
                else currentImageChoice = rand.Next(0, images.Count);
            }
            else
            {
                currentImageChoice = ImageNameIndex(nonRandomImageChoice.Value);
            }
        }

        public static int ImageNameIndex(string shortname)
        {
            char[] bs = new char[] { '\\' };
            for (int i=0; i < images.Count; i++)
            {
                if (images[i].Split(bs).GetLast() == shortname) return i;
            }
            return -1;
        }
    }
}
