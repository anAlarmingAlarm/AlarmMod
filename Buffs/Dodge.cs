﻿using Terraria;
using Terraria.ModLoader;
using Terraria.GameInput;
using AlarmMod.Items.Accessories;

namespace AlarmMod.Buffs
{
    public class DodgeCooldown : ModBuff
    {
        public override string Texture => "AlarmMod/Buffs/DodgeCooldown";

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
    }
    public class DodgeKeySystem : ModSystem
    {
        public static ModKeybind DodgeKeybind { get; private set; }

        public override void Load()
        {
            DodgeKeybind = KeybindLoader.RegisterKeybind(Mod, "Dodge", "G");
        }

        public override void Unload()
        {
            DodgeKeybind = null; // not technically required but good practice
        }
    }

    public class DodgePlayer : ModPlayer
    {
        public bool canDodge = false;
        public bool bewitched = false;

        public override void ResetEffects()
        {
            bewitched = false;
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (canDodge && DodgeKeySystem.DodgeKeybind.JustPressed && !Player.HasBuff<DodgeCooldown>())
            {
                Player.AddBuff(ModContent.BuffType<AsgardAccessories>(), 30);
            }
        }
        public override void PostUpdateEquips()
        {
            if (bewitched)
            {
                // Let AlarmTweaks handle this if it's present
                if (ModLoader.TryGetMod("AlarmTweaks", out Mod alarmTweaks))
                {
                    alarmTweaks.Call("bewitched", Player);
                }
                else
                {
                    Player.maxMinions++;
                }
            }
        }
    }
}
