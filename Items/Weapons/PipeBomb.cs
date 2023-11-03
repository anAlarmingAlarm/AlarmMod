using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Weapons
{
    public class PipeBomb : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatCountAsBombsForDemolitionistToSpawn[Type] = true;
            Item.ResearchUnlockCount = 99;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Grenade);
            Item.shoot = ModContent.ProjectileType<Projectiles.PipeBombProjectile>();
            Item.damage = 100;
            Item.width = 14;
            Item.height = 38;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.knockBack = 12;
            Item.value = Item.buyPrice(0, 0, 1, 50);
            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Grenade)
                .AddIngredient(ItemID.Wire, 10)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}