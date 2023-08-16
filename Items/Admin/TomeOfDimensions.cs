/* commented out since the world this was meant for probably doesn't even exist anymore and this hardly worked in the first place
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Admin
{
    public class TomeOfDimensions : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LifeFruit);
            Item.width = 22;
            Item.height = 22;
            Item.consumable = false;
        }

        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<TeleportPlayer>().Teleport();

            return true;
        }
    }

    public class TeleportPlayer : ModPlayer
    {
        bool teleported = false;

        public void Teleport()
        {
            if (teleported)
            {
                Player.Teleport(new Vector2(Player.position.X, Player.position.Y + 5000)); //400 feet down (200 blocks)
                teleported = false;
            } else
            {
                Player.Teleport(new Vector2(Player.position.X, Player.position.Y - 5000)); //400 feet up (200 blocks)
                teleported = true;
            }
        }
    }
}*/