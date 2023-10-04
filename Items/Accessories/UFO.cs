using AlarmMod.Buffs;
using System.Linq;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlarmMod.Items.Accessories
{
    public class UFO : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 44;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Cyan;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.noFallDmg = true;
            player.GetModPlayer<UFOPlayer>().ufo = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CobaltBar, 12)
                .AddIngredient(ItemID.SoulofFlight, 15)
                .AddTile(TileID.MythrilAnvil)
                .SortBefore(Main.recipe.First(recipe => recipe.createItem.wingSlot != -1)) // Places this recipe before any wing so every wing stays together in the crafting menu.
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.PalladiumBar, 12)
                .AddIngredient(ItemID.SoulofFlight, 15)
                .AddTile(TileID.MythrilAnvil)
                .SortBefore(Main.recipe.First(recipe => recipe.createItem.wingSlot != -1)) // see above
                .Register();
        }
    }

    public class UFOPlayer : ModPlayer
    {
        public bool ufo;

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (ufo && !Player.AnyExtraJumpUsable() && UFOKeySystem.UFOKeybind.JustPressed)
            {
                Player.velocity.Y = -8;
            }
        }

        public override void ResetEffects()
        {
            ufo = false;
        }
    }
    public class UFOKeySystem : ModSystem
    {
        public static ModKeybind UFOKeybind { get; private set; }

        public override void Load()
        {
            UFOKeybind = KeybindLoader.RegisterKeybind(Mod, "UFO", "Space");
        }

        public override void Unload()
        {
            UFOKeybind = null; // not technically required but good practice
        }
    }
}