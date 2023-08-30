using AlarmMod.Link;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
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
                Projectile.ai[1] = 0;
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
        }
    }

    public class SpectralFlamePlayer : ModPlayer
    {
        public List<Projectile> flames = new();
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
            if (timer == 0 || (Player.GetModPlayer<LinkPlayer>().spectralChains && Player.GetModPlayer<LinkPlayer>().spiritualEffect))
            {
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (Main.npc[i].Center.Distance(Player.Center) < 7 * 16)
                    {
                        flames.First().ai[1] = i;
                        timer = Player.GetModPlayer<LinkPlayer>().spiritualEffect ? 30 : 60;
                        return;
                    }
                }
            }
            else
            {
                timer--;
            }
        }

        public override void OnRespawn()
        {
            flames.Clear();
            timer = 60;
        }
    }
}