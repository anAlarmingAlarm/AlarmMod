using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Projectiles
{
    public class TwainsArcProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 600;
        }

        public override void AI()
        {
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GreenTorch, 0, 0, 0, default, 3);
            Main.dust[dust].noGravity = true;
            dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.YellowTorch, 0, 0, 0, default, 3);
            Main.dust[dust].noGravity = true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 2; i++)
            {
                int dust1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemEmerald);
                Main.dust[dust1].noGravity = true;
                int dust2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemAmber);
                Main.dust[dust2].noGravity = true;
            }
        }
    }
}