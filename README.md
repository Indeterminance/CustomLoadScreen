# Custom Load Screen

Custom Load Screens is a mod for Subnautica intended to bring back a bit of flavor to Subnautica's long loading time. After all, you may as well look at something nice while you wait for the game to load!

This mod was originally created by EckoTheCat/kylinator25 as a QMod, but they've since moved on to other things. This updated fork aims to continue CLS' legacy, and also works with BepInEx, making this the **first version of CLS compatible with Subnautica's Living Large Update**.

# Important changes
 - A configuration file can be found in BepInEx's `config` directory after launching the game.
 - All loading screens are now stored in `Subnautica/loading_screens/` instead of `Subnautica/QMods/CustomLoadScreen/`.

# Configuration

Released in this fork are a couple of configuration options pertaining to the randomization of loading screens. Those are as follows:

`SelectRandomLoadingScreens` - Determines whether loading screen randomization is used. If `false`, the loading screen is selected by `NonRandomLoadingScreen` instead. Defaults to `false`.

`VanillaScreenInRandom` - Determines whether Subnautica's vanilla loading screen is allowed to appear as a random loading screen. If `false`, Subnautica's vanilla loading screen won't appear as a random loading screen. Only used if `SelectRandomLoadingScreens` is set to `true`. Defaults to `false`.

`NonRandomLoadingScreen` - Used to determine what loading screen is shown if `SelectRandomLoadingScreens` is set to `false`. This takes the filename shown in the `loading_screens` directory. If left empty or set to a filename not in `loading_screens`, this will default to the vanilla loading screen.
