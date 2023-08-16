using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AlarmMod.Items.Consumables;

namespace AlarmMod.Items.Consumables
{
    public class OriginOrb : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LifeFruit);
            Item.width = 22;
            Item.height = 22;
            Item.value = 847500 * 5;
        }

        public override bool? UseItem(Player player)
        {
            if (player.statLifeMax >= 500 && player.statManaMax >= 200)
            {
                // Returning null will make the item not be consumed
                return null;
            }
            else if (player.statLifeMax >= 500)
            {
                player.statManaMax = 200;
                player.statMana = 200;
            }
            else if (player.statManaMax >= 200)
            {
                player.statLifeMax = 500;
                player.statLife = 500;
            }

            player.statLifeMax = 500;
            player.statLife = 500;
            player.statManaMax = 200;
            player.statMana = 200;

            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LifeCrystal, 15)
                .AddIngredient(ItemID.ManaCrystal, 9)
                .AddIngredient(ItemID.LifeFruit, 20)
                .Register();

            CreateRecipe()
                .AddIngredient<LifeOrb>()
                .AddIngredient<GrowthOrb>()
                .AddIngredient<ManaOrb>()
                .Register();

            CreateRecipe()
                .AddIngredient<PowerOrb>()
                .AddIngredient<GrowthOrb>()
                .Register();

            CreateRecipe()
                .AddIngredient<VitalityOrb>()
                .AddIngredient<ManaOrb>()
                .Register();
        }
    }
}