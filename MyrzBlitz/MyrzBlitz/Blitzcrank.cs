using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using MyrzBlitz.Modes;

namespace MyrzBlitz
{
    public static class Blitzcrank
    {

        public static int grabT;
        public static int grabS;
        public static float grabP;
        public static bool HasIgnite { get; private set; }

        public static void Main(string[] args)
        {
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            // Validate champ
            if (Player.Instance.Hero != Champion.Blitzcrank)
            {
                return;
            }

            // Initialize classes
            SpellManager.Initialize();
            Config.Initialize();
            ModeManager.Initialize();

            // Check if the player has ignite
            HasIgnite = Player.Instance.GetSpellSlotFromName("SummonerDot") != SpellSlot.Unknown;

            // Initialize damage indicator
            DamageIndicator.Initialize(Damages.GetTotalDamage);
            DamageIndicator.DrawingColor = System.Drawing.Color.Goldenrod;

            // Listend to required events
            Drawing.OnDraw += OnDraw;
            Gapcloser.OnGapcloser += OnGapcloser;
            Interrupter.OnInterruptableSpell += OnInterruptableSpell;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe && args.SData.Name == "RocketGrab")
                grabT++;
        }

        private static void OnDraw(EventArgs args)
        {
            if (!SpellManager.Q.IsReady() && Game.Time - grabP > 2)
            {
                foreach (var t in EntityManager.Heroes.Enemies.Where(t => t.HasBuff("rocketgrab2")))
                {
                    grabS++;
                    grabP = Game.Time;
                }
            }

            if (Config.Drawing.ShowStats)
            {
                if (grabT > 0)
                {
                    // ReSharper disable once PossibleLossOfFraction
                    var percent = (float)grabS / (float)grabT * 100f;
                    Drawing.DrawText(Drawing.Width * 0f, Drawing.Height * 0.12f, System.Drawing.Color.GreenYellow, " Grabs Thrown: " + grabT);
                    Drawing.DrawText(Drawing.Width * 0f, Drawing.Height * 0.138f, System.Drawing.Color.GreenYellow, " Successful Grabs: " + grabS);
                    Drawing.DrawText(Drawing.Width * 0f, Drawing.Height * 0.156f, System.Drawing.Color.GreenYellow, " Successful:" + percent + "%");
                }
            }

            if (Config.Drawing.DrawQ)
            {
                Circle.Draw(SpellManager.Q.GetColor(), SpellManager.Q.Range, Player.Instance);
            }

            if (Config.Drawing.DrawR)
            {
                Circle.Draw(SpellManager.R.GetColor(), SpellManager.R.Range, Player.Instance);
            }

            DamageIndicator.HealthbarEnabled = Config.Drawing.IndicatorHealthbar;
        }

        private static void OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs args)
        {
            if (sender.IsEnemy && Config.Misc.GapcloserR && SpellManager.R.IsReady() && SpellManager.R.IsInRange(args.End))
            {
                // Cast E on the gapcloser caster
                SpellManager.R.Cast();
            }
        }

        private static void OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs args)
        {
            if (sender.IsEnemy && args.DangerLevel == DangerLevel.High && Config.Misc.InterruptR && SpellManager.R.IsReady() && SpellManager.R.IsInRange(sender))
            {
                // Cast E on the unit casting the interruptable spell
                SpellManager.R.Cast();
            }
        }
    }
}
