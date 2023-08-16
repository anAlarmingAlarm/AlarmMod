using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Weapons
{
    public class DiscoRifle : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.PartyGirlGrenade);
            Item.useTime = 4;
            Item.useAnimation = 4;
            Item.shootSpeed = 16;
            Item.autoReuse = true;
            Item.consumable = false;
            Item.UseSound = SoundID.Item11;
            Item.noUseGraphic = false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PartyGirlGrenade, 99)
                .AddTile(TileID.DemonAltar)
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 1);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 50f;

            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            velocity = velocity.RotatedBy(MathHelper.ToRadians((Main.rand.NextFloat() - 0.5f) * 10));

            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
    }
}