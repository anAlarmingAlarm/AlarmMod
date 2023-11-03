using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Accessories.Sigils
{
    public class InfusionSigil : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.WarriorEmblem);
            Item.width = 44;
            Item.height = 44;
            Item.value = Item.sellPrice(gold: 1);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //player.GetModPlayer<SigilPlayer>().sigil = SigilPlayer.Sigil.Infusion;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            return player.GetModPlayer<SigilPlayer>().sigil == SigilPlayer.Sigil.None;
        }

        public override void AddRecipes()
        {
            /*CreateRecipe()
                .AddIngredient<EmptySigil>()
                .AddIngredient(ItemID.FallenStar, 50)
                .AddTile(TileID.Anvils)
                .Register();*/
        }
    }
}