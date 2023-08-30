using AlarmMod.Items.Accessories;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Weapons
{
    public class BoringBow : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.BoringBow;
        }

        public override void SetDefaults(Item entity)
        {
            entity.CloneDefaults(ItemID.TendonBow);
            entity.damage = 34;
            entity.useTime = 23;
            entity.useAnimation = 23;
            entity.knockBack = 0;
            entity.rare = ItemRarityID.Green;
            entity.SetNameOverride("Boring Bow");
        }
    }

    public class BoringBowDrop : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            // Any tier of Etherian Goblin, Goblin Bomber, Javelin Thrower, or Wyvern
            if (npc.type >= NPCID.DD2GoblinT1 && npc.type <= NPCID.DD2JavelinstT3)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.BoringBow, 200));
            }
        }
    }
}
