using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Accessories.Sigils
{
    public class MirrorSigil : ModItem
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
            player.GetModPlayer<SigilPlayer>().sigil = SigilPlayer.Sigil.Mirror;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            return player.GetModPlayer<SigilPlayer>().sigil == SigilPlayer.Sigil.None;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<EmptySigil>()
                .AddRecipeGroup(nameof(ItemID.MagicMirror))
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}