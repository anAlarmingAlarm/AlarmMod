using AlarmMod.Projectiles;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Weapons
{
    public class TelementalStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Terraria.Item.staff[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.AmethystStaff);

            Item.DefaultToStaff(ModContent.ProjectileType<TeleBolt>(), 9f, 30, 10);
            Item.SetWeaponValues(30, 0, 0);
            Item.SetShopValues(ItemRarityColor.Blue1, 500);
            Item.width = 42;
            Item.height = 42;
            Item.useStyle = ItemUseStyleID.Shoot;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AmethystStaff)
                .AddIngredient(ItemID.FallenStar, 15)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}