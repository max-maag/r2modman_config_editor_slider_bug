using BepInEx;
using BepInEx.Configuration;
using System;

namespace BugExample
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    public class BugExample : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "mmaag";
        public const string PluginName = "r2modman_config_editor_slider_bug_example";
        public const string PluginVersion = "1.0.0";


        public static ConfigEntry<float> SliderFloatWhole { get; set; }
        public static ConfigEntry<float> SliderFloatDecimal { get; set; }

        public static ConfigEntry<int> SliderInt { get; set; }

        public void Awake()
        {
            Log.Init(Logger);

            BindConfigEntries();

            LogConfigValues();
        }

        private void BindConfigEntries()
        {
            SliderFloatWhole = ConfigValueRange<float>(
                "slider_float_whole",
                "A slider with float value and whole number bounds.",
                0.5f,
                0f,
                1f
            );

            SliderFloatDecimal = ConfigValueRange<float>(
                "slider_float_decimal",
                "A slider with float value and decimal number bounds.",
                0.5f,
                0.1f,
                0.9f
            );

            SliderInt = ConfigValueRange<int>(
                "slider_int",
                "A slider with int value and bounds. This works correctly.",
                50,
                0,
                100
            );

            ConfigEntry<T> ConfigValueRange<T>(string key, string desc, T defaultValue, T min, T max) where T : IComparable
            {
                return Config.Bind<T>(
                    "settings",
                    key,
                    defaultValue,
                    new ConfigDescription(
                        desc,
                        new AcceptableValueRange<T>(min, max)
                    )
                );
            }
        }

        private void LogConfigValues()
        {
            Log.Info($"{SliderFloatWhole.Definition.Key}: {SliderFloatWhole.Value}");
            Log.Info($"{SliderFloatDecimal.Definition.Key}: {SliderFloatDecimal.Value}");
            Log.Info($"{SliderInt.Definition.Key}: {SliderInt.Value}");
        }
    }
}
