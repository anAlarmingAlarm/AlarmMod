using Terraria;
using Terraria.Chat;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AlarmMod.Systems
{
    public class AFKPlayer : ModPlayer
    {
        public bool active = false;
        public int timer = -1;

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (AFKKeySystem.AFKKeybind.JustPressed && timer == -1)
            {
                timer = 180;
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Player.name + (active ? " is no longer AFK" : " is now AFK")), new Microsoft.Xna.Framework.Color(255, 240, 20));
            }
        }

        public override void PreUpdate()
        {
            if (timer > -1)
            {
                timer--;

                if (timer == 0)
                {
                    timer = -1;
                    active = !active;
                }
            }

            if (active || timer > -1)
            {
                Player.AddBuff(BuffID.Webbed, 1);

                if (active && timer == -1)
                {
                    Player.immuneTime = 5;
                    Player.immuneAlpha = 128;
                    Player.aggro = -69420;
                }
            }
        }
    }

    public class AFKKeySystem : ModSystem
    {
        public static ModKeybind AFKKeybind { get; private set; }

        public override void Load()
        {
            AFKKeybind = KeybindLoader.RegisterKeybind(Mod, "Toggle AFK Mode", ";");
        }

        public override void Unload()
        {
            AFKKeybind = null; // not technically required but good practice
        }
    }
}
