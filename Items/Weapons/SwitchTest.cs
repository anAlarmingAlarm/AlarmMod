using AlarmMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Weapons
{
    public class SwitchTest : ModItem
    {
        public int attackType = 0; // keeps track of which attack it is

        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 48;
            Item.value = Item.sellPrice(gold: 2, silver: 50);
            Item.rare = ItemRarityID.Green;

            // Note that use time and use animation don't actually affect the projectile since it's hardcoded into it
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.knockBack = 7;
            Item.autoReuse = true;
            Item.damage = 62;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            
            Item.shoot = ModContent.ProjectileType<SwitchTestProjectile>();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                attackType = (attackType + 1) % 2; // Switch attack types
                for (int i = 0; i < 12; i++)
                    Dust.NewDust(player.position, player.width, player.height, DustID.Teleporter);
            }
            else
            {
                Projectile.NewProjectile(source, position, velocity, type, damage / (attackType + 1), knockback / (attackType + 1), Main.myPlayer, attackType);
            }
            
            return false;
        }

        public override bool MeleePrefix()
        {
            return true; // return true to allow weapon to have melee prefixes (e.g. Legendary)
        }
    }
}