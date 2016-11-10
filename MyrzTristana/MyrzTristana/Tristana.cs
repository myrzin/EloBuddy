using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using MyrzTristana.Modes;
using Color = System.Drawing.Color;

namespace MyrzTristana
{
    public static class Tristana
    {
        public static void Main(string[] args)
        {
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            // Validate champ
            if (Player.Instance.Hero != Champion.Tristana)
            {
                return;
            }

            // Initialize classes
            SpellManager.Initialize();
            Config.Initialize();
            ModeManager.Initialize();

            // Initialize damage indicator
            DamageIndicator.Initialize(Damages.GetTotalDamage);
            DamageIndicator.DrawingColor = Color.Goldenrod;

            // Listend to required events
            Drawing.OnDraw += OnDraw;
            Gapcloser.OnGapcloser += OnGapcloser;
            Interrupter.OnInterruptableSpell += OnInterruptableSpell;
            Orbwalker.OnPostAttack += OnPostAttack;
        }

        private static void OnDraw(EventArgs args)
        {
            if (Config.Drawing.DrawW)
            {
                Circle.Draw(SpellManager.W.GetColor(), SpellManager.W.Range, Player.Instance);
            }

            

            if (Config.Drawing.DrawEStacks)
            {
                foreach (var unit in EntityManager.Heroes.Enemies.Where(e => e.IsValidTarget() && e.HasBuff("TristanaEChargeSound")))
                {
                    if (SpellManager.E.Level > 0)
                    {
                        var x = unit.HPBarPosition.X + 25;
                        var y = unit.HPBarPosition.Y - 30;
                        var stacks = Damages.GetStacks(unit);
                        if (unit.HasBuff("TristanaEChargeSound"))
                        {
                            if (stacks > 0)
                            {
                                for (var i = 0; 4 > i; i++)
                                {
                                    Drawing.DrawLine(x + i * 20, y, x + i * 20 + 10, y, 10, i > (stacks-1) ? Color.PapayaWhip : Color.DarkRed);
                                }
                            }
                        }
                    }
                }               
            }
            

            DamageIndicator.HealthbarEnabled = Config.Drawing.IndicatorHealthbar;
        }

        private static void OnPostAttack(AttackableUnit unit, EventArgs args)
        {
            if (!(unit is AIHeroClient))
            {
                return;
            }
            var target = TargetSelector.GetTarget(SpellManager.Q.Range, DamageType.Physical);
            var hero = (AIHeroClient)unit;
            if (!hero.IsValid || hero.Type != GameObjectType.AIHeroClient)
            {
                return;
            }
            if (target != null)
            {
                if (SpellManager.Q.IsEnabledAndReady(Orbwalker.ActiveModes.Combo) && Config.Modes.Combo.UseQ)
                {
                    SpellManager.Q.Cast();
                }
                if (SpellManager.Q.IsEnabledAndReady(Orbwalker.ActiveModes.Harass) && Config.Modes.Harass.UseQ)
                {
                    SpellManager.Q.Cast();
                }
            }
        }

        private static void OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs args)
        {
            if (sender.IsEnemy && Config.PermaActive.GapcloserR && SpellManager.R.IsReady() && SpellManager.R.IsInRange(args.End))
            {
                // Cast R on the gapcloser caster
                SpellManager.R.Cast();
            }
        }

        private static void OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs args)
        {
            if (sender.IsEnemy && args.DangerLevel == DangerLevel.High && Config.PermaActive.InterruptR && SpellManager.R.IsReady() && SpellManager.R.IsInRange(sender))
            {
                // Cast R on the unit casting the interruptable spell
                SpellManager.R.Cast();
            }
        }
    }
}
