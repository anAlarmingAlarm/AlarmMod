using Terraria;
using Terraria.ModLoader;
using System;
using Terraria.DataStructures;

namespace AlarmMod.Buffs
{
    public class LifesaverBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<LifesaverPlayer>().active = true;
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            return true; // Block this buff from being reapplied
        }
    }

    public class LifesaverCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            return true; // Block this buff from being reapplied
        }
    }

    public class LifesaverPlayer : ModPlayer
    {
        public bool active;

        public override void ResetEffects()
        {
            if (active && !Player.HasBuff<LifesaverBuff>())
                Player.AddBuff(ModContent.BuffType<LifesaverCooldown>(), 3600);
            active = false;
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genDust, ref PlayerDeathReason damageSource)
        {
            if (active)
            {
                Player.statLife = Math.Min(Player.statLifeMax2, 50);
                Player.ClearBuff(ModContent.BuffType<LifesaverBuff>());
                return false;
            }
            return true;
        }
    }
}
