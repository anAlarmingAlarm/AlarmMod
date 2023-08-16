using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using AlarmMod.Projectiles;

namespace AlarmMod.Items.Weapons
{
    public class SpectralBlade : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.ArmorPenetration = 500;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 6;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.autoReuse = false;
            Item.width = 28;
            Item.height = 32;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<SpectralSword>();
            Item.shootSpeed = 14f;
            Item.value = Item.sellPrice(silver: 10);
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item71;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Book)
                .AddIngredient(ItemID.FallenStar, 10)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
