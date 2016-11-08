using System;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Utils;

namespace MyrzTristana.Modes
{
    public abstract class ModeBase
    {
        public static AIHeroClient Player => EloBuddy.Player.Instance;

        public static Spell.Active Q => SpellManager.Q;

        public static Spell.Skillshot W => SpellManager.W;

        public static Spell.Targeted E => SpellManager.E;

        public static Spell.Targeted R => SpellManager.R;

        public abstract bool ShouldBeExecuted();

        public abstract void Execute();
    }

    public static class ModeManager
    {
        private static readonly List<ModeBase> _availableModes = new List<ModeBase>();

        static ModeManager()
        {
            // Add all modes
            _availableModes.AddRange(new ModeBase[]
            {
                new PermaActive(),
                new Combo(),
                new Harass(),
                new LaneClear(),
                new JungleClear(),
                new LastHit(),
                new Flee()
            });

            // Listen to required events
            Game.OnTick += OnTick;
        }

        private static void OnTick(EventArgs args)
        {
            _availableModes.ForEach(mode =>
            {
                try
                {
                    if (mode.ShouldBeExecuted())
                    {
                        mode.Execute();
                    }
                }
                catch (Exception e)
                {
                    Logger.Error("There was an error executing mode {0}!\n{1}", mode.GetType().Name, e);
                }
            });
        }

        public static void Initialize()
        {
        }
    }
}
