using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Link.Links
{
    public class AfflictedLinks : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 44;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item29;
            Item.useTime = 38;
            Item.useAnimation = 38;
            Item.knockBack = 6;
        }

        public override void HoldItem(Player player)
        {
            if (player.TryGetModPlayer(out LinkPlayer lp))
            {
                lp.linkRange = 12;
                if (lp.afflictedLinks < 0)
                {
                    lp.afflictedLinks = 0;
                }
                else if (lp.afflictedLinks > 0)
                {
                    lp.afflictedLinks--;
                }
                lp.DrawLink(DustID.CorruptPlants);
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
                .AddIngredient(ItemID.DemoniteBar, 12)
                .AddIngredient(ItemID.ShadowScale, 8)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
