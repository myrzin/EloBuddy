using EloBuddy;
using EloBuddy.SDK;

namespace MyrzBlitz.Modes
{
    public sealed class Harass : ModeBase
    {
        public bool QSent { get; set; }

        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {
            if (Q.IsEnabledAndReady(Orbwalker.ActiveModes.Harass))
            {
                var target = TargetSelector.GetTarget(900, DamageType.Magical) ?? TargetSelector.GetTarget(900, DamageType.Physical);
                if (target != null && Config.Modes.Harass.UseQ && Config.Modes.Harass.ManaUsage < Player.ManaPercent && target.Distance(Player.ServerPosition) >= Config.Misc.MinDisQ)
                {
                    var prediction = Q.GetPrediction(target);
                    if (prediction.HitChancePercent >= Config.Misc.MinPred)
                    {
                        Q.Cast(prediction.CastPosition);
                    }
                }
            }

            if (E.IsEnabledAndReady(Orbwalker.ActiveModes.Harass))
            {
                var target = TargetSelector.GetTarget(E.Range, DamageType.Mixed);
                if (target != null && Config.Modes.Harass.UseE && Config.Modes.Harass.ManaUsage < Player.ManaPercent)
                {
                    E.Cast();
                }
            }
        }
    }
}
