using AlarmMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Weapons
{
    public class SwitchAxe : ModItem
    {
        public int attackType = 0; // keeps track of which attack it is

        public override void SetDefaults()
        {
            Item.width = 112;
            Item.height = 122;
            Item.value = Item.sellPrice(gold: 2, silver: 50);
            Item.rare = ItemRarityID.Green;

            // Note that use time and use animation don't actually affect the projectile since it's hardcoded into it
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.knockBack = 6;
            Item.autoReuse = true;
            Item.damage = 58;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            
            Item.shoot = ModContent.ProjectileType<SwitchAxeProjectile>();
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

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup("IronBar", 16)
                .AddRecipeGroup(nameof(ItemID.CopperBar), 6)
                .AddIngredient(ItemID.MeteoriteBar, 4)
                .AddRecipeGroup(nameof(ItemID.GrayPressurePlate), 2)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}