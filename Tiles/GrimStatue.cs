﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AlarmMod.Tiles
{
    public class GrimStatue : ModTile
    {
        public override void SetStaticDefaults()
        {
            // Properties
            Main.tileNoAttach[Type] = true;
            Main.tileFrameImportant[Type] = true;

            DustType = DustID.Stone;
            AdjTiles = new int[] { TileID.Statues };

            // Placement
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateHeights = new[] { 18, 18, 18 };
            TileObjectData.addTile(Type);

            // Etc
            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(200, 200, 200), name);
        }

        public override void NumDust(int x, int y, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        
        public override void KillMultiTile(int x, int y, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(x, y), x * 16, y * 16, 32, 16, ModContent.ItemType<Items.Placeables.GrimStatue>());
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            Player player = Main.LocalPlayer;

            //if (closer)
            //{
                //player.ZoneGraveyard = true;
                player.AddBuff(BuffID.Swiftness, 2);
                Main.SceneMetrics.GraveyardTileCount = 100;
            //}
        }
    }
}
