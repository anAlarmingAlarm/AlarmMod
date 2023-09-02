using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Link.Armor.Spiritual
{
    // The AutoloadEquip attribute automatically attaches an equip texture to this item.
    // Providing the EquipType.Body value here will result in TML expecting a X_Body.png file to be placed next to the item's main texture.
    [AutoloadEquip(EquipType.Body)]
    public class SpiritualGarb : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.value = Item.sellPrice(gold: 2);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 13;
        }

        public override void UpdateEquip(Player player)
        {
            player.maxMinions++;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Bone, 90)
                .AddIngredient(ItemID.SpectreBar, 5)
                .AddIngredient(ItemID.BeetleHusk, 2)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}