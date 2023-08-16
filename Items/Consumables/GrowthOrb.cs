using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Consumables
{
    public class GrowthOrb : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LifeFruit);
            Item.width = 22;
            Item.height = 22;
            Item.value = 400000 * 5;
        }

        public override bool? UseItem(Player player)
        {
            if (player.statLifeMax >= 500 || player.statLifeMax < 400)
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
                .AddIngredient(ItemID.LifeFruit, 20)
                .Register();
        }
    }
}