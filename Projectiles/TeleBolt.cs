using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Projectiles
{
    public class TeleBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.AmethystBolt);
            AIType = ProjectileID.AmethystBolt;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Vector2 old = target.position;
            target.Teleport(Main.player[Projectile.owner].position);
            Main.player[Projectile.owner].Teleport(old);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Vector2 old = target.position;
            target.Teleport(Main.player[Projectile.owner].position);
            Main.player[Projectile.owner].Teleport(old);
        }
    }
}