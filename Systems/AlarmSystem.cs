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

            RecipeGroup.RegisterGroup(nameof(ItemID.Sapphire), new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Gem",
                ItemID.Sapphire,
                ItemID.Ruby,
                ItemID.Emerald,
                ItemID.Topaz,
                ItemID.Amethyst,
                ItemID.Diamond,
                ItemID.Amber));
        }
    }
}