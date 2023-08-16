using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AlarmMod.Projectiles
{
    public class Barrier : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.tileCollide = false;
            Projectile.alpha = 65;
            Projectile.light = 0.2f;
            Projectile.timeLeft = 30;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Projectile.rotation = MathHelper.ToRadians(-90 + 12 * Projectile.timeLeft);
            Projectile.Center = player.Center + Vector2.One.RotatedBy(Projectile.rotation) * 32f;
            Projectile.velocity *= 0;
            player.heldProj = Projectile.whoAmI;

            // Delete colliding projectiles (only do this every other frame; 30 times a second is more than enough)
            if (Projectile.timeLeft % 2 == 0)
            {
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    Projectile target = Main.projectile[i];

                    // Abort if projectile wouldn't actually hurt the player
                    // (also ensures this projectile doesn't delete itself)
                    if (!target.hostile)
                    {
                        continue;
                    }

                    // Check if the projectiles are touching
                    if (Projectile.Colliding(target.getRect(), Projectile.getRect()))
                    {
                        if (Projectile.damage >= target.damage)
                        {
                            target.Kill();
                        }
                        else
                        {
                            target.damage -= Projectile.damage;
                        }
                        
                    }
                }
            }

            /* not quite sure what's going on here but it probably involved me randomly stopping to do something else
            for (int i = 0; i < 20; i++)
            {

            }
            Dust.NewDust(Projectile.Center, 3, 3);*/
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override bool CanHitPlayer(Player target)
        {
            return false;
        }

        public override bool CanHitPvp(Player target)
        {
            return false;
        }
    }
}