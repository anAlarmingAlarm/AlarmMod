using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Accessories.Sigils
{
    public class EmptySigil : ModItem
    {
        public override string Texture => "AlarmMod/Items/Accessories/Sigils/]EmptySigil";

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.WarriorEmblem);
            Item.width = 44;
            Item.height = 44;
            Item.value = Item.sellPrice(silver: 25);
            Item.accessory = false;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 30;
            Item.useAnimation = 30;
        }

        public override bool CanUseItem(Player player)
        {
            bool nearGhost = false;

            foreach (NPC npc in Main.npc)
            {
                if (npc.active && npc.life > 0 && (npc.type == NPCID.Ghost || npc.type == NPCID.PirateGhost)
                    && player.whoAmI == Main.myPlayer && npc.Distance(player.position) < 160)
                {
                    nearGhost = true;
                    break;
                }
            }

            return nearGhost;
        }

        public override bool? UseItem(Player player)
        {
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && npc.life > 0 && (npc.type == NPCID.Ghost || npc.type == NPCID.PirateGhost)
                    && player.whoAmI == Main.myPlayer && npc.Distance(player.position) < 160)
                {
                    player.HeldItem.TurnToAir();
                    npc.life = 0;
                    player.QuickSpawnItem(player.HeldItem.GetSource_FromThis(), ModContent.ItemType<PhaseSigil>());

                    for (int i = 0; i < 10; i++)
                    {
                        Dust.NewDust(player.position, player.width, player.height, DustID.Cloud);
                        Dust.NewDust(npc.position, npc.width, npc.height, DustID.Cloud);
                    }

                    break;
                }
            }

            return null;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.IronBar, 15)
                .AddIngredient(ItemID.MeteoriteBar, 8)
                .AddIngredient(ItemID.Bone, 30)
                .AddIngredient(ItemID.Diamond, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}