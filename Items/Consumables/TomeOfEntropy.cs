using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Consumables
{
    public class TomeOfEntropy : ModItem
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
            if (Main.hardMode)
            {
                // Returning null will make the item not be consumed
                return null;
            }

            Main.hardMode = true;

            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Book)
                .AddIngredient(ItemID.SoulofLight, 5)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}