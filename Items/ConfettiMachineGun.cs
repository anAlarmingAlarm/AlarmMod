using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items
{
    public class ConfettiMachineGun : ModItem
    {
        //Hijack the Material line in the tooltip to add another line to the tooltip describing the item's function
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (tooltips.Find(line => line.Name == "Material") != null)
                tooltips.Find(line => line.Name == "Material").Text = "Rapidly fires confetti\nMaterial";
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ConfettiGun);

            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.width = 56;
            Item.height = 32;
            Item.maxStack = 1;
            Item.autoReuse = true;
            Item.consumable = false;
            Item.shootSpeed *= 2f;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.buyPrice(platinum: 2);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(3)); // slight inaccuracy to give it more visual oomph

            // make the confetti actually come out of the barrels
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 20f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
        }
    }

    public class PartyGirl : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.PartyGirl;
        }

        public override void ModifyShop(NPCShop shop)
        {
            shop.Add(ModContent.ItemType<ConfettiMachineGun>());
        }
    }
}
