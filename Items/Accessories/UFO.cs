using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Accessories
{
    public class UFO : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 44;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Cyan;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.noFallDmg = true;
            player.hasJumpOption_Cloud = true;
            player.canJumpAgain_Cloud = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CobaltBar, 12)
                .AddIngredient(ItemID.SoulofFlight, 15)
                .AddTile(TileID.MythrilAnvil)
                .SortBefore(Main.recipe.First(recipe => recipe.createItem.wingSlot != -1)) // Places this recipe before any wing so every wing stays together in the crafting menu.
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.PalladiumBar, 12)
                .AddIngredient(ItemID.SoulofFlight, 15)
                .AddTile(TileID.MythrilAnvil)
                .SortBefore(Main.recipe.First(recipe => recipe.createItem.wingSlot != -1)) // see above
                .Register();
        }
    }
}