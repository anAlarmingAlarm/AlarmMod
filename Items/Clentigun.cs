using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items
{
    public class Clentigun : ModItem
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 4));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true; // Have animation while dropped too
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Clentaminator);

            Item.width = 52;
            Item.height = 20;
            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.value = Item.sellPrice(gold: 30);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 9; i++)
            {
                float radians = MathHelper.ToRadians(i < 5 ? -90 : 90);
                Projectile.NewProjectile(source, position + (velocity.RotatedBy(radians) * 3f * (i > 4 ? i - 5: i)), velocity, type, damage, knockback, player.whoAmI);
                if (i == 4) i++;
            }

            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2f, 0f);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Clentaminator)
                .AddIngredient(ItemID.Diamond, 15)
                .AddIngredient(ItemID.Cog, 15)
                .AddIngredient(ItemID.Nanites, 10)
                .AddTile(TileID.Furnaces)
                .Register();
        }
    }
}
