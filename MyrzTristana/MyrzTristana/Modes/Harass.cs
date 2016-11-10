using EloBuddy;
using EloBuddy.SDK;
using h = MyrzTristana.Config.Modes.Harass;

namespace MyrzTristana.Modes
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
            if (E.IsEnabledAndReady(Orbwalker.ActiveModes.Harass))
            {
                var target = TargetSelector.GetTarget(E.Range, DamageType.Magical) ?? TargetSelector.GetTarget(E.Range, DamageType.Physical);
                if (target != null && h.UseE && h.ManaUsage < Player.ManaPercent)
                {
                    E.Cast(target);
                }
            }
        }
    }
}
