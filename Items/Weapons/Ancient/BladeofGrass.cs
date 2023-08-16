using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Weapons.Ancient
{
    public class BladeofGrass : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.BladeofGrass);

            Item.width = 60;
            Item.height = 66;
            Item.damage = 28;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.knockBack = 3;
            Item.shoot = ProjectileID.None;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(4))
            {
                target.AddBuff(BuffID.Poisoned, 420);
            }
        }

        public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
        {
            if (Main.rand.NextBool(4))
            {
                target.AddBuff(BuffID.Poisoned, 420);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BladeofGrass)
                .AddTile(TileID.DemonAltar)
                .Register();

            CreateRecipe(ItemID.NightsEdge)
                .AddIngredient(ItemID.LightsBane)
                .AddIngredient(ItemID.Muramasa)
                .AddIngredient(this)
                .AddIngredient(ItemID.FieryGreatsword)
                .AddTile(TileID.DemonAltar)
                .Register();

            CreateRecipe(ItemID.NightsEdge)
                .AddIngredient(ItemID.BloodButcherer)
                .AddIngredient(ItemID.Muramasa)
                .AddIngredient(this)
                .AddIngredient(ItemID.FieryGreatsword)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
