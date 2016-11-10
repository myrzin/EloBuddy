using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
// ReSharper disable All


namespace MyrzBlitz
{
    public static class Config
    {
        private const string MenuName = "Blitz";

        private static readonly Menu Menu;

        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName+"_Myrzin");
            Menu.AddGroupLabel("Myrzin's Blitzcrank");

            Modes.Initialize();
            Misc.Initialize();
            Drawing.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class Modes
        {
            private static readonly Menu Menu;

            static Modes()
            {
                Menu = Config.Menu.AddSubMenu(MenuName);

                Combo.Initialize();
                Menu.AddSeparator();
                Harass.Initialize();
                Menu.AddSeparator();
                LaneClear.Initialize();
                Menu.AddSeparator();
                JungleClear.Initialize();
                Menu.AddSeparator();
                PermaActive.Initialize();
                Menu.AddSeparator();
            }

            public static void Initialize()
            {
            }

            public static class Combo
            {
                public const string GroupName = "Combo";

                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly CheckBox _useR;
                private static readonly CheckBox _useSmiteQ;
                private static readonly Slider _useRMin;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }
                public static bool UseR
                {
                    get { return _useR.CurrentValue; }
                }

                public static int UseRMin
                {
                    get { return _useRMin.CurrentValue; }
                }

                public static bool UseSmiteQ
                {
                    get { return _useSmiteQ.CurrentValue; }
                }

                static Combo()
                {
                    // Initialize the menu values
                    Menu.AddGroupLabel(GroupName);
                    _useQ = Menu.Add("comboUseQ", new CheckBox("Use Q"));
                    _useW = Menu.Add("comboUseW", new CheckBox("Use W", false));
                    _useE = Menu.Add("comboUseE", new CheckBox("Use E"));
                    _useR = Menu.Add("comboUseR", new CheckBox("Use R"));
                    _useSmiteQ = Menu.Add("smiteQ", new CheckBox("Use Smite > Q"));
                    _useRMin = Menu.Add("comboMinR", new Slider("Min Enemies in Range to use R", 1, 1, 5));
                    
                }

                public static void Initialize()
                {
                }
            }

            public static class Harass
            {
                public const string GroupName = "Harass";

                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useE;
                private static readonly Slider _mana;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                public static int ManaUsage
                {
                    get { return _mana.CurrentValue; }
                }

                static Harass()
                {
                    Menu.AddGroupLabel(GroupName);

                    _useQ = Menu.Add("harassUseQ", new CheckBox("Use Q"));
                    _useE = Menu.Add("harassUseE", new CheckBox("Use E"));
                    _mana = Menu.Add("harassMana", new Slider("Mana Usage ", 40));

                }

                public static void Initialize()
                {
                }
            }

            public static class LaneClear
            {
                public const string GroupName = "LaneClear";

                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly CheckBox _useR;

                private static readonly Slider _hitNumR;
                private static readonly Slider _mana;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }
                public static bool UseR
                {
                    get { return _useR.CurrentValue; }
                }

                public static int HitNumberR
                {
                    get { return _hitNumR.CurrentValue; }
                }
                
                public static int ManaUsage
                {
                    get { return _mana.CurrentValue; }
                }

                static LaneClear()
                {
                    Menu.AddGroupLabel(GroupName);

                    _useQ = Menu.Add("laneUseQ", new CheckBox("Use Q"));
                    _useW = Menu.Add("laneUseW", new CheckBox("Use W"));
                    _useE = Menu.Add("laneUseE", new CheckBox("Use E"));
                    _useR = Menu.Add("laneUseR", new CheckBox("Use R"));

                    _hitNumR = Menu.Add("laneHitR", new Slider("Hit number for R", 3, 1, 10));
                    _mana = Menu.Add("laneMana", new Slider("Mana usage in percent (%)", 30));
                }

                public static void Initialize()
                {
                }
            }

            public static class JungleClear
            {
                public const string GroupName = "JungleClear";

                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly CheckBox _useR;

                private static readonly Slider _hitNumR;
                private static readonly Slider _mana;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }
                public static bool UseR
                {
                    get { return _useR.CurrentValue; }
                }

                public static int HitNumberR
                {
                    get { return _hitNumR.CurrentValue; }
                }

                public static int ManaUsage
                {
                    get { return _mana.CurrentValue; }
                }

                static JungleClear()
                {
                    Menu.AddGroupLabel(GroupName);

                    _useQ = Menu.Add("jungleUseQ", new CheckBox("Use Q"));
                    _useW = Menu.Add("jungleUseW", new CheckBox("Use W"));
                    _useE = Menu.Add("jungleUseE", new CheckBox("Use E"));
                    _useR = Menu.Add("jungleUseR", new CheckBox("Use R"));

                    _hitNumR = Menu.Add("jungleHitR", new Slider("Hit number for R", 3, 1, 10));
                    _mana = Menu.Add("jungleMana", new Slider("Mana usage in percent (%)", 30));
                }

                public static void Initialize()
                {
                }
            }
        }

        public static class PermaActive
        {
            public const string MenuName = "PermaActive";

            private static readonly CheckBox _qKs;
            private static readonly CheckBox _rKs;
            private static readonly CheckBox _qDashing;
            private static readonly CheckBox _qUnder;
            private static readonly CheckBox _smiteQUnder;

            public static bool QKs
            {
                get { return _qKs.CurrentValue; }
            }

            public static bool RKs
            {
                get { return _rKs.CurrentValue; }
            }

            public static bool QDashing
            {
                get { return _qDashing.CurrentValue; }
            }

            public static bool QUnder
            {
                get { return _qUnder.CurrentValue; }
            }

            public static bool SmiteQunder
            {
                get { return _smiteQUnder.CurrentValue; }
            }

            static PermaActive()
            {
                var menu = Menu.AddSubMenu(MenuName);

                _qKs = menu.Add("permaQKs", new CheckBox("Ks With Q"));
                _rKs = menu.Add("permaRKs", new CheckBox("Ks With R"));
                _qDashing = menu.Add("permaQDashing", new CheckBox("Auto Q on Dashing/Imobille"));
                _qUnder = menu.Add("qUnder", new CheckBox("Auto Q Under Turret"));
                _smiteQUnder = menu.Add("smiteQUnder", new CheckBox("Auto Smite > Q Under Tower"));
            }

            public static void Initialize()
            {
            }
        }

        public static class Misc
        {
            public const string MenuName = "Miscellaneous";

            //private static readonly CheckBox _gapcloser;
            private static readonly CheckBox _interrupterQ;
            private static readonly CheckBox _interrupterR;
            private static readonly Slider _minDisQ;
            private static readonly Slider _minPred;

            /*public static bool GapcloserQ
            {
                get { return _gapcloser.CurrentValue; }
            }*/
            public static bool InterruptQ
            {
                get { return _interrupterQ.CurrentValue; }
            }

            public static bool InterruptR
            {
                get { return _interrupterQ.CurrentValue; }
            }

            public static int MinDisQ
            {
                get { return _minDisQ.CurrentValue; }
            }

            public static int MinPred
            {
                get { return _minPred.CurrentValue; }
            }

            static Misc()
            {
                // Initialize menu
                var menu = Menu.AddSubMenu(MenuName);

                //_gapcloser = menu.Add("miscGapcloseQ", new CheckBox("Use Q against gapclosers"));
                _interrupterQ = menu.Add("miscInterruptQ", new CheckBox("Use Q to interrupt dangerous spells"));
                _interrupterR = menu.Add("miscInterruptR", new CheckBox("Use R to interrupt dangerous spells"));
                _minDisQ = menu.Add("minDisQ", new Slider("Min. Distance to Use Q", 300, 0, 800));
                _minPred = menu.Add("minPred", new Slider("Minimum Hitchance for Q:", 70, 0, 100));
            }

            public static void Initialize()
            {
            }
        }

        public static class Drawing
        {
            public const string MenuName = "Drawing";

            private static readonly CheckBox _drawQ;
            private static readonly CheckBox _drawR;
            private static readonly CheckBox _showStats;
            private static readonly CheckBox _healthbar;

            public static bool DrawQ
            {
                get { return _drawQ.CurrentValue; }
            }
            public static bool DrawR
            {
                get { return _drawR.CurrentValue; }
            }

            public static bool ShowStats
            {
                get { return _showStats.CurrentValue; }
            }
            public static bool IndicatorHealthbar
            {
                get { return _healthbar.CurrentValue; }
            }

            static Drawing()
            {
                // Initialize menu
                var menu = Menu.AddSubMenu(MenuName);

                menu.AddGroupLabel("Spell ranges");
                _drawQ = menu.Add("drawQ", new CheckBox("Q range"));
                _drawR = menu.Add("drawR", new CheckBox("R range", false));
                _showStats = menu.Add("showStats", new CheckBox("Draw Grab Stats"));
                menu.AddGroupLabel("Damage indicators");
                _healthbar = menu.Add("healthbar", new CheckBox("Healthbar overlay"));
            }

            public static void Initialize()
            {
            }
        }
    }
}