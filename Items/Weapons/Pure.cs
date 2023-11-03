using AlarmMod.Items.Accessories.Sigils;
using AlarmMod.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Weapons
{
    public class Pure : ModItem
    {
        public override void SetStaticDefaults()
        {
            Terraria.Item.staff[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.MeteorStaff);

            Item.width = 58;
            Item.height = 58;

            Item.damage = 100;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.knockBack = 2;

            Item.SetShopValues(ItemRarityColor.Pink5, 500);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity.Y += 4; // Make the staff point downwards slightly
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, new(player.GetModPlayer<SigilPlayer>().mouse.X, player.Center.Y - Main.screenHeight / 2),
                new(0, Item.shootSpeed), type + Main.rand.Next(3), damage, knockback, player.whoAmI, 0f, 0.5f + Main.rand.NextFloat() * 0.3f);
            return false;
        }

        /*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 pointPoisition = new(player.position.X + player.width * 0.5f + (Main.rand.Next(201) * -player.direction) + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
            pointPoisition.X = (pointPoisition.X + player.Center.X) / 2f + Main.rand.Next(-200, 201);
            float cursorX = Main.mouseX + Main.screenPosition.X - pointPoisition.X + Main.rand.Next(-40, 41) * 0.03f;
            float cursorY = Main.mouseY + Main.screenPosition.Y - pointPoisition.Y;
            if (player.gravDir == -1f)
            {
                cursorY = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - pointPoisition.Y;
            }
            if (cursorY < 0f)
            {
                cursorY *= -1f;
            }
            if (cursorY < 20f)
            {
                cursorY = 20f;
            }
            float num112 = Item.shootSpeed / (float)Math.Sqrt(cursorX * cursorX + cursorY * cursorY);
            cursorX *= num112;
            cursorY *= num112;
            Projectile.NewProjectile(source, pointPoisition.X, pointPoisition.Y, cursorX * 0.75f, cursorY + Main.rand.Next(-40, 41) * 0.02f * 0.75f, type + Main.rand.Next(3), damage, knockback, player.whoAmI, 0f, 0.5f + Main.rand.NextFloat() * 0.3f);

            return false;
        }*/

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.MeteorStaff)
                .AddIngredient(ItemID.HallowedBar, 15)
                .AddIngredient(ItemID.FallenStar, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}