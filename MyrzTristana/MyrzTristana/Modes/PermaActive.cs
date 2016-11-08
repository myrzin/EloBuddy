﻿using System;
using EloBuddy;
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
            if (W.IsReady() && Config.Modes.Combo.UseW && Config.PermaActive.WKs)
            {
                var target = TargetSelector.GetTarget(W.Range, DamageType.Physical) ??
                             TargetSelector.GetTarget(W.Range, DamageType.Magical);
                
                if (target != null && W.GetRealDamage(target) > target.TotalShieldHealth())
                {
                    var pred = W.GetPrediction(target);
                    if (!Config.Modes.Combo.UseWTower && target.IsUnderHisturret())
                    {
                        if (Config.Modes.Combo.UseWMax >= target.CountEnemiesInRange(1500))
                        {
                            Console.WriteLine("KSing with W");
                            W.Cast(pred.CastPosition);
                        }
                    }
                    else if (Config.Modes.Combo.UseWTower)
                    {
                        if (Config.Modes.Combo.UseWMax >= target.CountEnemiesInRange(1500))
                        {
                            Console.WriteLine("KSing with W");
                            W.Cast(pred.CastPosition);
                        }
                    }
                }
            }
            if (R.IsReady() && Config.PermaActive.RKs)
            {
                var target = TargetSelector.GetTarget(W.Range, DamageType.Physical) ??
                             TargetSelector.GetTarget(W.Range, DamageType.Magical);
                if (target != null && target.TotalShieldHealth() < R.GetRealDamage(target))
                {
                    R.Cast(target);
                }
            }
            if (R.IsReady() && Config.PermaActive.ERKs)
            {
                var target = TargetSelector.GetTarget(W.Range, DamageType.Physical) ??
                             TargetSelector.GetTarget(W.Range, DamageType.Magical);
                if (target != null && target.HasBuff("TristanaECharge") &&
                    target.TotalShieldHealth() < (R.GetRealDamage(target) + E.GetRealDamage(target)))
                {
                    R.Cast(target);
                }
            }
        }
    }
}
