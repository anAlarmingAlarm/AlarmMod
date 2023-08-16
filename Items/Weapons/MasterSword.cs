using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using AlarmMod.Projectiles;

namespace AlarmMod.Items.Weapons
{
    public class MasterSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 54;
            Item.scale = 1.2f;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;

            Item.damage = 135;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.knockBack = 6;
            Item.shoot = ModContent.ProjectileType<MasterBeam>();
            Item.shootSpeed = 22;
        }

        public override bool CanShoot(Player player)
        {
            if (player.statLife < player.statLifeMax2 * 0.9 || player.HasBuff<Unworthy>())
            {
                return false;
            }
            else
            {
                return base.CanShoot(player);
            }

        }

        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.CountsAsACritter)
            {
                modifiers.FinalDamage.Flat = -1337; // this should hopefully make it deal 1 damage
                player.AddBuff(ModContent.BuffType<Unworthy>(), 300);
                player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " was deemed unworthy."), 50, 0);
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            int randNum = Main.rand.Next(3);
            int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, randNum switch
            {
                0 => 15,
                1 => 57,
                _ => 58,
            }, player.direction * 2, 0f, 150, default, 1.3f);
            Main.dust[dust].velocity *= 0.2f;

            base.MeleeEffects(player, hitbox);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.EnchantedSword)
                .AddIngredient(ItemID.BrokenHeroSword)
                .AddIngredient(ItemID.HallowedBar, 50)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }

    public class Unworthy : ModBuff
    {
        public override string Texture => "AlarmMod/Buffs/Unworthy";

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
    }
}
