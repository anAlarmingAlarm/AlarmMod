using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlarmMod.Items.Accessories.Sigils
{
    public class SigilPlayer : ModPlayer
    {
        public enum Sigil
        {
            None,
            Acromania,
            //Infusion,
            Magnetic,
            Mirror,
            Phase
        }

        public Sigil sigil;

        const float maxLight = 16.8f;

        int oldMana;                // Old mana value, used for Infusion
        public int cooldown = -1;   // Equip/unequip cooldown used for Phase
        public Vector2 mouse;       // Mouse location used for Magnetic

        public override void ResetEffects()
        {
            //if (sigil != Sigil.Infusion)
            //    oldMana = -6969;

            if (sigil != Sigil.Phase && cooldown > 0)
                cooldown = -600;
            else if (cooldown > 0)
                cooldown--;
            else if (cooldown < 0)
                cooldown++;
            
            sigil = Sigil.None;
        }

        public override void PostUpdateEquips()
        {
            if (sigil == Sigil.Acromania)
            {
                Player.moveSpeed += Player.GetDamage(DamageClass.Generic).Additive + Player.GetDamage(Player.HeldItem.DamageType).Additive - 2;
                Player.GetAttackSpeed(Player.HeldItem.DamageType) += Player.moveSpeed - 1;
                Player.GetDamage(Player.HeldItem.DamageType) = StatModifier.Default / 2;
            }
        }

        public override void PostUpdate()
        {
            /*if (sigil == Sigil.Infusion)
            {
                if (oldMana == -6969)
                {
                    Player.statMana = Math.Min(Player.statManaMax2 - 10, 50);
                }
                else if (Player.statMana - oldMana > 0)
                {
                    Main.NewText("heal " + (Player.statMana - oldMana));
                    Player.Heal(Player.statMana - oldMana);
                }
                else if (Player.statMana - oldMana < 0)
                {
                    Main.NewText("hurt " + ((Player.statMana - oldMana) * -1));
                    Player.Hurt(PlayerDeathReason.ByCustomReason(Player.name + " used too much power."), (Player.statMana - oldMana) * -1, 0);
                }
                oldMana = Player.statMana;
                Player.statMana = Math.Min(Player.statManaMax2 - 10, 30);
            }
            else*/ if (sigil == Sigil.Phase)
            {
                if (cooldown <= 0)
                {
                    Player.GetDamage(DamageClass.Generic).Scale(0);
                    if (cooldown == 0)
                    {
                        Player.aggro -= 696969;
                    }
                }
            }
            else if (Main.myPlayer == Player.whoAmI)
            {
                mouse = Main.MouseWorld;
            }
        }

        public override bool CanHitNPC(NPC target) { return cooldown >= 0; }

        public override bool? CanHitNPCWithItem(Item item, NPC target) { return cooldown >= 0; }

        public override bool? CanHitNPCWithProj(Projectile proj, NPC target) { return cooldown >= 0; }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (sigil == Sigil.Mirror)
            {
                if (Lighting.Brightness((int)Player.Center.X / 16, (int)Player.Center.Y / 16) > maxLight / 2
                    || (Player.Center.Y < Main.worldSurface && Main.IsItDay()))
                {
                    modifiers.FinalDamage *= 1.25f;
                }
                else
                {
                    modifiers.FinalDamage *= 0.75f;
                }
            }
        }

        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot) { return !(sigil == Sigil.Phase && cooldown == 0); }

        public override bool CanBeHitByProjectile(Projectile proj) { return !(sigil == Sigil.Phase && cooldown == 0); }

        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            if (sigil == Sigil.Mirror)
            {
                if (Lighting.Brightness((int)Player.Center.X / 16, (int)Player.Center.Y / 16) > maxLight / 2
                    || (Player.Center.Y < Main.worldSurface && !Main.IsItDay()))
                {
                    modifiers.FinalDamage *= 1.25f;
                }
                else
                {
                    modifiers.FinalDamage *= 0.75f;
                }
            }
        }
    }

    /*public class InfusionSigilNoPotion : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.healMana > 0;
        }

        public override bool CanUseItem(Item item, Player player)
        {
            return player.GetModPlayer<SigilPlayer>().sigil != SigilPlayer.Sigil.Infusion;
        }
    }*/

    public class MagneticSigilReverse : GlobalProjectile
    {
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            SigilPlayer sp;
            try
            {
                sp = Main.player[projectile.owner].GetModPlayer<SigilPlayer>();
            }
            catch { return; }
            if (sp.sigil == SigilPlayer.Sigil.Magnetic)
            {
                projectile.position = sp.mouse;
                projectile.velocity *= -1;
            }
        }
    }
}