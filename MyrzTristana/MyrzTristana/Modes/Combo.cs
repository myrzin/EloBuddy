﻿using EloBuddy;
using EloBuddy.SDK;

namespace MyrzTristana.Modes
{
    public sealed class Combo : ModeBase
    {
        public bool WSent { get; set; }
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            if (W.IsReady() && E.IsReady() &&
                R.IsReady() && Config.Modes.Combo.UseWMinMana <= Player.Mana)
            {
                var target = TargetSelector.GetTarget(W.Range, DamageType.Physical) ?? TargetSelector.GetTarget(E.Range, DamageType.Magical);
                if (target != null && Config.Modes.Combo.FullCombo &&
                    target.TotalShieldHealth() < (W.GetRealDamage(target) + E.GetRealDamage(target) + R.GetRealDamage(target)))
                {
                    var pred = W.GetPrediction(target);
                    W.Cast(pred.CastPosition);
                    E.Cast(target);
                }
            }

            if (W.IsEnabledAndReady(Orbwalker.ActiveModes.Combo))
            {
                var target = TargetSelector.GetTarget(W.Range, DamageType.Physical) ?? TargetSelector.GetTarget(W.Range, DamageType.Magical);
                if (target != null && Config.Modes.Combo.UseW && Config.Modes.Combo.UseWMinHealth < Player.HealthPercent && Config.Modes.Combo.UseWMinMana <= Player.Mana)
                {
                    var pred = W.GetPrediction(target);
                    if (Config.Modes.Combo.UseWKill && W.GetRealDamage(target) > target.Health)
                    {
                        if (!Config.Modes.Combo.UseWTower && !target.IsUnderHisturret())
                        {
                            if (Config.Modes.Combo.UseWMax >= target.CountEnemiesInRange(1500))
                            {
                                W.Cast(pred.CastPosition);
                            }
                        }
                        else if (!Config.Modes.Combo.UseWStacks && Config.Modes.Combo.UseWTower)
                        {
                            if (Config.Modes.Combo.UseWMax >= target.CountEnemiesInRange(1500))
                            {
                                W.Cast(pred.CastPosition);
                            }
                        }
                    }
                    else if (!Config.Modes.Combo.UseWKill && Config.Modes.Combo.UseWStacks && Damages.GetStacks(target) == 4)
                    {
                        if (!Config.Modes.Combo.UseWTower && !target.IsUnderHisturret())
                        {
                            if (Config.Modes.Combo.UseWMax >= target.CountEnemiesInRange(1500))
                            {
                                W.Cast(pred.CastPosition);
                            }
                        }
                        else if (Config.Modes.Combo.UseWTower)
                        {
                            if (Config.Modes.Combo.UseWMax >= target.CountEnemiesInRange(1500))
                            {
                                W.Cast(pred.CastPosition);
                            }
                        }
                    }
                    else if (!Config.Modes.Combo.UseWKill && !Config.Modes.Combo.UseWStacks && !Config.Modes.Combo.UseWTower && !target.IsUnderHisturret())
                    {
                        if (Config.Modes.Combo.UseWMax >= target.CountEnemiesInRange(1500))
                        {
                            W.Cast(pred.CastPosition);
                        }
                    }
                    else if (!Config.Modes.Combo.UseWKill && !Config.Modes.Combo.UseWStacks && Config.Modes.Combo.UseWTower)
                    {
                        if (Config.Modes.Combo.UseWMax >= target.CountEnemiesInRange(1500))
                        {
                            W.Cast(pred.CastPosition);
                        }
                    }
                }
            }

            if (E.IsEnabledAndReady(Orbwalker.ActiveModes.Combo))
            {
                var target = TargetSelector.GetTarget(E.Range, DamageType.Physical) ?? TargetSelector.GetTarget(E.Range, DamageType.Magical);
                if (target != null && Config.Modes.Combo.UseE)
                {
                    E.Cast(target);
                }
            }
        }
    }
}
