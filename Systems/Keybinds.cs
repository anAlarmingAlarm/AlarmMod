using Terraria;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AlarmMod.Systems
{
    public class Keybinds : ModSystem
    {
        public static ModKeybind AmmoSwitchKeybind { get; private set; }
        //public static ModKeybind DefenderBuffToggle { get; private set; }

        public override void Load()
        {
            AmmoSwitchKeybind = KeybindLoader.RegisterKeybind(Mod, "Cycle Ammo Slots", "K");
            //DefenderBuffToggle = KeybindLoader.RegisterKeybind(Mod, "Toggle Defender's Forge Autobuff", ";");
        }

        public override void Unload()
        {
            AmmoSwitchKeybind = null;
            //DefenderBuffToggle = null;
        }
    }

    public class KeybindPlayer : ModPlayer
    {
        //bool toggleDefender = false;

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (Keybinds.AmmoSwitchKeybind.JustPressed)
            {
                // This is really difficult to do iteratively so I'm just going to brute force it
                Item selected = Player.HeldItem;
                Item temp;
                {
                    if (selected.useAmmo == AmmoID.None || (Player.inventory[54].ammo != selected.useAmmo && Player.inventory[55].ammo != selected.useAmmo && Player.inventory[56].ammo != selected.useAmmo && Player.inventory[57].ammo != selected.useAmmo) || (Player.inventory[54].ammo == selected.useAmmo && Player.inventory[55].ammo != selected.useAmmo && Player.inventory[56].ammo != selected.useAmmo && Player.inventory[57].ammo != selected.useAmmo) || (Player.inventory[54].ammo != selected.useAmmo && Player.inventory[55].ammo == selected.useAmmo && Player.inventory[56].ammo != selected.useAmmo && Player.inventory[57].ammo != selected.useAmmo) || (Player.inventory[54].ammo != selected.useAmmo && Player.inventory[55].ammo != selected.useAmmo && Player.inventory[56].ammo == selected.useAmmo && Player.inventory[57].ammo != selected.useAmmo) || (Player.inventory[54].ammo != selected.useAmmo && Player.inventory[55].ammo != selected.useAmmo && Player.inventory[56].ammo != selected.useAmmo && Player.inventory[57].ammo == selected.useAmmo))
                    {
                        // 0-1 ammo slots compatible or weapon does not use ammo: nothing to swap
                        return;
                    }
                    else if (Player.inventory[54].ammo == selected.useAmmo && Player.inventory[55].ammo == selected.useAmmo && Player.inventory[56].ammo != selected.useAmmo && Player.inventory[57].ammo != selected.useAmmo)
                    {
                        temp = Player.inventory[55];
                        Player.inventory[55] = Player.inventory[54];
                        Player.inventory[54] = temp;
                    }
                    else if (Player.inventory[54].ammo == selected.useAmmo && Player.inventory[55].ammo != selected.useAmmo && Player.inventory[56].ammo == selected.useAmmo && Player.inventory[57].ammo != selected.useAmmo)
                    {
                        temp = Player.inventory[54];
                        Player.inventory[54] = Player.inventory[56];
                        Player.inventory[56] = temp;
                    }
                    else if (Player.inventory[54].ammo != selected.useAmmo && Player.inventory[55].ammo == selected.useAmmo && Player.inventory[56].ammo == selected.useAmmo && Player.inventory[57].ammo != selected.useAmmo)
                    {
                        temp = Player.inventory[55];
                        Player.inventory[55] = Player.inventory[56];
                        Player.inventory[56] = temp;
                    }
                    else if (Player.inventory[54].ammo == selected.useAmmo && Player.inventory[55].ammo != selected.useAmmo && Player.inventory[56].ammo != selected.useAmmo && Player.inventory[57].ammo == selected.useAmmo)
                    {
                        temp = Player.inventory[57];
                        Player.inventory[57] = Player.inventory[54];
                        Player.inventory[54] = temp;
                    }
                    else if (Player.inventory[54].ammo != selected.useAmmo && Player.inventory[55].ammo == selected.useAmmo && Player.inventory[56].ammo != selected.useAmmo && Player.inventory[57].ammo == selected.useAmmo)
                    {
                        temp = Player.inventory[57];
                        Player.inventory[57] = Player.inventory[55];
                        Player.inventory[55] = temp;
                    }
                    else if (Player.inventory[54].ammo != selected.useAmmo && Player.inventory[55].ammo != selected.useAmmo && Player.inventory[56].ammo == selected.useAmmo && Player.inventory[57].ammo == selected.useAmmo)
                    {
                        temp = Player.inventory[57];
                        Player.inventory[57] = Player.inventory[56];
                        Player.inventory[56] = temp;
                    }
                    else if (Player.inventory[54].ammo == selected.useAmmo && Player.inventory[55].ammo == selected.useAmmo && Player.inventory[56].ammo == selected.useAmmo && Player.inventory[57].ammo != selected.useAmmo)
                    {
                        temp = Player.inventory[56];
                        Player.inventory[56] = Player.inventory[55];
                        Player.inventory[55] = Player.inventory[54];
                        Player.inventory[54] = temp;
                    }
                    else if (Player.inventory[54].ammo == selected.useAmmo && Player.inventory[55].ammo == selected.useAmmo && Player.inventory[56].ammo != selected.useAmmo && Player.inventory[57].ammo == selected.useAmmo)
                    {
                        temp = Player.inventory[57];
                        Player.inventory[57] = Player.inventory[55];
                        Player.inventory[55] = Player.inventory[54];
                        Player.inventory[54] = temp;
                    }
                    else if (Player.inventory[54].ammo == selected.useAmmo && Player.inventory[55].ammo != selected.useAmmo && Player.inventory[56].ammo == selected.useAmmo && Player.inventory[57].ammo == selected.useAmmo)
                    {
                        temp = Player.inventory[57];
                        Player.inventory[57] = Player.inventory[56];
                        Player.inventory[56] = Player.inventory[54];
                        Player.inventory[54] = temp;
                    }
                    else if (Player.inventory[54].ammo != selected.useAmmo && Player.inventory[55].ammo == selected.useAmmo && Player.inventory[56].ammo == selected.useAmmo && Player.inventory[57].ammo == selected.useAmmo)
                    {
                        temp = Player.inventory[57];
                        Player.inventory[57] = Player.inventory[56];
                        Player.inventory[56] = Player.inventory[55];
                        Player.inventory[55] = temp;
                    }
                    else if (Player.inventory[54].ammo == selected.useAmmo && Player.inventory[55].ammo == selected.useAmmo && Player.inventory[56].ammo == selected.useAmmo && Player.inventory[57].ammo == selected.useAmmo)
                    {
                        temp = Player.inventory[57];
                        Player.inventory[57] = Player.inventory[56];
                        Player.inventory[56] = Player.inventory[55];
                        Player.inventory[55] = Player.inventory[54];
                        Player.inventory[54] = temp;
                    }
                    // If it somehow misses all of these conditions something is wrong, though there's not really a point in aborting at this point
                    else
                    {
                        Main.NewText("All conditions missed");
                    }
                }
            }
            /*if (Keybinds.DefenderBuffToggle.JustPressed)
            {
                if (toggleDefender)
                    toggleDefender = false;
                else
                    toggleDefender = true;
            }*/
        }

        public override void PreUpdateBuffs()
        {
            /*if (toggleDefender)
            {
                Item curItem;
                for (int i = 0; i < 10; i++) //only check first 10 slots of Defender's Forge for "balance" and to prevent the accursed buff cap glitches
                {
                    curItem = Player.bank3.item[i];
                    if (curItem.buffType != 0)
                    {
                        if (!Player.HasBuff(curItem.buffType))
                        {
                            Player.AddBuff(curItem.buffType, curItem.buffTime);
                            if (curItem.consumable && curItem.stack != curItem.maxStack)
                                if (curItem.stack > 1)
                                    Player.bank3.item[i].stack--;
                                else
                                    Player.bank3.item[i] = null;
                        }
                    }
                    else if (curItem.type == ItemID.AmmoBox)
                    {
                        Player.AddBuff(BuffID.AmmoBox, 2);
                    }
                    else if (curItem.type == ItemID.BewitchingTable)
                    {
                        Player.AddBuff(BuffID.Bewitched, 2);
                    }
                    else if (curItem.type == ItemID.CrystalBall)
                    {
                        Player.AddBuff(BuffID.Clairvoyance, 2);
                    }
                    else if (curItem.type == ItemID.SharpeningStation)
                    {
                        Player.AddBuff(BuffID.Sharpened, 2);
                    }
                    else if (curItem.type == ItemID.SliceOfCake)
                    {
                        Player.AddBuff(BuffID.SugarRush, 2);
                    }
                    else if (curItem.type == ItemID.CatBast)
                    {
                        Player.AddBuff(BuffID.CatBast, 2);
                    }
                    else if (curItem.type == ItemID.Campfire || curItem.type == ItemID.Fireplace)
                    {
                        Player.AddBuff(BuffID.Campfire, 2);
                    }
                    else if (curItem.type == ItemID.HeartLantern)
                    {
                        Player.AddBuff(BuffID.HeartLamp, 2);
                    }
                    else if (curItem.type == ItemID.StarinaBottle)
                    {
                        Player.AddBuff(BuffID.StarInBottle, 2);
                    }
                    else if (curItem.type == ItemID.Sunflower)
                    {
                        Player.AddBuff(BuffID.Sunflower, 2);
                    }
                    else if (curItem.type == ItemID.PeaceCandle)
                    {
                        Player.AddBuff(BuffID.PeaceCandle, 2);
                    }
                    else if (curItem.type == ItemID.WaterCandle)
                    {
                        Player.AddBuff(BuffID.WaterCandle, 2);
                    }
                    else if (curItem.type == ItemID.HoneyBucket || curItem.type == ItemID.BottledHoney)
                    {
                        Player.AddBuff(BuffID.Honey, 2);
                    }
                }
            }*/
        }
    }

    //Change this item's tooltip to hopefully make the change less obscure
    /*public class DefenderForge : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstatiation)
        {
            return item.type == ItemID.DefendersForge;
        }

        public override void SetDefaults(Item item)
        {
            item.StatsModifiedBy.Add(Mod); // Notify the game that we've made a functional change to this item.
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Find(line => line.Name == "Tooltip1").Text += "\nBuff potions and furniture in the first row are used automatically from anywhere" +
                "\nThis can be toggled with a keybind (toggled off by default)\nFurniture and full stacks of buff potions grant buffs but are not consumed";
        }
    }*/
}
