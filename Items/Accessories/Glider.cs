using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Wings)]
    public class Glider : ModItem
    {
        public override void SetStaticDefaults()
        {
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(0, 4f, 1.5f);
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 22;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f; // Falling glide speed
            ascentWhenRising = 0f; // Rising speed
            maxCanAscendMultiplier = 0f;
            maxAscentMultiplier = 0f;
            constantAscend = 0.135f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 10)
                .AddRecipeGroup(RecipeGroupID.IronBar, 2)
                .AddTile(TileID.Loom)
                .SortBefore(Main.recipe.First(recipe => recipe.createItem.wingSlot != -1)) // Places this recipe before any wing so every wing stays together in the crafting menu.
                .Register();
        }
    }
}