﻿using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace AlarmMod.Items.Accessories
{
    public class MythicalInsignia : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 38;
            Item.value = Item.buyPrice(gold: 12);
            Item.rare = ItemRarityID.Yellow;
            Item.expert = true;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.pStone = true;
            player.empressBrooch = true;
            player.moveSpeed += 0.1f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CharmofMyths)
                .AddIngredient(ItemID.EmpressFlightBooster)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}