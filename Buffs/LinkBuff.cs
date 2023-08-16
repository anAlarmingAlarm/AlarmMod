using Terraria;
using Terraria.ModLoader;
using System;

namespace AlarmMod.Buffs
{
    public class Link : ModBuff
    {
        public override string Texture => "AlarmMod/Buffs/Link";

        public override bool RightClick(int buffIndex)
        {
            Main.LocalPlayer.AddBuff(ModContent.BuffType<Linknt>(), 2, false);
            return true;
        }
    }
    public class Linknt : ModBuff
    {
        public override string Texture => "AlarmMod/Buffs/Linknt";

        public override void SetStaticDefaults()
        {
            Main.persistentBuff[Type] = true;
        }
    }
}
