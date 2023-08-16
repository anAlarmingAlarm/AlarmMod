﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Consumables
{
    public class LifeOrb : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LifeFruit);
            Item.width = 22;
            Item.height = 22;
            Item.value = 225000 * 5;
        }

        public override bool? UseItem(Player player)
        {
            if (player.statLifeMax >= 400)
            {
                // Returning null will make the item not be consumed
                return null;
            }

            player.statLifeMax = 400;
            player.statLife = 400;

            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LifeCrystal, 15)
                .Register();
        }
    }
}