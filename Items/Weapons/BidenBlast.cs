using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AlarmMod.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using Terraria.Audio;
using ReLogic.Utilities;

namespace AlarmMod.Items.Weapons
{
    public class BidenBlast : ModItem
    {
        public static readonly float knockback = 3;
        public static readonly float shootSpeed = 22f;

        public override void SetDefaults()
        {
            Item.damage = 36;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 2;
            Item.useTime = 4;
            Item.useAnimation = 4;
            Item.knockBack = knockback;
            Item.autoReuse = true;
            Item.width = 54;
            Item.height = 54;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.rare = ItemRarityID.LightRed;
            Item.shootSpeed = shootSpeed;
            Item.value = Item.sellPrice(silver: 10);
            Item.useStyle = ItemUseStyleID.Shoot;
        }

        public override bool? UseItem(Player player)
        {
            if (player.GetModPlayer<BidenBlastPlayer>().state <= 2)
                player.GetModPlayer<BidenBlastPlayer>().state = 1;

            return base.UseItem(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SoulofLight, 25)
                .AddIngredient(ItemID.SoulofNight, 25)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }

    public class BidenBlastPlayer : ModPlayer
    {
        public int state; // 0 is not active, 1 is charging, 2 is firing, anything higher is on cooldown
        int timer;
        static readonly int cap = 100;

        SoundStyle fire = new("AlarmMod/Sounds/BidenBlast/fire");
        SoundStyle speech = new("AlarmMod/Sounds/BidenBlast/speech");
        SlotId speechSlot;

        public override void PostUpdate()
        {
            if (Main.GameUpdateCount % 5 == 0)
            {
                if (state > 2)
                {
                    if (--state == 2) state = 0;
                }
                else if ((state == 2 || timer == cap) && Player.whoAmI == Main.LocalPlayer.whoAmI)
                {
                    IEntitySource source = Player.GetSource_ItemUse(Player.HeldItem);
                    Vector2 velocity = new Vector2(BidenBlast.shootSpeed, 0).RotatedBy(Main.LocalPlayer.Center.DirectionTo(Main.MouseWorld).ToRotation());
                    Vector2 position = Player.position + new Vector2(34 / 2);
                    int type = ModContent.ProjectileType<BidenBlastProj>();
                    int damage = Player.GetWeaponDamage(Player.HeldItem) * (1 + 1 / cap);
                    float knockback = BidenBlast.knockback;
                    int owner = Player.whoAmI;
                    if (timer == cap)
                    {
                        velocity *= 1.6f;
                        position = Player.position + new Vector2(58 / 2);
                        damage *= 6;
                        knockback *= 5;
                        type = ModContent.ProjectileType<BidenBlastProjLarge>();
                    }

                    Projectile.NewProjectile(source, position, velocity, type, damage, knockback, owner);

                    state = 40;
                    timer = 0;

                    if (SoundEngine.TryGetActiveSound(speechSlot, out var activeSound)) activeSound.Stop();
                    SoundEngine.PlaySound(fire, Player.Center);
                }
                else if (state == 1)
                {
                    if (timer < cap)
                        timer++;

                    if (!SoundEngine.TryGetActiveSound(speechSlot, out var activeSound))
                    {
                        speechSlot = SoundEngine.PlaySound(speech, Player.Center);
                    }
                    else
                    {
                        activeSound.Position = Player.Center;
                    }
                }
                else
                {
                    timer = 0;
                }
            }
        }
    }
}
