using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;

namespace AlarmMod.Projectiles
{
    public class BidenBlastProj : ModProjectile
    {
        public override void SetDefaults()
        {

            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 180;
            Projectile.light = 0.2f;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 40; i++)
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.ManaRegeneration, Projectile.velocity.X * 0.05f, Projectile.velocity.Y * 0.05f, 150, default, 1.2f);
        }
    }

    public class BidenBlastProjLarge : ModProjectile
    {
        public override string Texture => "AlarmMod/Projectiles/BidenBlastProj";

        // Variables to handle capping hit effects in a short time
        private int cap;
        private int capTimer;

        public override void SetDefaults()
        {

            Projectile.width = 58;
            Projectile.height = 58;
            Projectile.scale = 2;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 180;
            Projectile.light = 0.2f;
            Projectile.tileCollide = false;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        public override void PostAI()
        {
            if (capTimer-- <= 0)
                cap = 0;
        }

        void Hit()
        {
            if (cap < 3)
            {
                for (int i = 0; i < 30; i++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.ManaRegeneration, Projectile.velocity.X * 0.05f, Projectile.velocity.Y * 0.05f, 150, default, 1.2f);
                }
                cap++;
                capTimer = 20;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) { Hit(); }

        public override void OnHitPlayer(Player target, Player.HurtInfo info) { Hit(); }
    }
}