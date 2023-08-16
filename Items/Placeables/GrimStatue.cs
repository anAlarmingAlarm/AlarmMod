using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Placeables
{
    public class GrimStatue : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.AngelStatue);
            Item.createTile = ModContent.TileType<Tiles.GrimStatue>();
            Item.width = 24;
            Item.height = 32;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.StoneBlock, 50)
                .AddCondition(Condition.InGraveyard)
                .AddTile(TileID.HeavyWorkBench)
                .Register();
        }
    }
}