using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Accessories.Sigils
{
    public class PhaseSigil : ModItem
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
            SigilPlayer sp = player.GetModPlayer<SigilPlayer>();
            sp.sigil = SigilPlayer.Sigil.Phase;
            if (sp.cooldown < 0)
                sp.cooldown = 600;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            return player.GetModPlayer<SigilPlayer>().sigil == SigilPlayer.Sigil.None;
        }
    }
}