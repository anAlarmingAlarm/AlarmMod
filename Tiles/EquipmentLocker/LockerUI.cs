/* commented since this is breaking item drops in general for some reason
using Microsoft.Xna.Framework;
using rail;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlarmMod.Tiles.EquipmentLocker
{
    class LockerUI : UIState {
        public override void OnInitialize()
        {
            UIPanel panel = new();
            panel.Width.Set(600, 0);
            panel.Height.Set(300, 0);
            panel.Left.Set(600, 0);
            panel.Top.Set(18, 0);
            Append(panel);

            UIText text = new("Equipment Locker");
            panel.Append(text);

            UIItemSlot hotbar1 = new(new Item[] {
                new()
            }, 0, 3);
            hotbar1.Top.Set(15, 0);
            hotbar1.Left.Set(-15, 0);
            panel.Append(hotbar1);

            UIItemSlot hotbar2 = new(new Item[] {
                new()
            }, 0, 3);
            hotbar2.Top.Set(15, 0);
            hotbar2.Left.Set(17, 0);
            panel.Append(hotbar2);

            UIItemSlot hotbar3 = new(new Item[] {
                new()
            }, 0, 3);
            hotbar3.Top.Set(15, 0);
            hotbar3.Left.Set(49, 0);
            panel.Append(hotbar3);

            UIItemSlot hotbar4 = new(new Item[] {
                new()
            }, 0, 3);
            hotbar4.Top.Set(15, 0);
            hotbar4.Left.Set(81, 0);
            panel.Append(hotbar4);

            UIItemSlot hotbar5 = new(new Item[] {
                new()
            }, 0, 3);
            hotbar5.Top.Set(15, 0);
            hotbar5.Left.Set(113, 0);
            panel.Append(hotbar5);

            UIItemSlot hotbar6 = new(new Item[] {
                new()
            }, 0, 3);
            hotbar6.Top.Set(15, 0);
            hotbar6.Left.Set(145, 0);
            panel.Append(hotbar6);

            UIItemSlot hotbar7 = new(new Item[] {
                new()
            }, 0, 3);
            hotbar7.Top.Set(15, 0);
            hotbar7.Left.Set(177, 0);
            panel.Append(hotbar7);

            UIItemSlot hotbar8 = new(new Item[] {
                new()
            }, 0, 3);
            hotbar8.Top.Set(15, 0);
            hotbar8.Left.Set(209, 0);
            panel.Append(hotbar8);

            UIItemSlot hotbar9 = new(new Item[] {
                new()
            }, 0, 3);
            hotbar9.Top.Set(15, 0);
            hotbar9.Left.Set(241, 0);
            panel.Append(hotbar9);

            UIItemSlot hotbar10 = new(new Item[] {
                new()
            }, 0, 3);
            hotbar10.Top.Set(15, 0);
            hotbar10.Left.Set(273, 0);
            panel.Append(hotbar10);
        }
    }

    class LockerSystem : ModSystem {
        internal UserInterface uInterface;
        internal LockerUI UI;
        private GameTime _lastUpdateUiGameTime;

        public static ModKeybind DebugKeybind { get; private set; }

        public override void Load()
        {
            if (!Main.dedServ)
            {
                uInterface = new UserInterface();

                UI = new LockerUI();
                UI.Activate();
            }

            DebugKeybind = KeybindLoader.RegisterKeybind(Mod, "Locker Menu Debug", ";");
        }

        public override void Unload()
        {
            // Might need a UI-side unload function here for textures or other things
            UI = null;
            DebugKeybind = null;
        }

        public override void PreUpdatePlayers()
        {
            if (DebugKeybind.JustPressed)
            {
                if (uInterface?.CurrentState == null)
                {
                    uInterface?.SetState(UI);
                }
                else
                {
                    uInterface?.SetState(null);
                }
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;
            if (uInterface?.CurrentState != null)
            {
                uInterface.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "AlarmMod: LockerInterface",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && uInterface?.CurrentState != null)
                        {
                            uInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                    InterfaceScaleType.UI));
            }
        }

        // not needed currently
        internal void ShowUI()
        {

        }

        internal void HideUI()
        {

        }
    }
}*/
