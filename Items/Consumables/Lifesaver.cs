using AlarmMod.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Consumables
{
    public class Lifesaver : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LifeFruit);
            Item.width = 26;
            Item.height = 26;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.value = Item.buyPrice(gold: 25);
            Item.UseSound = SoundID.Item2;
            Item.buffType = ModContent.BuffType<LifesaverBuff>();
            Item.buffTime = 180;
        }

        public override bool CanUseItem(Player player)
        {
            return !player.HasBuff<LifesaverBuff>() && !player.HasBuff<LifesaverCooldown>();
        }
    }

    public class LifesaverNPC : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.Merchant;
        }

        public override void ModifyShop(NPCShop shop)
        {
            shop.Add(ModContent.ItemType<Lifesaver>());
        }
    }
}