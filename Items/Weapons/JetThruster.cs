using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AlarmMod.Items.Weapons
{
    public class JetThruster : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.sellPrice(silver: 10);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;

            Item.damage = 45;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.knockBack = 7;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Thrust;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.useTurn = false;
        }

        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            hitbox = new Rectangle(Convert.ToInt32(player.Center.X), Convert.ToInt32(player.Center.Y), player.width, player.height);
        }

        public override bool CanUseItem(Player player)
        {
            if (base.CanUseItem(player))
            {
                Vector2 velocity = new(Main.MouseWorld.X - player.Center.X, Main.MouseWorld.Y - player.Center.Y);
                player.velocity += velocity.SafeNormalize(Vector2.UnitX) * 12;

                if (player.immuneTime < Item.useTime / 1.5)
                {
                    player.AddImmuneTime(0, Convert.ToInt32(12 - Item.useTime / 1.5));
                }

                return true;
            }
            return false;
        }

        /*public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            int randNum = Main.rand.Next(3);
            int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, randNum switch
            {
                0 => 15,dd
                1 => 57,
                _ => 58,
            }, player.direction * 2, 0f, 150, default, 1.3f);
            Main.dust[dust].velocity *= 0.2f;

            base.MeleeEffects(player, hitbox);
        }*/
    }
}
