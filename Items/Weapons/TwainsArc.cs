using AlarmMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Weapons
{
    public class TwainsArc : ModItem
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true; // Have animation while dropped too
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 90;
            Item.scale = 0.75f;
            Item.rare = ItemRarityID.Pink;


            Item.DamageType = DamageClass.Ranged;
            Item.damage = 400;
            Item.knockBack = 5f;
            Item.noMelee = true;
            Item.useTime = 80;
            Item.useAnimation = 80;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<TwainsArcProjectile>();
            Item.shootSpeed = 10f;

            Item.UseSound = new SoundStyle($"{nameof(AlarmMod)}/Sounds/TwainsArc");
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ChlorophyteBar, 15)
                .AddIngredient(ItemID.FallenStar, 15)
                .AddTile(ItemID.MythrilAnvil)
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2f, 0f);
        }
    }
}