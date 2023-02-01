using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace CustomLoadScreen
{
    // Patch the init function of the loading screen to patch our desired graphics in
    [HarmonyPatch(typeof(uGUI_SceneLoading))]
    public static class uGUI_SceneLoadingPatch
    {
        [HarmonyPatch("Init")]
        public static void Postfix(uGUI_SceneLoading __instance)
        {
            if (CLS_Main.currentImageChoice == -1) return;

            Sprite img = ImageUtils.LoadSprite(CLS_Main.images[CLS_Main.currentImageChoice]);
            Image[] graphics = __instance.loadingBackground.GetComponentsInChildren<Image>();

            foreach (Image graphic in graphics)
            {
                graphic.sprite = img;
            }
        }
    }

    // Patch the fadeout of the start screen to generate random numbers for us
    [HarmonyPatch(typeof(StartScreenFade))]
    public static class PressStartPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void Prefix()
        {
            if (CLS_Main.selectRandomImages.Value)
            {
                if (CLS_Main.includeVanillaScreenInRandom.Value) CLS_Main.currentImageChoice = CLS_Main.rand.Next(-1, CLS_Main.images.Count);
                else CLS_Main.currentImageChoice = CLS_Main.rand.Next(0, CLS_Main.images.Count);
            }
            else
            {
                CLS_Main.currentImageChoice = CLS_Main.ImageNameIndex(CLS_Main.nonRandomImageChoice.Value);
            }
        }

    }
}
