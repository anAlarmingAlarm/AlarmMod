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
        private const float distance = 90f;
        private const int speed = 6;
        private Player owner;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 6;
        }

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
            Projectile.tileCollide = false;
        }

        public override void OnSpawn(IEntitySource source)
        {
            try
            {
                owner = Main.player[Projectile.owner];
                if (owner.ownedProjectileCounts[Projectile.type] > 1)
                {
                    // Something is wrong, despawn this immediately
                    Projectile.Kill();
                }
                else if (owner.ownedProjectileCounts[Projectile.type] == 1)
                {
                    // This is the second comet, so go below the player instead
                    Projectile.ai[0] = 180;
                }
                else
                {
                    // This is the first and only comet right now, so just go above the player
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
            if (!owner.GetModPlayer<LinkPlayer>().cosmicSet)
            {
                Projectile.Kill();
                return;
            }

            // Orbit the player
            Projectile.Center = owner.Center + new Vector2(distance, 0).RotatedBy(MathHelper.ToRadians(Projectile.ai[0])) + new Vector2(-6, 0); // offset this by some amount to fix weird X axis offset
            Projectile.ai[0] += speed;
            if (Projectile.ai[0] >= 360)
                Projectile.ai[0] -= 360;

            Projectile.rotation = Projectile.Center.DirectionTo(owner.Center).ToRotation() + MathHelper.ToRadians(180);

            // Animate projectile
            if (++Projectile.frameCounter > 30)
                Projectile.frameCounter = 0;
            if (Projectile.frameCounter % 6 == 0)
                Projectile.frame = Projectile.frameCounter / 6;
        }
    }
}