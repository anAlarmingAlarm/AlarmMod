using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AlarmMod.Systems
{
    public class AlarmSystem : ModSystem
    {
        public override void AddRecipeGroups()
        {
            RecipeGroup.RegisterGroup(nameof(ItemID.GoldBar), new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.GoldBar)}",
                ItemID.GoldBar,
                ItemID.PlatinumBar));

            RecipeGroup.RegisterGroup(nameof(ItemID.CopperBar), new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.CopperBar)}",
                ItemID.CopperBar,
                ItemID.TinBar));

            RecipeGroup.RegisterGroup(nameof(ItemID.MagicMirror), new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.MagicMirror)}",
                ItemID.MagicMirror,
                ItemID.IceMirror));

            RecipeGroup.RegisterGroup(nameof(ItemID.Sapphire), new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Gem",
                ItemID.Sapphire,
                ItemID.Ruby,
                ItemID.Emerald,
                ItemID.Topaz,
                ItemID.Amethyst,
                ItemID.Diamond,
                ItemID.Amber));

            RecipeGroup.RegisterGroup(nameof(ItemID.GrayPressurePlate), new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Pressure Plate",
                ItemID.GrayPressurePlate,
                ItemID.BrownPressurePlate,
                ItemID.BluePressurePlate,
                ItemID.LihzahrdPressurePlate,
                ItemID.RedPressurePlate,
                ItemID.GreenPressurePlate,
                ItemID.YellowPressurePlate,
                ItemID.OrangePressurePlate,
                ItemID.WeightedPressurePlatePink,
                ItemID.WeightedPressurePlateOrange,
                ItemID.WeightedPressurePlatePurple,
                ItemID.WeightedPressurePlateCyan,
                ItemID.ProjectilePressurePad));
        }
    }
}