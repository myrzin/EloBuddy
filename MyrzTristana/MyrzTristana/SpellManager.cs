using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using SharpDX;

namespace MyrzTristana
{
    public static class SpellManager
    {
        public static Spell.Active Q { get; private set; }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Targeted E { get; private set; }
        public static Spell.Targeted R { get; private set; }

        public static Spell.SpellBase[] Spells { get; private set; }
        public static Dictionary<SpellSlot, Color> ColorTranslation { get; private set; }

        public static void Initialize()
        {
            // Initialize spells
            var range = (uint)(630 + 7*(Player.Instance.Level - 1));
            Q = new Spell.Active(SpellSlot.Q, range);
            W = new Spell.Skillshot(SpellSlot.W, 925, SkillShotType.Circular, 250, 1200, 150);
            E = new Spell.Targeted(SpellSlot.E, range);
            R = new Spell.Targeted(SpellSlot.R, range);

            Spells = (new Spell.SpellBase[] { Q, W, E, R }).OrderByDescending(o => o.Range).ToArray();
            ColorTranslation = new Dictionary<SpellSlot, Color>
            {
                { SpellSlot.W, Color.IndianRed.ToArgb(150) },
            };
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
                /*case Orbwalker.ActiveModes.LaneClear:
                    switch (spell.Slot)
                    {
                        case SpellSlot.Q:
                            return Config.Modes.LaneClear.UseQ;
                        case SpellSlot.E:
                            return Config.Modes.LaneClear.UseE;
                    }
                    break;
                case Orbwalker.ActiveModes.JungleClear:
                    switch (spell.Slot)
                    {
                        case SpellSlot.Q:
                            return Config.Modes.JungleClear.UseQ;
                        case SpellSlot.E:
                            return Config.Modes.JungleClear.UseE;
                    }
                    break;*/
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
            return TargetSelector.GetTarget(targets, DamageType.Physical);
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
