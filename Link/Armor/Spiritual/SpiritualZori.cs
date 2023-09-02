using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Link.Armor.Spiritual
{
    // The AutoloadEquip attribute automatically attaches an equip texture to this item.
    // Providing the EquipType.Legs value here will result in TML expecting a X_Legs.png file to be placed next to the item's main texture.
    [AutoloadEquip(EquipType.Legs)]
    public class SpiritualZori : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1, silver: 75);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 11;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.1f;
            player.wingTimeMax = (int)(player.wingTimeMax * 1.1);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Bone, 80)
                .AddIngredient(ItemID.SpectreBar, 4)
                .AddIngredient(ItemID.BeetleHusk)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}