using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Weapons
{
    public class BonkingStick : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item1;

            Item.damage = 1;
            Item.useTime = 14;
            Item.useAnimation = 14;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.boss)
            {
                Main.NewText(target.FullName + " has been excluded from reality.", Color.Purple);
                target.active = false;
            }
            else
            {
                target.Teleport(new Vector2(0, 0));
            }
        }

        public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
        {
            Main.NewText(target.name + " has been excluded from reality.", Color.Red);
            target.active = false;
        }
    }

    /*public class BonkPlayer : ModPlayer
    {
        public void CeaseExistence()
        {
            for (int i = 0; i < Main.player.Length; i++)
            {
                if (Main.player[i] == Player)
                {
                    Main.NewText(Player.name + " has been excluded from reality.", Color.Yellow);
                    Main.player[i] = null;
                }
            }
        }
    }*/
}
