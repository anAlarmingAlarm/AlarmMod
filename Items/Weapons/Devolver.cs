﻿using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using AlarmMod.Projectiles;

namespace AlarmMod.Items.Weapons
{
    public class Devolver : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Revolver);

            Item.width = 54;
            Item.height = 32;
            Item.damage = 420;
            Item.crit = 0;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.knockBack = 12;
            Item.value = Item.sellPrice(gold: 3);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " shot themself."), damage, 0);

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1f, -1f);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Revolver)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
