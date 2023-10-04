using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Accessories
{
    public class PersonalShieldGenerator : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 38;
            Item.value = Item.buyPrice(silver: 15);
            Item.rare = ItemRarityID.Green;
            Item.expert = true;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.pStone = true;
            player.empressBrooch = true;
            player.moveSpeed += 0.1f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CharmofMyths)
                .AddIngredient(ItemID.EmpressFlightBooster)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }

    public class PSGPlayer : ModPlayer
    {
        public bool equipped;

        public override void ResetEffects()
        {
            equipped = false;
        }
    }
}