using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;

namespace MyrzBlitz.Modes
{
    public sealed class PermaActive : ModeBase
    {
        int GetSmiteDamage()
        {
            int[] calcSmiteDamage =
            {
                20 * ObjectManager.Player.Level + 370,
                30 * ObjectManager.Player.Level + 330,
                40 * ObjectManager.Player.Level + 240,
                50 * ObjectManager.Player.Level + 100
            };

            return calcSmiteDamage.Max();
        }

        public override bool ShouldBeExecuted()
        {
            return true;
        }

        public override void Execute()
        {
            if (Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Mixed);
                if (target != null)
                {
                    var prediction = Q.GetPrediction(target);
                    var qDmg = Q.GetRealDamage(target);
                    if (prediction.HitChancePercent >= Config.Misc.MinPred && qDmg >= target.TotalShieldHealth() && Config.PermaActive.QKs)
                    {
                        Q.Cast(prediction.CastPosition);
                    }
                }
            }

            if (Q.IsReady() && R.IsReady())
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Mixed);
                if (target != null)
                {
                    var prediction = Q.GetPrediction(target);
                    var qDmg = Q.GetRealDamage(target)+R.GetRealDamage(target);
                    if (prediction.HitChancePercent >= Config.Misc.MinPred && qDmg >= target.TotalShieldHealth() && Config.PermaActive.QKs)
                    {
                        Q.Cast(prediction.CastPosition);
                    }
                }
            }

            if (R.IsReady())
            {
                var target = EntityManager.Heroes.Enemies.Where(e => e.IsValidTarget(R.Range) && e.TotalShieldHealth() < R.GetRealDamage(e)).OrderByDescending(e => R.GetRealDamage(e)).ToArray();
                if (target.Length > 0 && Config.PermaActive.RKs)
                {
                    R.Cast();
                }
            }

            if (Q.IsReady() && Config.PermaActive.QDashing)
            {
                foreach (
                    var target in
                        EntityManager.Heroes.Enemies.Where(
                            e =>
                                e.IsValidTarget() && e != null && e.Distance(Player.ServerPosition) < Q.Range &&
                                e.IsDashing()))
                {
                    var pred = Q.GetPrediction(target);
                    if (pred.HitChance == HitChance.Dashing)
                    {
                        Q.Cast(pred.CastPosition);
                    }
                }
                foreach (
                    var target in
                        EntityManager.Heroes.Enemies.Where(
                            e =>
                                e.IsValidTarget() && e != null && e.Distance(Player.ServerPosition) < Q.Range &&
                                !e.CanMove))
                {
                    var pred = Q.GetPrediction(target);
                    if (pred.HitChance == HitChance.Immobile)
                    {
                        Q.Cast(pred.CastPosition);
                    }
                }
            }

            if (Q.IsReady() && Player.IsUnderHisturret() && Config.PermaActive.QUnder)
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Mixed);
                if (target != null)
                {
                    var pred = Q.GetPrediction(target);
                        if (pred.HitChance == HitChance.Dashing || pred.HitChance == HitChance.Immobile || pred.HitChance == HitChance.High)
                        {
                            Q.Cast(pred.CastPosition);
                        }
                }
            }

            if (Q.IsReady() && Player.IsUnderHisturret() && Config.PermaActive.SmiteQunder)
            {
                var target = TargetSelector.GetTarget(900, DamageType.Magical) ?? TargetSelector.GetTarget(900, DamageType.Physical);
                if (target != null)
                {
                    var pred = Q.GetPrediction(target);
                    var collisions = pred.CollisionObjects.Where(x => !(x is AIHeroClient)).ToList();
                    if (collisions.Count == 1 && collisions[0].Distance(ObjectManager.Player) <= SpellManager.Smite.Range &&
                        collisions[0].Health <= GetSmiteDamage() && target.IsValid && target.Distance(Player.ServerPosition) >= Config.Misc.MinDisQ)
                    {
                        Q.Cast(pred.CastPosition);
                        Core.RepeatAction(() => SpellManager.Smite.Cast(pred.CollisionObjects[0]), 50, 1500);
                    }
                }
            }
        }
    }
}
