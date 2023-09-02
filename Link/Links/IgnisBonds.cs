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
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.value = Item.sellPrice(silver: 75);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item29;
            Item.useTime = 38;
            Item.useAnimation = 38;
        }

        public override void HoldItem(Player player)
        {
            if (player.TryGetModPlayer(out LinkPlayer lp))
            {
                lp.ignisBonds = true;

                lp.DrawLink(Main.rand.NextBool() ? DustID.Torch : DustID.SolarFlare);
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
                .AddIngredient(ItemID.HellstoneBar, 15)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
