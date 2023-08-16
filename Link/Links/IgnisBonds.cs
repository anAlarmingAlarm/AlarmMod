using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Link.Links
{
    public class IgnisBonds : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 44;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.useTime = 38;
            Item.useAnimation = 38;
            Item.knockBack = 6;
        }

        public override void HoldItem(Player player)
        {
            if (player.TryGetModPlayer(out LinkPlayer lp))
            {
                lp.linkRange = 10;
                lp.linkDefense = 4;
            }
        }

        public override bool? UseItem(Player player)
        {
            if (player.TryGetModPlayer(out LinkPlayer lp))
            {
                return lp.AttemptLink();
            }
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup("Wood", 8)
                .AddIngredient(ItemID.FallenStar, 3)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
