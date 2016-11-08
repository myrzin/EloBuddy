using System.Linq;
using EloBuddy.SDK;
using Settings = MyrzBlitz.Config.Modes.LaneClear;

namespace MyrzBlitz.Modes
{
    public sealed class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        }

        public override void Execute()
        {
            if (!Q.IsEnabledAndReady(Orbwalker.ActiveModes.LaneClear) && !W.IsEnabledAndReady(Orbwalker.ActiveModes.LaneClear) && !E.IsEnabledAndReady(Orbwalker.ActiveModes.LaneClear) && !R.IsEnabledAndReady(Orbwalker.ActiveModes.LaneClear))
            {
                return;
            }

            var minions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.ServerPosition, R.Range, false).ToArray();
            if (minions.Length == 0)
            {
                return;
            }

            if (Q.IsEnabledAndReady(Orbwalker.ActiveModes.LaneClear))
            {
                var farmLocation = Q.GetBestLinearCastPosition(minions);
                if (farmLocation.HitNumber > 0 && Config.Modes.LaneClear.UseQ && Config.Modes.LaneClear.ManaUsage < Player.ManaPercent)
                {
                    Q.Cast(farmLocation.CastPosition);
                }
            }

            if (W.IsEnabledAndReady(Orbwalker.ActiveModes.LaneClear))
            {
                if (minions.Length > 0 && Config.Modes.LaneClear.UseW && Config.Modes.LaneClear.ManaUsage < Player.ManaPercent)
                {
                    W.Cast();
                }
            }

            if (E.IsEnabledAndReady(Orbwalker.ActiveModes.LaneClear))
            {
                if (minions.Length > 0 && Config.Modes.LaneClear.UseE && Config.Modes.LaneClear.ManaUsage < Player.ManaPercent)
                {
                    E.Cast();
                }
            }

            if (R.IsEnabledAndReady(Orbwalker.ActiveModes.LaneClear))
            {
                if (minions.Length >= Config.Modes.LaneClear.HitNumberR && Config.Modes.LaneClear.UseR &&
                    Config.Modes.LaneClear.ManaUsage < Player.ManaPercent)
                {
                    R.Cast();
                }
            }
        }
    }
}