using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using AlarmMod.Buffs;

namespace AlarmMod.Items.Accessories
{
    public class AsgardianRing : ModItem
    {
        public override string Texture => "AlarmMod/Items/Accessories/AsgardianRing";

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 20;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DodgePlayer>().canDodge = true;
        }
    }

    // Give the Asgardian Ring a 10% chance to drop from any Mimic (including Biome Mimics)
    public class AsgardianRingDrop : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.Mimic ||
                npc.type == NPCID.IceMimic ||
                npc.type == NPCID.BigMimicCorruption ||
                npc.type == NPCID.BigMimicCrimson ||
                npc.type == NPCID.BigMimicHallow ||
                npc.type == NPCID.BigMimicJungle)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AsgardianRing>(), 10));
            }
        }
    }

    public class AsgardianGauntlet : ModItem
    {
        public override string Texture => "AlarmMod/Items/Accessories/AsgardianGauntlet";

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.buyPrice(gold: 10);
            Item.rare = ItemRarityID.Lime;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += 0.15f;
            player.GetModPlayer<DodgePlayer>().canDodge = true;
            player.buffImmune[BuffID.Bewitched] = true; // Make the player unable to get Bewitched (we're giving them the effect permanently)
            player.GetModPlayer<DodgePlayer>().bewitched = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AsgardianRing>()
                .AddIngredient(ItemID.AvengerEmblem)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }

    public class AsgardAccessories : ModBuff
    {
        public override string Texture => "AlarmMod/Buffs/Dodge";

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.immune = true;
            player.immuneTime = 1;
            player.immuneAlpha = 175;

            if (player.buffTime[buffIndex] <= 1)
            {
                player.AddBuff(ModContent.BuffType<DodgeCooldown>(), 480);
            }
        }
    }
}