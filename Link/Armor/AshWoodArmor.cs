using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Link.Armor
{
    // Modify the Ash Wood set to be a Link set (it's not like it was being used much before)
    public class AshWoodHelmet : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstatiation)
        {
            return item.type == ItemID.AshWoodHelmet;
        }

        public override void SetDefaults(Item item)
        {
            item.defense = 7;
            item.rare = ItemRarityID.Orange;
        }

        public override void UpdateEquip(Item item, Player player)
        {
            player.GetModPlayer<LinkPlayer>().linkDamage += 5;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Find(line => line.Name == "Defense").Text += "\n5% increased damage for link target";
        }
    }
    public class AshWoodBreastplate : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstatiation)
        {
            return item.type == ItemID.AshWoodBreastplate;
        }

        public override void SetDefaults(Item item)
        {
            item.defense = 8;
            item.rare = ItemRarityID.Orange;
        }

        public override void UpdateEquip(Item item, Player player)
        {
            player.GetModPlayer<LinkPlayer>().linkArmorPen += 3;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Find(line => line.Name == "Defense").Text += "\nIncreases link target's armor penetration by 3";
        }
    }
    public class AshWoodGreaves : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstatiation)
        {
            return item.type == ItemID.AshWoodGreaves;
        }

        public override void SetDefaults(Item item)
        {
            item.defense = 8;
            item.rare = ItemRarityID.Orange;
        }

        public override void UpdateEquip(Item item, Player player)
        {
            player.GetModPlayer<LinkPlayer>().linkDefense += 6;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Find(line => line.Name == "Defense").Text += "\n6 defense for link target";
        }
    }
    public class AshWoodRecipe : ModSystem
    {
        // Add 3 Hellstone to the recipes of each piece of the Ash Wood armor set
        public override void PostAddRecipes()
        {
            foreach (Recipe recipe in Main.recipe)
            {
                if (recipe.HasIngredient(ItemID.AshWood) && recipe.HasTile(TileID.WorkBenches) && (recipe.HasResult(ItemID.AshWoodHelmet) || recipe.HasResult(ItemID.AshWoodBreastplate) || recipe.HasResult(ItemID.AshWoodGreaves)))
                {
                    recipe.AddIngredient(ItemID.Hellstone, 3);
                }
            }
        }
    }
}
