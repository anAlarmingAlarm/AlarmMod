using AlarmMod.Buffs;
using AlarmMod.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AlarmMod.Link
{
    public class LinkPlayer : ModPlayer
    {
        // Values inherent to linking

        ///<summary> The player this player is linked to; null if not linked </summary>
        public Player target;

        ///<summary> The range of this player's link, in tiles; -69 if not holding a Link item </summary>
        public float linkRange;



        // Common bonuses between links

        ///<summary> The damage bonus the link target receives, not counting the base +75% for linking, in percent (i.e. 3 = +78% crit chance) </summary>
        public float linkDamage;

        ///<summary> The critical chance bonus the link target receives, in percent (i.e. 3 = +3% crit chance) </summary>
        public float linkCrit;

        ///<summary> The armor penetration bonus the link target receives </summary>
        public float linkArmorPen;

        ///<summary> The defense bonus the link target receives </summary>
        public int linkDefense;

        ///<summary> The damage reduction bonus the link target receives </summary>
        public float linkEndurance;

        ///<summary> The movement speed bonus the link target receives, in percent (i.e. 5 = +5% move speed) </summary>
        public float linkMoveSpeed;

        ///<summary> How much life the link target regenerates per second </summary>
        public int linkRegen;



        // Specific link effects

        ///<summary> Cosmic Armor set effect (orbiting comets) </summary>
        public bool cosmicSet;

        ///<summary> Afflicted Links effect (reduced damage with cooldown); -1 if disabled, 0 if active, above 0 if on cooldown </summary>
        public int afflictedLinks;

        ///<summary> Gored Links effect (bonus damage when rapidly attacking) </summary>
        public bool goredLinks;

        ///<summary> Hits to track for the goredLinks effect </summary>
        public int[] goredLinksHits;

        ///<summary> Cufflinks effect (bonus armor penetration, but halved if the target has more than 0 already) </summary>
        public bool cufflinks;

        ///<summary> Spectral Chains effect (summon flames when no enemies nearby, target nearby enemies, disable if spiritualEffect) </summary>
        public bool spectralChains;

        ///<summary> 1 for Hallowed Links effect (bonus link damage and Paladin's Shield effect while you have high hp),
        ///2 for Refractive Links effect (stronger version of previous), 0 for neither effect </summary>
        public int saviorEffect;

        ///<summary> Elegy Emblem effect, not including damage (increased stats if link target dies) </summary>
        public bool elegyEmblem;

        ///<summary> Clockwork Brace effect (clock mode) </summary>
        public bool clockworkBrace;

        ///<summary> Spiritual Armor set effect (summon flames when no enemies nearby, target nearby enemies, synergy if spectralChains) </summary>
        public bool spiritualEffect;

        ///<summary> Prismatic Links effect (enemies within half of linkRange have *0.65 defense) </summary>
        public bool pristmaticLinks;

        ///<summary> Uplinks effect (teleport out of the way of fatal damage and heal) </summary>
        public bool uplinks;

        ///<summary> Whirlwind Armor set effect (press key to switch places and create two explosions) </summary>
        public bool whirlwindSet;

        ///<summary> Lens Arcana effect (attacks while linked mark enemies for bonus damage from link target) </summary>
        public bool lensArcana;

        ///<summary> Fractures effect, not counting damage (all attacks from link target are duplicated) </summary>
        public bool fractures;

        ///<summary> TBBLOBNOERN effect (whirlwindSet cooldown is halved) </summary>
        public bool tbblobnoern;

        public override void PreUpdate()
        {
            foreach (Player p in Main.player)
            {
                if (p.TryGetModPlayer(out LinkPlayer lp))
                {
                    if (lp.target == Player)
                    {
                        if (!p.active || p.statLife <= 0 || p.Center.Distance(Player.Center) < linkRange * 16 || Player.HasBuff<Linknt>())
                        {
                            // Remove link from both sides if the linking player is dead, too far, or their target cancelled the link
                            lp.target = null;
                        }
                        else
                        {
                            // Otherwise apply effects
                            Player.GetDamage(DamageClass.Generic) += (lp.linkDamage + 75) / 100f;
                            Player.GetCritChance(DamageClass.Generic) += lp.linkCrit / 100f;
                            Player.GetArmorPenetration(DamageClass.Generic) += lp.linkArmorPen;
                            Player.statDefense += lp.linkDefense;
                            Player.endurance += lp.linkEndurance;
                            Player.moveSpeed += lp.linkMoveSpeed;
                            Player.lifeRegen += lp.linkRegen * 2;

                            // Cufflinks effect
                            if (Player.GetArmorPenetration(DamageClass.Generic) > lp.linkArmorPen)
                            {
                                Player.GetArmorPenetration(DamageClass.Generic) += 5;
                            }
                            else
                            {
                                Player.GetArmorPenetration(DamageClass.Generic) += 10;
                            }
                        }
                    }
                }
            }

            // If this person has a link target and is dead or inactive, remove that target
            if (Player.statLife <= 0 || !Player.active)
            {
                target = null;
            }

            // Make Linknt permanent
            if (Player.HasBuff<Linknt>())
            {
                Player.AddBuff(ModContent.BuffType<Linknt>(), 2);
            }

            // Cosmic Set
            if (cosmicSet && Player.ownedProjectileCounts[ModContent.ProjectileType<CometProjectile>()] < 2 && Main.myPlayer == Player.whoAmI)
            {
                Projectile.NewProjectile(Player.GetSource_FromThis("SetBonus_CosmicSet"), Player.Center, Vector2.Zero, ModContent.ProjectileType<CometProjectile>(), 12, 0, Main.myPlayer);
            }
        }

        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            foreach (Player p in Main.player)
            {
                if (p.TryGetModPlayer(out LinkPlayer lp))
                {
                    // Duplicate attacks if this player is the link target of a player using Fractures
                    if (lp.target == Player && lp.fractures)
                    {
                        base.Shoot(item, new EntitySource_ItemUse_WithAmmo(p, item, source.AmmoItemIdUsed), p.position, velocity, type, damage, knockback);
                    }
                }
            }
            return base.Shoot(item, source, position, velocity, type, damage, knockback);
        }
        
        ///<summary> Common on-hit code to handle Afflicted Links' reduced damage taken effect </summary>
        private void AfflictedLinkModifier(ref Player.HurtModifiers modifiers)
        {
            if (afflictedLinks == 0)
            {
                modifiers.IncomingDamageMultiplier *= 0.85f;
                afflictedLinks = 180;
            }
            else
            {
                foreach (Player p in Main.player)
                {
                    if (p.TryGetModPlayer(out LinkPlayer lp))
                    {
                        if (lp.target == Player && lp.afflictedLinks == 0)
                        {
                            modifiers.IncomingDamageMultiplier *= 0.85f;
                            lp.afflictedLinks = 180;
                        }
                    }
                }
            }
        }
        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            AfflictedLinkModifier(ref modifiers);
            base.ModifyHitByNPC(npc, ref modifiers);
        }
        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            AfflictedLinkModifier(ref modifiers);
            base.ModifyHitByProjectile(proj, ref modifiers);
        }

        // Gored Links effects
        public override void OnHitAnything(float x, float y, Entity victim)
        {
            if (goredLinks)
            {
                goredLinksHits[2] = goredLinksHits[1];
                goredLinksHits[1] = goredLinksHits[0];
                goredLinksHits[0] = 0;
            }
            else
            {
                foreach (Player p in Main.player)
                {
                    if (p.TryGetModPlayer(out LinkPlayer lp))
                    {
                        if (lp.target == Player && lp.goredLinks)
                        {
                            lp.goredLinksHits[2] = lp.goredLinksHits[1];
                            lp.goredLinksHits[1] = lp.goredLinksHits[0];
                            lp.goredLinksHits[0] = 0;
                        }
                    }
                }
            }
            base.OnHitAnything(x, y, victim);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            foreach (Player p in Main.player)
            {
                if (p.TryGetModPlayer(out LinkPlayer lp))
                {
                    if (lp.target == Player && lp.goredLinks && lp.goredLinksHits[2] - 60 <= lp.goredLinksHits[0] && lp.goredLinksHits[0] <= 60)
                    {
                        modifiers.FlatBonusDamage += 3;
                    }
                }
            }
            base.ModifyHitNPC(target, ref modifiers);
        }

        public override void ResetEffects()
        {
            linkRange = -69;
            linkDamage = 0;
            linkCrit = 0;
            linkDefense = 0;
            linkArmorPen = 0;
            linkEndurance = 0;
            linkMoveSpeed = 0;
            linkRegen = 0;
            cosmicSet = false;
            afflictedLinks = -1;
            if (!goredLinks)
            {
                goredLinksHits = Array.Empty<int>();
            }
            else
            {
                goredLinksHits[0]++;
                goredLinksHits[1]++;
                goredLinksHits[2]++;
            }
            goredLinks = false;
            cufflinks = false;
            spectralChains = false;
            saviorEffect = 0;
            elegyEmblem = false;
            clockworkBrace = false;
            spiritualEffect = false;
            pristmaticLinks = false;
            uplinks = false;
            whirlwindSet = false;
            fractures = false;
            tbblobnoern = false;
        }

        ///<summary> Attempt to link this player to another player, returning true if successful or false if it fails </summary>
        public bool AttemptLink()
        {
            foreach (Player p in Main.player)
            {
                // Player must be alive, within link range, not currently blocking links, and not linked to this person
                if (p.active && p.statLife > 0 && p.Center.Distance(Player.Center) < linkRange * 16 && !p.HasBuff<Linknt>() && p.GetModPlayer<LinkPlayer>().target != Player)
                {
                    // If pvp is enabled, they also can't be on opposing teams
                    if (!Player.hostile || !Player.InOpposingTeam(p))
                    {
                        Rectangle targetRect = p.getRect();
                        targetRect.Offset(Player.position.ToPoint());
                        Rectangle cursorRect = new((int)(Main.MouseScreen.X - 24), (int)(Main.MouseScreen.Y - 24), 48, 48);
                        if (targetRect.Intersects(cursorRect))
                        {
                            target = p;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        ///<summary> Draw dust for the link range and the link, if active </summary>
        public void DrawLink(int dust, Color color = default) //
        {
            if (linkRange > 0) // This should never be false, but just in case
            {
                Vector2 point = new Vector2(Player.Center.X + linkRange * 16, Player.Center.Y);
                float deg = 360 / (8 * linkRange);
                for (int i = 0; i < 8 * linkRange; i++)
                {
                    Dust.NewDust(point, 1, 1, dust, 0, 0, 50, color);
                    point = point.RotatedBy(MathHelper.ToRadians(deg), Player.Center);
                }
            }
            if (target != null)
            {
                Vector2 point = new(Player.Center.X, Player.Center.Y);
                Vector2 end = new(target.Center.X, target.Center.Y);
                while (point != end)
                {
                    Dust.NewDust(point, 1, 1, dust, 0, 0, 10, color);
                    point.MoveTowards(end, 16);
                }
            }
        }
    }
}
