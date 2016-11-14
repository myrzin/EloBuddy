using System;
using System.Linq;
using System.Runtime.Remoting.Channels;
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

        public static int GrabT;
        public static int GrabS;
        public static float GrabP;
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
            Interrupter.OnInterruptableSpell += OnInterruptableSpell;
            Gapcloser.OnGapcloser += OnGapcloser;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
        }

        private static void OnGapcloser(Obj_AI_Base sender, Gapcloser.GapcloserEventArgs args)
        {
            if (Config.PermaActive.QDashing && sender.IsEnemy)
            {
                if (SpellManager.Q.IsReady() && Player.Instance.IsInRange(sender, SpellManager.Q.Range) &&
                    sender.Distance(Player.Instance.ServerPosition) >= Config.Misc.MinDisQ)
                {
                    SpellManager.Q.Cast(args.End);
                }
            }
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe && args.SData.Name == "RocketGrab")
                GrabT++;
            if (args.SData.Name.ToLower().Contains("summonerflash") && sender.IsEnemy)
            {
                if (SpellManager.Q.IsReady() && Player.Instance.IsInRange(sender, SpellManager.Q.Range) &&
                    sender.Distance(Player.Instance.ServerPosition) >= Config.Misc.MinDisQ)
                {
                    SpellManager.Q.Cast(args.End);
                }
            }
        }

        private static void OnDraw(EventArgs args)
        {
            if (!SpellManager.Q.IsReady() && Game.Time - GrabP > 2)
            {
                foreach (var t in EntityManager.Heroes.Enemies.Where(t => t.HasBuff("rocketgrab2")))
                {
                    GrabS++;
                    GrabP = Game.Time;
                }
            }

            if (Config.Drawing.ShowStats)
            {
                if (GrabT > 0)
                {
                    // ReSharper disable once PossibleLossOfFraction
                    var percent = (float)GrabS / (float)GrabT * 100f;
                    Drawing.DrawText(Drawing.Width * 0f, Drawing.Height * 0.12f, System.Drawing.Color.GreenYellow, " Grabs Thrown: " + GrabT);
                    Drawing.DrawText(Drawing.Width * 0f, Drawing.Height * 0.138f, System.Drawing.Color.GreenYellow, " Successful Grabs: " + GrabS);
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

        private static void OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs args)
        {
            if (sender.IsEnemy && args.DangerLevel == DangerLevel.High)
            {
                if (Config.Misc.InterruptR && SpellManager.R.IsReady() && SpellManager.R.IsInRange(sender))
                {
                    SpellManager.R.Cast();
                }
                else if (Config.Misc.InterruptQ && SpellManager.Q.IsReady() && SpellManager.Q.IsInRange(sender))
                {
                    var pred = SpellManager.Q.GetPrediction(sender);
                    SpellManager.Q.Cast(pred.CastPosition);
                }
            }
        }
    }
}
