using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Projectiles
{
	public class GunwhipBullet : ModProjectile
	{
		public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bullet);
            AIType = ProjectileID.Bullet;
            Projectile.DamageType = DamageClass.Summon;
        }
    }

    public class GunwhipPrimeBullet : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.BulletHighVelocity);
            AIType = ProjectileID.Bullet;
            Projectile.DamageType = DamageClass.Summon;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            if (Projectile.alpha < 140)
            {
                return new Color(255, 255, 255, 100);
            }
            return Color.Transparent;
        }
    }
}