using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AlarmMod.Systems
{
    public class AlarmBroadcast : ModPlayer
    {
        public AlarmMessage[] messages;
        public int numElements = 0;

        public override void PostUpdate()
        {
            if (messages == null)
                messages = new AlarmMessage[100];
            if (Main.myPlayer == Player.whoAmI && Main.GameUpdateCount > 5)
            {
                for (int i = 0; i < numElements; i++)
                {
                    Main.NewText(messages[i].message, messages[i].color);
                }
            }
            numElements = 0;
        }
    }

    public class AlarmMessage
    {
        public string message;
        public Color color;

        AlarmMessage(string message, Color color)
        {
            this.message = message;
            this.color = color;
        }

        public static void QueueMessage(string message, Color color)
        {
            AlarmMessage msg = new(message, color);

            foreach (Player p in Main.player)
            {
                p.GetModPlayer<AlarmBroadcast>().messages[p.GetModPlayer<AlarmBroadcast>().numElements++] = msg;
            }
        }

        public static void QueueMessage(string message, Color color, Player player)
        {
            AlarmMessage msg = new(message, color);
            player.GetModPlayer<AlarmBroadcast>().messages[player.GetModPlayer<AlarmBroadcast>().messages.Length] = msg;
        }
    }
}