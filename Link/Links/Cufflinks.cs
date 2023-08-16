using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Link.Links
{
    public class Cufflinks : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 44;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.value = Item.buyPrice(gold: 3);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item29;
            Item.useTime = 38;
            Item.useAnimation = 38;
            Item.knockBack = 6;
        }

        public override void HoldItem(Player player)
        {
            if (player.TryGetModPlayer(out LinkPlayer lp))
            {
                lp.linkRange = 14;
                lp.cufflinks = true;
                lp.DrawLink(19); // Gold Coin projectile
            }
        }

        public override bool? UseItem(Player player)
        {
            if (player.TryGetModPlayer(out LinkPlayer lp))
            {
                return lp.AttemptLink();
            }
            return false;
        }
    }

   public class CufflinksNPC : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.Clothier;
        }

        public override void ModifyShop(NPCShop shop)
        {
            shop.Add<Cufflinks>();
        }
    }
}
