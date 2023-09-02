using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Link.Armor.Spiritual
{
    // The AutoloadEquip attribute automatically attaches an equip texture to this item.
    // Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
    [AutoloadEquip(EquipType.Head)]
    public class SpiritualJingasa : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.value = Item.sellPrice(gold: 1, silver: 50);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 10;
        }

        public override void UpdateEquip(Player player)
        {
            player.maxMinions++;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<SpiritualGarb>() && legs.type == ModContent.ItemType<SpiritualZori>();
        }

        // UpdateArmorSet allows you to give set bonuses to the armor.
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "While linked, summon magic flames when no enemies are near\nWhen enemies are near, your flames will attack them";
            if (player.GetModPlayer<LinkPlayer>().spectralChains)
                player.setBonus += "\nSynergy: Flames are always generated, even if enemies are near";

            player.GetModPlayer<LinkPlayer>().spiritualEffect = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Bone, 70)
                .AddIngredient(ItemID.SpectreBar, 3)
                .AddIngredient(ItemID.BeetleHusk)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}