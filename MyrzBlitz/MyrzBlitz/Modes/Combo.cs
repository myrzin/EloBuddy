using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

namespace MyrzBlitz.Modes
{
    public sealed class Combo : ModeBase
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

        public bool QSent { get; set; }

        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            if (Q.IsEnabledAndReady(Orbwalker.ActiveModes.Combo))
            {
                var target = TargetSelector.GetTarget(900, DamageType.Magical) ?? TargetSelector.GetTarget(900, DamageType.Physical);
                if (target != null && target.IsValid)
                {
                    var prediction = Q.GetPrediction(target);
                    if (prediction.HitChancePercent >= Config.Misc.MinPred && target.Distance(Player.ServerPosition) >= Config.Misc.MinDisQ)
                    {
                        Q.Cast(prediction.CastPosition);
                    }
                }
            }


            
            if (Config.Modes.Combo.UseSmiteQ && Q.IsEnabledAndReady(Orbwalker.ActiveModes.Combo))
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
            

            if (W.IsEnabledAndReady(Orbwalker.ActiveModes.Combo))
            {
                var target = TargetSelector.GetTarget(900, DamageType.Magical) ?? TargetSelector.GetTarget(900, DamageType.Physical);
                if (target != null)
                {
                        W.Cast();
                }
            }

            if (E.IsEnabledAndReady(Orbwalker.ActiveModes.Combo))
            {
                var target = TargetSelector.GetTarget(300, DamageType.Magical) ?? TargetSelector.GetTarget(300, DamageType.Physical);
                if (target != null)
                {
                    E.Cast();
                }
            }

            if (R.IsEnabledAndReady(Orbwalker.ActiveModes.Combo))
            {
                var target = EntityManager.Heroes.Enemies.Where(t => t.IsValidTarget(SpellManager.R.Range) && t.IsValid).ToArray();
                if (target.Count() >= Config.Modes.Combo.UseRMin)
                {
                    R.Cast();
                }
            }
        }
    }
}
