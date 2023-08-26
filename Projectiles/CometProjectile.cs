using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using Terraria.DataStructures;
using AlarmMod.Link;

namespace AlarmMod.Projectiles
{
    public class CometProjectile : ModProjectile
    {
        private const float distance = 80f;
        private const int speed = 3;

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.light = 0.8f;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 12;
        }

        public override void OnSpawn(IEntitySource source)
        {
            try
            {
                Player owner = Main.player[Projectile.owner];
                if (owner.ownedProjectileCounts[Projectile.type] > 2)
                {
                    // Something is wrong, despawn this immediately
                    Projectile.Kill();
                }
                else if (owner.ownedProjectileCounts[Projectile.type] == 2)
                {
                    // This is the second comet, so go below the player instead
                    Projectile.Center = owner.Center + new Vector2(0, distance);
                    Projectile.ai[0] = 180;
                }
                else
                {
                    // This is the first and only comet right now, so just go above the player
                    Projectile.Center = owner.Center + new Vector2(0, distance * -1);
                    Projectile.ai[0] = 0;
                }
            }
            catch (Exception)
            {
                // Just in case something tries to spawn this other than a player
                Projectile.Kill();
                Main.NewText("comet projectile despawned, likely missing projectile owner"); // debug
            }
        }

        public override void AI()
        {
            // If the player doesn't have the set bonus, delete
            if (!Main.player[Projectile.owner].GetModPlayer<LinkPlayer>().cosmicSet)
            {
                Projectile.Kill();
                return;
            }

            // Orbit the player
            Projectile.Center = Main.player[Projectile.owner].Center + new Vector2(distance, 0).RotatedBy(MathHelper.ToRadians(Projectile.ai[0]));
            Projectile.ai[0] += speed;
            if (Projectile.ai[0] >= 360)
                Projectile.ai[0] -= 360;

            // I honestly have barely any idea what's going on here, but it handles the Flamelash visual that I'm blatantly stealing so ¯\_(ツ)_/¯
            {
                int num3 = Main.maxTilesY * 16;
                int num4 = 0;
                if (Projectile.ai[0] >= 0f)
                {
                    num4 = (int)(Projectile.ai[1] / (float)num3);
                }
                if (Projectile.type == 34)
                {
                    if (Projectile.frameCounter++ >= 4)
                    {
                        Projectile.frameCounter = 0;
                        if (++Projectile.frame >= Main.projFrames[Projectile.type])
                        {
                            Projectile.frame = 0;
                        }
                    }
                    if (Projectile.penetrate == 1 && Projectile.ai[0] >= 0f && num4 == 0)
                    {
                        Projectile.ai[1] += num3;
                        num4 = 1;
                        Projectile.netUpdate = true;
                    }
                    if (Projectile.penetrate == 1 && Projectile.ai[0] == -1f)
                    {
                        Projectile.ai[0] = -2f;
                        Projectile.netUpdate = true;
                    }
                    if (num4 > 0 || Projectile.ai[0] == -2f)
                    {
                        Projectile.localAI[0] += 1f;
                    }
                }
                float lerpValue = Utils.GetLerpValue(0f, 10f, Projectile.localAI[0], clamped: true);
                Color newColor = Color.Lerp(Color.Transparent, Color.Crimson, lerpValue);
                if (Main.rand.NextBool(6))
                {
                    Dust dust6 = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Torch, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, newColor, 3.5f);
                    dust6.noGravity = true;
                    dust6.velocity *= 1.4f;
                    dust6.velocity += Main.rand.NextVector2Circular(1f, 1f);
                    dust6.velocity += Projectile.velocity * 0.15f;
                }
                if (Main.rand.NextBool(12))
                {
                    Dust dust7 = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Torch, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, newColor, 1.5f);
                    dust7.velocity += Main.rand.NextVector2Circular(1f, 1f);
                    dust7.velocity += Projectile.velocity * 0.15f;
                }
                int num2 = Main.rand.Next(2, 5 + (int)(lerpValue * 4f));
                for (int j = 0; j < num2; j++)
                {
                    Dust dust4 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, newColor, 1.5f);
                    dust4.velocity *= 0.3f;
                    dust4.position = Projectile.Center;
                    dust4.noGravity = true;
                    dust4.velocity += Main.rand.NextVector2Circular(0.5f, 0.5f);
                    dust4.fadeIn = 2.2f;
                    dust4.position += (dust4.position - Projectile.Center) * lerpValue * 10f;
                }
            }
        }
    }
}