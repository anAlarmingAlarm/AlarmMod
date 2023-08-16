using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Consumables
{
    public class ManaOrb : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LifeFruit);
            Item.width = 22;
            Item.height = 22;
            Item.value = 22500 * 5;
        }

        public override bool? UseItem(Player player)
        {
            if (player.statManaMax >= 200)
            {
                // Returning null will make the item not be consumed
                return null;
            }

            player.statManaMax = 200;
            player.statMana = 200;

            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ManaCrystal, 9)
                .Register();
        }
    }
}