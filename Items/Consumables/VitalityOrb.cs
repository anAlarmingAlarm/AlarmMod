﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AlarmMod.Items.Consumables;

namespace AlarmMod.Items.Consumables
{
    public class VitalityOrb : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LifeFruit);
            Item.width = 22;
            Item.height = 22;
            Item.value = 625000 * 5;
        }

        public override bool? UseItem(Player player)
        {
            if (player.statLifeMax >= 500)
            {
                // Returning null will make the item not be consumed
                return null;
            }

            player.statLifeMax = 500;
            player.statLife = 500;

            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LifeCrystal, 15)
                .AddIngredient(ItemID.LifeFruit, 20)
                .Register();

            CreateRecipe()
                .AddIngredient<LifeOrb>()
                .AddIngredient<GrowthOrb>()
                .Register();
        }
    }
}