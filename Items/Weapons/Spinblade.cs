using System;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using AlarmMod.Projectiles;
using Terraria.Audio;

namespace AlarmMod.Items.Weapons
{
    public class Spinblade : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LightDisc);

            Item.width = 28;
            Item.height = 32;
            Item.maxStack = 1;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 50;
            Item.knockBack = 0;

            Item.value = Item.buyPrice(gold: 10);

            Item.shoot = ModContent.ProjectileType<SpinbladeProjectile>();
        }

        public override bool CanUseItem(Player player)
        {
            if (player.GetModPlayer<SpinbladePlayer>().remainingSpinblades > 0)
            {
                return true;
            }

            return false;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(SoundID.Item1, player.position);
            player.GetModPlayer<SpinbladePlayer>().remainingSpinblades--;
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HallowedBar, 24)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }

    public class SpinbladePlayer : ModPlayer
    {
        public int remainingSpinblades = 3;
    }
}
