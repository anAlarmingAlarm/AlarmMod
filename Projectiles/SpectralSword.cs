using log4net.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace AlarmMod.Projectiles
{
    public class SpectralSword : ModProjectile
    {
        public override void SetDefaults()
        {

            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.alpha = 45;
            Projectile.light = 0.5f;
            Projectile.timeLeft = 120;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.myPlayer == Projectile.owner)
            {
                Vector2 position = target.Center + new Vector2(10 * 16, 0).RotatedBy(MathHelper.ToRadians(Main.rand.NextFloat() * 360));
                Vector2 velocity = Vector2.Normalize(position.DirectionTo(target.Center)) * Projectile.velocity;

                int index = -1;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (Main.npc[i] == target)
                    {
                        index = i;
                        break;
                    }
                }

                Projectile.NewProjectile(Projectile.GetSource_FromThis(), position, velocity, ModContent.ProjectileType<SpectralEcho>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, index);
                
            }
            Projectile.Kill();
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity, ModContent.ProjectileType<SpectralHitEffectMajor>(), 0, 0, Main.myPlayer);
        }
    }
    public class SpectralEcho : ModProjectile
    {
        public override string Texture => "AlarmMod/Projectiles/SpectralSword";

        public override void SetDefaults()
        {

            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.alpha = 125;
            Projectile.light = 0.3f;
            Projectile.timeLeft = 80;
        }

        public override void AI()
        {
            if (Projectile.ai[0] != -1)
            {
                if (!Main.npc[(int)Projectile.ai[0]].active || Main.npc[(int)Projectile.ai[0]].life <= 0)
                {
                    Projectile.ai[0] = -1;
                }
                else
                {
                    Projectile.velocity = Vector2.Normalize(Projectile.Center.DirectionTo(Main.npc[(int)Projectile.ai[0]].Center)) * 256;
                    Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45);
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity, ModContent.ProjectileType<SpectralHitEffectMinor>(), 0, 0, Main.myPlayer);
        }
    }
    public class SpectralHitEffectMajor : ModProjectile
    {
        public override string Texture => "AlarmMod/Projectiles/SpectralSword";

        public override void SetDefaults()
        {

            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.alpha = 45;
            Projectile.light = 0.5f;
            Projectile.timeLeft = 15;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45);
            Projectile.velocity = new(0, 0);
        }

        public override void AI()
        {
            Projectile.alpha += 12;
            Projectile.scale *= 1.05f;
            Projectile.light -= (1f / 30f);
        }

        public override bool? CanDamage()
        {
            return false;
        }
    }
    public class SpectralHitEffectMinor : ModProjectile
    {
        public override string Texture => "AlarmMod/Projectiles/SpectralSword";

        public override void SetDefaults()
        {

            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.alpha = 125;
            Projectile.light = 0.3f;
            Projectile.timeLeft = 15;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45);
            Projectile.velocity = new(0, 0);
        }

        public override void AI()
        {
            Projectile.alpha += (130 / 15);
            Projectile.scale *= 1.05f;
            Projectile.light -= 0.02f;
        }

        public override bool? CanDamage()
        {
            return false;
        }
    }
}