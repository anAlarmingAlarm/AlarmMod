using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunesWeapons
{
    public class ArgentRunebladeProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 90;
            Projectile.height = 34;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 100;                  // in ticks; 60 ticks/second
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;     // allows the below to function
            Projectile.localNPCHitCooldown = -1;        // each projectile hits once and doesn't cause invuln frames (?)
        }
    }
}