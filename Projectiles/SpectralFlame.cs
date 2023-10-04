/* Commented until Link is fully implemented
using AlarmMod.Link;
using AlarmMod.Link.Links;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Projectiles
{
    public class SpectralFlame : ModProjectile
    {
        private const float distance = 32f;
        private const float speed = 3f;
        private Player owner;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.SpiritFlame);
            Projectile.DamageType = DamageClass.Summon;
            Projectile.tileCollide = false;
        }

        public override void OnSpawn(IEntitySource source)
        {
            // ai[0] tracks which flame this is
            // ai[1] tracks what state the flame is in: -1 for following player, higher than that for attacking that index of Main.npc
            owner = Main.player[Projectile.owner];

            Projectile.ai[0] = owner.GetModPlayer<SpectralFlamePlayer>().SpawnFlame(Projectile);
            if (Projectile.ai[0] == -1)
                Projectile.Kill();
            else
                Projectile.ai[1] = -1;
        }

        public override void AI()
        {
            if (Projectile.ai[1] == -1)
            {
                // Following state
                Projectile.Center = owner.Center + new Vector2(distance, 0).RotatedBy(MathHelper.ToRadians(Projectile.ai[0] * 90
                     + Projectile.ai[0] > 4 ? 45 : 0)); // Rotate by 45 degrees if above 8, to continue the circle without overlapping
            }
            else
            {
                // Attacking state
                NPC target = null;
                try // Just in case
                {
                    target = Main.npc[(int)Projectile.ai[1]];
                }
                catch (IndexOutOfRangeException) {}

                if (target != null || !target.active || target.life <= 0)
                {
                    Projectile.ai[1] = -1;
                    owner.GetModPlayer<SpectralFlamePlayer>().timer = 0; // Reset attack cooldown since this attack failed
                }
                else
                {
                    Projectile.Center += Vector2.Multiply(Vector2.Normalize(Projectile.Center.DirectionTo(target.Center)), speed);
                }
            }

            // Animate projectile
            if (++Projectile.frameCounter > 20)
                Projectile.frameCounter = 0;
            if (Projectile.frameCounter % 4 == 0)
                Projectile.frame = Projectile.frameCounter / 4;
        }

        public override void Kill(int timeLeft)
        {
            // Clear this from the flames array when it is removed
            owner.GetModPlayer<SpectralFlamePlayer>().flames[(int)Projectile.ai[0]] = null;
        }
    }

    public class SpectralFlamePlayer : ModPlayer
    {
        public List<Projectile> flames = new(8);
        public int timer;

        public int SpawnFlame(Projectile proj)
        {
            int max = Player.GetModPlayer<LinkPlayer>().spiritualEffect ? 8 : 4;

            for (int i = 0; i < max; i++)
            {
                if (flames[i] == null)
                {
                    flames[i] = proj;
                    return i;
                }
            }
            return -1;
        }

        public override void PreUpdate()
        {
            LinkPlayer modPlayer = Player.GetModPlayer<LinkPlayer>();
            int max = Player.GetModPlayer<LinkPlayer>().spiritualEffect ? 8 : 4;
            int selectedFlame = -1;
            bool found = false;

            if (timer == 0 && (modPlayer.spectralChains || modPlayer.spiritualEffect))
            {
                // Only spawn flames or attack if actually linked
                if (modPlayer.target != null)
                {
                    // First check if we have any flames to use
                    for (int i = max - 1; i >= 0; i++)
                    {
                        if (flames[i] != null)
                        {
                            selectedFlame = i;
                            break;
                        }
                    }

                    // If we do, check if we should attack
                    if (selectedFlame != -1)
                    {
                        for (int i = 0; i < Main.npc.Length; i++)
                        {
                            if (Main.npc[i].Center.Distance(Player.Center) < (modPlayer.spiritualEffect ? 9 : 7) * 16)
                            {
                                flames[selectedFlame].ai[1] = i;
                                found = true;
                                break;
                            }
                        }
                    }

                    // If we didn't find a target, or if the spectral-spiritual synergy is active, spawn a flame if below the maximum
                    // (only do this for the owner to prevent duplicate projectiles)
                    if ((!found || (modPlayer.spectralChains && modPlayer.spiritualEffect)) && Main.myPlayer == Player.whoAmI)
                    {
                        for (int i = 0; i < max; i++)
                        {
                            if (flames[i] == null)
                            {
                                Projectile.NewProjectile(Player.GetSource_ItemUse(ModContent.GetModItem(ModContent.ItemType<SpectralChains>()).Item), Player.Center,
                                    Vector2.Zero, ModContent.ProjectileType<SpectralFlame>(), modPlayer.spiritualEffect ? 80 : 30, 4, Player.whoAmI);
                            }
                        }
                    }
                }
                else
                {
                    // Despawn just one flame each second
                    for (int i = 7; i >= 0; i++)
                    {
                        if (flames[i] != null)
                        {
                            flames[i].Kill();
                            break;
                        }
                    }
                }

                // Reset the timer
                timer = modPlayer.spiritualEffect ? 30 : 60;
            }
            else
            {
                if (!modPlayer.spectralChains && !modPlayer.spiritualEffect)
                    ClearFlames();
                else
                    timer--;
            }
        }

        private void ClearFlames()
        {
            for (int i = 0; i < flames.Capacity; i++)
            {
                try
                {
                    if (flames[i].GetType() == typeof(SpectralFlame))
                        flames[i].Kill(); // Kill() will clear the corresponding array slot, so we don't have to do it here
                } catch { }
            }

            // In case the above missed a non-null non-projectile value
            flames.Clear();
        }

        public override void OnEnterWorld()
        {
            ClearFlames();
        }

        public override void OnRespawn()
        {
            ClearFlames();
        }
    }
}*/