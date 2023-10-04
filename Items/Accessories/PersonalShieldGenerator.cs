using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Accessories
{
    public class PersonalShieldGenerator : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 20;
            Item.value = Item.buyPrice(silver: 15);
            Item.rare = ItemRarityID.White;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<PSGPlayer>().equipped = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(nameof(ItemID.GoldBar), 12)
                .AddRecipeGroup(nameof(ItemID.Sapphire), 3)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }

    public class PSGPlayer : ModPlayer
    {
        public bool equipped;
        float shield = 0;
        int timeSinceDamage = 0;
        int overheal;
        int negated;

        int ShieldMax { get => (int)(Player.statLifeMax2 * 0.08); }

        public override void ResetEffects()
        {
            if (!equipped)
            {
                shield = 0;
                timeSinceDamage = 7 * 60;
                overheal = 0;
                negated = 0;
            }

            equipped = false;
        }

        public override void PreUpdate()
        {
            if (equipped)
            {
                if (timeSinceDamage > 0)
                    timeSinceDamage--;
                else if (shield < ShieldMax)
                    shield += ShieldMax / 120f;
                else if (shield > ShieldMax)
                    shield = ShieldMax;
            }
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if (shield > 0)
            {
                if (info.Damage > shield)
                {
                    Player.statLife += (int)shield;
                    negated = (int)shield;
                    shield = 0;
                }
                else
                {
                    Player.statLife += info.Damage;
                    negated = info.Damage;
                    shield -= info.Damage;
                }
            }
            else
            {
                negated = 0;
            }
            overheal = Math.Max(0, Player.statLife - Player.statLifeMax2);
            Player.statLife = Math.Min(Player.statLife, Player.statLifeMax2);
            timeSinceDamage = 7 * 60;
        }

        public override void PostHurt(Player.HurtInfo info)
        {
            Player.statLife += overheal;
            Player.statLife = Math.Min(Player.statLife, Player.statLifeMax2);

            if (negated > 0)
            {
                foreach (CombatText text in Main.combatText)
                {
                    if (text.active) Main.NewText(text.text + " / " + info.Damage.ToString());
                    if (text.text == info.Damage.ToString() && text.active && text.position.Distance(Player.Center) < 160
                        && (text.color == CombatText.DamagedFriendly || text.color == CombatText.DamagedFriendlyCrit))
                    {
                        if (negated >= info.Damage)
                            text.active = false;
                        else
                            text.text = (info.Damage - negated).ToString();
                        break;
                    }
                }
                CombatText.NewText(Player.Hitbox, CombatText.HealMana, info.Damage);
            }
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            r -= shield / ShieldMax * 0.5f;
            g -= shield / ShieldMax * 0.4f;

            b += shield / ShieldMax * 0.4f;
        }
    }
}