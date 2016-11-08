using System.Linq;
using EloBuddy.SDK;

namespace MyrzBlitz.Modes
{
    public sealed class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            var active = Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
            if (!Q.IsEnabledAndReady(Orbwalker.ActiveModes.JungleClear) && !W.IsEnabledAndReady(Orbwalker.ActiveModes.JungleClear) && !E.IsEnabledAndReady(Orbwalker.ActiveModes.JungleClear) && !R.IsEnabledAndReady(Orbwalker.ActiveModes.JungleClear))
            {
                return;
            }

            var minions = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.ServerPosition, R.Range, false).ToArray();
            if (minions.Length == 0)
            {
                return;
            }

            if (Q.IsEnabledAndReady(Orbwalker.ActiveModes.JungleClear))
            {
                if (Config.Modes.JungleClear.UseQ && Config.Modes.JungleClear.ManaUsage < Player.ManaPercent)
                {
                    Q.Cast(minions[0]);
                }
            }

            if (W.IsEnabledAndReady(Orbwalker.ActiveModes.JungleClear))
            {
                if (minions.Length > 0 && Config.Modes.JungleClear.UseW && Config.Modes.JungleClear.ManaUsage < Player.ManaPercent)
                {
                    W.Cast();
                }
            }

            if (E.IsEnabledAndReady(Orbwalker.ActiveModes.JungleClear))
            {
                if (minions.Length > 0 && Config.Modes.JungleClear.UseE && Config.Modes.JungleClear.ManaUsage < Player.ManaPercent)
                {
                    E.Cast();
                }
            }

            if (R.IsEnabledAndReady(Orbwalker.ActiveModes.JungleClear))
            {
                if (minions.Length >= Config.Modes.JungleClear.HitNumberR && Config.Modes.JungleClear.UseR &&
                    Config.Modes.JungleClear.ManaUsage < Player.ManaPercent)
                {
                    R.Cast();
                }
            }
        }
    }
}
