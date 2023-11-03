using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Weapons
{
    public class MarksmanRifle : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Musket);

            Item.damage = 28;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(gold: 2);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4f, 0f);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Musket)
                .AddIngredient(ItemID.IllegalGunParts)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			if (type == ProjectileID.Bullet)
				type = ProjectileID.BulletHighVelocity;
		}
    }
}
