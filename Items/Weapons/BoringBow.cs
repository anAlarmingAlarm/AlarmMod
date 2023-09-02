using AlarmMod.Items.Accessories;
using Microsoft.Xna.Framework;
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

        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity *= 1.5f;
        }
    }

    public class BoringBowDrop : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            // 0.5% chance to drop from any tier of Etherian Goblin, Goblin Bomber, Javelin Thrower, or Wyvern
            if (npc.type >= NPCID.DD2GoblinT1 && npc.type <= NPCID.DD2JavelinstT3)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.BoringBow, 200));
            }
        }
    }
}
