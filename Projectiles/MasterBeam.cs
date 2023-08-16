using AlarmMod.Items.Weapons;
using log4net.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace AlarmMod.Projectiles
{
    public class MasterBeam : ModProjectile
    {
        public override void SetDefaults()
        {

            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 180;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.CountsAsACritter)
            {
                modifiers.FinalDamage.Flat = -1337; // this should hopefully make it deal 1 damage
                Main.player[Projectile.owner].AddBuff(ModContent.BuffType<Unworthy>(), 300);
                Main.player[Projectile.owner].Hurt(PlayerDeathReason.ByCustomReason(Main.player[Projectile.owner] + "was deemed unworthy."), 50, 0);
            }
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity, ModContent.ProjectileType<MasterHitEffect>(), 0, 0, Main.myPlayer);
        }
    }

    public class MasterHitEffect : ModProjectile
    {
        public override string Texture => "AlarmMod/Projectiles/MasterBeam";

        public override void SetDefaults()
        {

            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.tileCollide = false;
            Projectile.alpha = 125;
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
        }

        public override bool? CanDamage()
        {
            return false;
        }
    }
}