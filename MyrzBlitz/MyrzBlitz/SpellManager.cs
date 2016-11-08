using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using SharpDX;

namespace MyrzBlitz
{
    public static class SpellManager
    {
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Active W { get; private set; }
        public static Spell.Active E { get; private set; }
        public static Spell.Active R { get; private set; }

        public static Spell.Targeted Smite { get; private set; }

        public static Spell.SpellBase[] Spells { get; private set; }
        public static Dictionary<SpellSlot, Color> ColorTranslation { get; private set; }

        public static void Initialize()
        {
            // Initialize spells
            Q = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Linear, 250, 1800, 70);
            W = new Spell.Active(SpellSlot.W, 600);
            E = new Spell.Active(SpellSlot.E, 250);
            R = new Spell.Active(SpellSlot.R, 600);

            SetSmite();

            // Finetune spells
            Q.AllowedCollisionCount = 0;

            Spells = (new Spell.SpellBase[] { Q, W, E, R }).OrderByDescending(o => o.Range).ToArray();
            ColorTranslation = new Dictionary<SpellSlot, Color>
            {
                { SpellSlot.Q, Color.IndianRed.ToArgb(150) },
                { SpellSlot.R, Color.DarkRed.ToArgb(150) }
            };
        }

        private static void SetSmite()
        {
            int[] smiteRed = { 3715, 1415, 1414, 1413, 1412 };
            int[] smiteBlue = { 3706, 1403, 1402, 1401, 1400 };

            SpellSlot smiteSlot;
            if (smiteBlue.Any(x => ObjectManager.Player.InventoryItems.FirstOrDefault(a => a.Id == (ItemId)x) != null))
                smiteSlot = ObjectManager.Player.GetSpellSlotFromName("s5_summonersmiteplayerganker");
            else if (smiteRed.Any(x => ObjectManager.Player.InventoryItems.FirstOrDefault(a => a.Id == (ItemId)x) != null))
                smiteSlot = ObjectManager.Player.GetSpellSlotFromName("s5_summonersmiteduel");
            else
                smiteSlot = ObjectManager.Player.GetSpellSlotFromName("summonersmite");
            Smite = new Spell.Targeted(smiteSlot, 500);
        }


        public static bool IsEnabled(this Spell.SpellBase spell, Orbwalker.ActiveModes mode)
        {
            switch (mode)
            {
                case Orbwalker.ActiveModes.Combo:
                    switch (spell.Slot)
                    {
                        case SpellSlot.Q:
                            return Config.Modes.Combo.UseQ;
                        case SpellSlot.W:
                            return Config.Modes.Combo.UseW;
                        case SpellSlot.E:
                            return Config.Modes.Combo.UseE;
                        case SpellSlot.R:
                            return Config.Modes.Combo.UseR;
                    }
                    break;
                case Orbwalker.ActiveModes.Harass:
                    switch (spell.Slot)
                    {
                        case SpellSlot.Q:
                            return Config.Modes.Harass.UseQ;
                        case SpellSlot.E:
                            return Config.Modes.Harass.UseE;
                    }
                    break;
                case Orbwalker.ActiveModes.LaneClear:
                    switch (spell.Slot)
                    {
                        case SpellSlot.Q:
                            return Config.Modes.LaneClear.UseQ;
                        case SpellSlot.W:
                            return Config.Modes.LaneClear.UseW;
                        case SpellSlot.E:
                            return Config.Modes.LaneClear.UseE;
                        case SpellSlot.R:
                            return Config.Modes.LaneClear.UseR;
                    }
                    break;
                case Orbwalker.ActiveModes.JungleClear:
                    switch (spell.Slot)
                    {
                        case SpellSlot.Q:
                            return Config.Modes.JungleClear.UseQ;
                        case SpellSlot.W:
                            return Config.Modes.JungleClear.UseW;
                        case SpellSlot.E:
                            return Config.Modes.JungleClear.UseE;
                        case SpellSlot.R:
                            return Config.Modes.JungleClear.UseR;
                    }
                    break;
            }

            return false;
        }

        public static bool IsEnabledAndReady(this Spell.SpellBase spell, Orbwalker.ActiveModes mode)
        {
            return spell.IsEnabled(mode) && spell.IsReady();
        }

        public static AIHeroClient GetTarget(this Spell.SpellBase spell, params AIHeroClient[] excludeTargets)
        {
            var targets = EntityManager.Heroes.Enemies.Where(o => o.IsValidTarget() && !excludeTargets.Contains(o) && spell.IsInRange(o)).ToArray();
            return TargetSelector.GetTarget(targets, DamageType.Mixed);
        }

        public static bool CastOnBestTarget(this Spell.SpellBase spell)
        {
            var target = spell.GetTarget();
            return target != null && spell.Cast(target);
        }

        private static Color ToArgb(this Color color, byte a)
        {
            return new ColorBGRA(color.R, color.G, color.B, a);
        }

        public static Color GetColor(this Spell.SpellBase spell)
        {
            return ColorTranslation.ContainsKey(spell.Slot) ? ColorTranslation[spell.Slot] : Color.Wheat;
        }
    }
}
