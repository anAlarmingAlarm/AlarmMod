using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using AlarmMod.Projectiles;
using AlarmMod.Items.Consumables;

namespace AlarmMod.Items.Weapons
{
    public class SpectralFlurry : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.ArmorPenetration = 500;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 8;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.autoReuse = true;
            Item.width = 28;
            Item.height = 32;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Pink;
            Item.shoot = ModContent.ProjectileType<SpectralSword>();
            Item.shootSpeed = 14f;
            Item.value = Item.sellPrice(gold: 1);
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item71;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(7);
            position += Vector2.Normalize(velocity) * 12f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }
            return false; // don't fire the original projectile
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<SpectralBlade>()
                .AddIngredient(ItemID.HallowedBar, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
