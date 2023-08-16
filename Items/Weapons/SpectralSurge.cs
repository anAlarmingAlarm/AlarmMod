using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using AlarmMod.Projectiles;
using Terraria.DataStructures;
using AlarmMod.Items.Consumables;

namespace AlarmMod.Items.Weapons
{
    public class SpectralSurge : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.ArmorPenetration = 500;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 10;
            Item.useTime = 7;
            Item.useAnimation = 19;
            Item.reuseDelay = 25;
            Item.autoReuse = true;
            Item.width = 28;
            Item.height = 32;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Yellow;
            Item.shoot = ModContent.ProjectileType<SpectralSword>();
            Item.shootSpeed = 14f;
            Item.value = Item.sellPrice(gold: 10);
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item71;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 3 + (player.GetModPlayer<SpectralSurgePlayer>().shotNum == 2 ? 1 : 0);
            float rotation = MathHelper.ToRadians(10);
            position += Vector2.Normalize(velocity) * 12f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }
            player.GetModPlayer<SpectralSurgePlayer>().shotNum += player.GetModPlayer<SpectralSurgePlayer>().shotNum < 3 ? 1 : -2; 
            return false; // don't fire the original projectile
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<SpectralFlurry>()
                .AddIngredient(ItemID.SpectreBar, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }

    public class SpectralSurgePlayer : ModPlayer
    {
        public int shotNum = 1;

        public override void PostUpdateBuffs()
        {
            if (Player.HasBuff(BuffID.Cursed))
            {
                shotNum = 1;
            }
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            shotNum = 1;
        }
    }
}
