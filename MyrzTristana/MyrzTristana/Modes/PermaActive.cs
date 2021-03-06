﻿using EloBuddy;
using EloBuddy.SDK;

namespace MyrzTristana.Modes
{
    public sealed class PermaActive : ModeBase
    {

        public override bool ShouldBeExecuted()
        {
            return true;
        }

        public override void Execute()
        {
            if (W.IsReady() && Config.Modes.Combo.UseW && Config.PermaActive.WKs && Config.Modes.Combo.UseWMax < Player.HealthPercent && Config.Modes.Combo.UseWMinMana <= Player.Mana)
            {
                var target = TargetSelector.GetTarget(W.Range, DamageType.Physical) ??
                             TargetSelector.GetTarget(W.Range, DamageType.Magical);
                
                if (target != null && W.GetRealDamage(target) > target.TotalShieldHealth())
                {
                    var pred = W.GetPrediction(target);
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
            }
            if (R.IsReady() && Config.PermaActive.RKs)
            {
                var target = TargetSelector.GetTarget(R.Range, DamageType.Physical) ??
                             TargetSelector.GetTarget(R.Range, DamageType.Magical);
                if (target != null && target.TotalShieldHealth() < R.GetRealDamage(target))
                {
                    R.Cast(target);
                }
            }
            if (R.IsReady() && Config.PermaActive.ERKs)
            {
                var target = TargetSelector.GetTarget(E.Range, DamageType.Physical) ??
                             TargetSelector.GetTarget(E.Range, DamageType.Magical);
                if (target != null && target.HasBuff("TristanaECharge") &&
                    target.TotalShieldHealth() < (R.GetRealDamage(target) + E.GetRealDamage(target)))
                {
                    R.Cast(target);
                }
            }
            if (R.IsReady() && W.IsReady() && Config.PermaActive.WKs && Config.PermaActive.RKs && Config.Modes.Combo.UseW && Config.Modes.Combo.UseWMax < Player.HealthPercent && Config.Modes.Combo.UseWMinMana <= Player.Mana)
            {
                var target = TargetSelector.GetTarget(W.Range, DamageType.Physical) ??
                             TargetSelector.GetTarget(W.Range, DamageType.Magical);
                if (target != null && target.TotalShieldHealth() < (R.GetRealDamage(target) + (W.GetRealDamage(target))))
                {
                    var pred = W.GetPrediction(target);
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

            }

            if (Config.PermaActive.FocusE)
            {
                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) ||
                    Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
                {
                    var target =
                        EntityManager.Heroes.Enemies.Find(
                            x => x.HasBuff("TristanaEChargeSound") && x.IsValidTarget(SpellManager.E.Range));
                    if (target != null && Player.Distance(target) < E.Range)
                    {
                        Orbwalker.ForcedTarget = target;
                    }
                    else Orbwalker.ForcedTarget = null;
                }
                else Orbwalker.ForcedTarget = null;
            }
        }
    }
}
