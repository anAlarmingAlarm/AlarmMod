﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Weapons
{
    public class Eyelander : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 56;
            Item.scale = 1.3f;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;

            Item.damage = 20;
            Item.useTime = 38;
            Item.useAnimation = 38;
            Item.knockBack = 6;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (player.HasBuff(BuffID.Tipsy))
            {
                damage.Base = 40;
            }
            else
            {
                damage.Base = 20;
            }

            base.ModifyWeaponDamage(player, ref damage);
        }

        public override float UseSpeedMultiplier(Player player)
        {
            if (player.HasBuff(BuffID.Tipsy))
            {
                return base.UseSpeedMultiplier(player) * 1.9f;
            }
            else
            {
                return base.UseSpeedMultiplier(player);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup("IronBar", 14)
                .AddTile(TileID.Hellforge)
                .Register();
        }
    }
}
