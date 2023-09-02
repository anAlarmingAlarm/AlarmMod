using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Link.Links
{
    public class SpectralChains : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 44;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.value = Item.sellPrice(silver: 50);
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
                lp.spectralChains = true;

                lp.DrawLink(180); // Dungeon Spirit
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

    public class SpectralChainsDrop : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            // ~0.667% chance to drop from all Angry Bones variants and Cursed Skull, ~1.333% chance to drop from Dark Casters
            if (npc.type == NPCID.AngryBones || npc.type == NPCID.AngryBonesBig || npc.type == NPCID.AngryBonesBigHelmet || npc.type == NPCID.AngryBonesBigMuscle ||
                npc.type == NPCID.ShortBones || npc.type == NPCID.BigBoned      || npc.type == NPCID.CursedSkull)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SpectralChains>(), 150));
            else if (npc.type == NPCID.DarkCaster)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SpectralChains>(), 75));

        }
    }
}
