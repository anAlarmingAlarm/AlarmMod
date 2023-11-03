using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AlarmMod.Items.Weapons.Slate
{
    public class Slate : ModItem
    {
        public enum Biome
        {
            Forest,
            Snow,
            Desert,
            Jungle,
            Ocean,
            Underground, // including Cavern
            Mushroom,
            Space,
            Corruption,
            Crimson,
            Hallow,
            Dungeon,
            Underworld,
            Marble,
            Granite,
            Graveyard,
            Meteorite,
            Town,
            Aether
        }

        const int shotIncrement = 5;
        const int shotsNeeded = 500 * 6 * shotIncrement; // Bursts needed * 6 * shotIncrement

        int[] biomeCount = new int[19];
        int highestBiome = -1;
        int shot = 0;

        public void TrackShot(Player player)
        {
            // Find the biome the player is in
            int biome = -1;
            {
                if (player.ZoneGraveyard)
                {
                    biome = (int)Biome.Graveyard;
                }
                else if (player.townNPCs > 2f)
                {
                    biome = (int)Biome.Town;
                }
                else if (player.ZoneMeteor)
                {
                    biome = (int)Biome.Meteorite;
                }
                else if (player.ZoneShimmer)
                {
                    biome = (int)Biome.Aether;
                }
                else if (player.ZoneDungeon)
                {
                    biome = (int)Biome.Dungeon;
                }
                else if (player.ZoneCorrupt)
                {
                    biome = (int)Biome.Graveyard;
                }
                else if (player.ZoneSkyHeight)
                {
                    biome = (int)Biome.Space;
                }
                else if (player.ZoneCrimson)
                {
                    biome = (int)Biome.Crimson;
                }
                else if (player.ZoneHallow)
                {
                    biome = (int)Biome.Hallow;
                }
                else if (player.ZoneGlowshroom)
                {
                    biome = (int)Biome.Mushroom;
                }
                else if (player.ZoneMarble)
                {
                    biome = (int)Biome.Marble;
                }
                else if (player.ZoneGranite)
                {
                    biome = (int)Biome.Granite;
                }
                else if (player.ZoneSnow)
                {
                    biome = (int)Biome.Snow;
                }
                else if (player.ZoneDesert)
                {
                    biome = (int)Biome.Desert;
                }
                else if (player.ZoneJungle)
                {
                    biome = (int)Biome.Jungle;
                }
                else if (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight)
                {
                    biome = (int)Biome.Underground;
                }
                else if (player.ZoneUnderworldHeight)
                {
                    biome = (int)Biome.Underworld;
                }
                else if (player.ZoneBeach)
                {
                    biome = (int)Biome.Ocean;
                }
                else if (player.ZoneForest)
                {
                    biome = (int)Biome.Forest;
                }
            }

            // If the player is in a supported biome, alter counts
            if (biome > -1)
            {
                for (int i = 0; i < biomeCount.Length; i++)
                {
                    if (i == biome)
                    {
                        biomeCount[i] += shotIncrement;

                        if (biomeCount[i] > shotsNeeded)
                        {
                            Evolve(player, i);
                            return;
                        }
                        if (biomeCount[i] > biomeCount[highestBiome])
                        {
                            highestBiome = i;
                        }
                    }
                    else
                    {
                        biomeCount[i]--;
                    }
                }
            }
        }

        public void Evolve(Player player, int biome)
        {
            if (player.HeldItem.type == Type)
            {
                int prefix = player.HeldItem.prefix;
                int item = -6969;

                // Get the corresponding item
                {
                    if (biome == (int)Biome.Forest)
                    {
                        item = ModContent.ItemType<Slate>();
                    }
                    if (biome == (int)Biome.Snow)
                    {
                        item = ModContent.ItemType<Slate>();
                    }
                    if (biome == (int)Biome.Desert)
                    {
                        item = ModContent.ItemType<Slate>();
                    }
                    if (biome == (int)Biome.Jungle)
                    {
                        item = ModContent.ItemType<Slate>();
                    }
                    if (biome == (int)Biome.Ocean)
                    {
                        item = ModContent.ItemType<Slate>();
                    }
                    if (biome == (int)Biome.Underground)
                    {
                        item = ModContent.ItemType<Slate>();
                    }
                    if (biome == (int)Biome.Mushroom)
                    {
                        item = ModContent.ItemType<Slate>();
                    }
                    if (biome == (int)Biome.Space)
                    {
                        item = ModContent.ItemType<Slate>();
                    }
                    if (biome == (int)Biome.Corruption)
                    {
                        item = ModContent.ItemType<Slate>();
                    }
                    if (biome == (int)Biome.Crimson)
                    {
                        item = ModContent.ItemType<Slate>();
                    }
                    if (biome == (int)Biome.Hallow)
                    {
                        item = ModContent.ItemType<Slate>();
                    }
                    if (biome == (int)Biome.Dungeon)
                    {
                        item = ModContent.ItemType<Slate>();
                    }
                    if (biome == (int)Biome.Underworld)
                    {
                        item = ModContent.ItemType<Slate>();
                    }
                    if (biome == (int)Biome.Marble)
                    {
                        item = ModContent.ItemType<Slate>();
                    }
                    if (biome == (int)Biome.Granite)
                    {
                        item = ModContent.ItemType<Slate>();
                    }
                    if (biome == (int)Biome.Graveyard)
                    {
                        item = ModContent.ItemType<Slate>();
                    }
                    if (biome == (int)Biome.Meteorite)
                    {
                        item = ModContent.ItemType<Slate>();
                    }
                    if (biome == (int)Biome.Town)
                    {
                        item = ModContent.ItemType<Slate>();
                    }
                    if (biome == (int)Biome.Aether)
                    {
                        item = ModContent.ItemType<Slate>();
                    }
                }

                if (item > -69)
                {
                    player.HeldItem.TurnToAir();
                    Item spawned = player.QuickSpawnItemDirect(null, ItemID.ChainGun);
                    spawned.prefix = prefix;
                }
            }
        }

        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 76;
            Item.height = 28;
            Item.rare = ItemRarityID.LightRed;

            // Use Properties
            Item.useTime = 4;
            Item.useAnimation = 24;
            Item.reuseDelay = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;

            // Weapon Properties
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 20;
            Item.knockBack = 1f;
            Item.noMelee = true;

            // Gun Properties
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 8f;
            Item.useAmmo = AmmoID.Bullet;
            Item.consumeAmmoOnLastShotOnly = true;
        }

        public override void AddRecipes()
        {
            /*CreateRecipe()
                .AddIngredient()
                .AddTile()
                .Register();*/
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2f, -2f);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			Vector2 muzzleOffset = Vector2.Normalize(velocity) * 35f - Vector2.Normalize(velocity).RotatedBy(90) * (((velocity.X < 0 ? -2.5f : 0.5f) + shot % 3) * 5f);

			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0)) {
				position += muzzleOffset;
			}

            SoundEngine.PlaySound(SoundID.Item11, player.Center);
            if (shot++ == 5)
            {
                shot = 0;
            }
            TrackShot(player);
		}

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            drawColor = Color.Red;
            return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }
    }
}