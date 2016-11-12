using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace MyrzTristana
{
    public static class Config
    {
        private const string MenuName = "Tristana";

        private static readonly Menu _menu;

        static Config()
        {
            _menu = MainMenu.AddMenu(MenuName, MenuName+"_Myrzin");
            _menu.AddGroupLabel("Myrzin's Tristana");

            Modes.Initialize();
            Drawing.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class Modes
        {
            private static readonly Menu _menu;

            static Modes()
            {
                _menu = Config._menu.AddSubMenu(MenuName);

                Combo.Initialize();
                _menu.AddSeparator();
                Harass.Initialize();
                _menu.AddSeparator();
                /*LaneClear.Initialize();
                _menu.AddSeparator();
                JungleClear.Initialize();
                _menu.AddSeparator();*/
                PermaActive.Initialize();
                _menu.AddSeparator();
            }

            public static void Initialize()
            {
            }

            public static class Combo
            {
                public const string GroupName = "Combo";

                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly Slider _useWMinHP;
                private static readonly Slider _useWMinMP;
                private static readonly CheckBox _useWKill;
                private static readonly Slider _useWMax;
                private static readonly CheckBox _useWTower;
                private static readonly CheckBox _useWStacks;
                private static readonly CheckBox _useE;
                private static readonly CheckBox _fullCombo;

                public static bool UseQ => _useQ.CurrentValue;

                public static bool UseW => _useW.CurrentValue;

                public static int UseWMinHealth => _useWMinHP.CurrentValue;

                public static int UseWMinMana => _useWMinMP.CurrentValue;

                public static bool UseWKill => _useWKill.CurrentValue;

                public static int UseWMax => _useWMax.CurrentValue;

                public static bool UseWTower => _useWTower.CurrentValue;

                public static bool UseWStacks => _useWStacks.CurrentValue;

                public static bool UseE => _useE.CurrentValue;

                public static bool FullCombo => _fullCombo.CurrentValue;


                static Combo()
                {
                    _menu.AddGroupLabel(GroupName);
                    _useQ = _menu.Add("combo_useQ", new CheckBox("Use Q"));
                    _useW = _menu.Add("comboUseW", new CheckBox("Use W", false));
                    _useWMinHP = _menu.Add("useWMinHP", new Slider("Min. Health % to use W", 30));
                    _useWMinMP = _menu.Add("useWminMP", new Slider("Min. Mana to use W", 190, 0, 500));
                    _useWKill = _menu.Add("comboWKill", new CheckBox("W Only if Killable"));
                    _useWMax = _menu.Add("useWMax", new Slider("Max Enemies to Use W", 2, 1, 5));
                    _useWTower = _menu.Add("useWTower", new CheckBox("Use W Under Tower", false));
                    _useWStacks = _menu.Add("useWStacks", new CheckBox("Use W If Target Have 4 Stacks"));
                    _useE = _menu.Add("comboUseE", new CheckBox("Use E"));
                    _fullCombo = _menu.Add("fullCombo",
                        new CheckBox("Use Full Combo if Killable? (recommended only for AP tristana"));
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

                public static bool UseQ => _useQ.CurrentValue;

                public static bool UseE => _useE.CurrentValue;

                public static int ManaUsage => _mana.CurrentValue;

                static Harass()
                {
                    _menu.AddGroupLabel(GroupName);

                    _useQ = _menu.Add("harass_useQ", new CheckBox("Use Q"));
                    _useE = _menu.Add("harassUseE", new CheckBox("Use E"));
                    _mana = _menu.Add("harassMana", new Slider("Mana Usage ", 60));

                }

                public static void Initialize()
                {
                }
            }

            /*public static class LaneClear
            {
                public const string GroupName = "LaneClear";

                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useE;
                private static readonly Slider _minE;
                private static readonly Slider _mana;

                public static bool UseQ => _useQ.CurrentValue;

                public static bool UseE => _useE.CurrentValue;

                public static int MinE => _minE.CurrentValue;

                public static int ManaUsage => _mana.CurrentValue;

                static LaneClear()
                {
                    _menu.AddGroupLabel(GroupName);

                    _useQ = _menu.Add("lane_useQ", new CheckBox("Use Q"));
                    _useE = _menu.Add("laneUseE", new CheckBox("Use E"));
                    _minE = _menu.Add("laneMinE", new Slider("Min Minions to Use E", 3, 1, 7));

                    _mana = _menu.Add("laneMana", new Slider("Mana usage in percent (%)", 60));
                }

                public static void Initialize()
                {
                }
            }

            public static class JungleClear
            {
                public const string GroupName = "JungleClear";

                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useE;
                private static readonly Slider _minE;
                private static readonly Slider _mana;

                public static bool UseQ => _useQ.CurrentValue;

                public static bool UseE => _useE.CurrentValue;

                public static int MinE => _minE.CurrentValue;

                public static int ManaUsage => _mana.CurrentValue;

                static JungleClear()
                {
                    _menu.AddGroupLabel(GroupName);

                    _useQ = _menu.Add("jungle_useQ", new CheckBox("Use Q"));
                    _useE = _menu.Add("jungleUseE", new CheckBox("Use E"));
                    _minE = _menu.Add("jungleMinE", new Slider("Minimum Monsters to Use E", 3, 1, 7));

                    _mana = _menu.Add("jungleMana", new Slider("Mana usage in percent (%)", 60));
                }

                public static void Initialize()
                {
                }
            }*/
        }

        public static class PermaActive
        {
            public const string MenuName = "PermaActive";

            private static readonly CheckBox _focusE;
            private static readonly CheckBox _wKs;
            private static readonly CheckBox _rKs;
            private static readonly CheckBox _erKs;
            private static readonly CheckBox _gapcloser;
            private static readonly CheckBox _interrupter;

            public static bool FocusE => _focusE.CurrentValue;

            public static bool WKs => _wKs.CurrentValue;

            public static bool RKs => _rKs.CurrentValue;

            public static bool ERKs => _erKs.CurrentValue;
            public static bool GapcloserR => _gapcloser.CurrentValue;
            public static bool InterruptR => _interrupter.CurrentValue;

            static PermaActive()
            {
                var menu = _menu.AddSubMenu(MenuName);

                _focusE = menu.Add("focusE", new CheckBox("Focus enemy carrying Bomb"));
                _wKs = menu.Add("wKS", new CheckBox("Ks With W?"));
                _rKs = menu.Add("permaRKs", new CheckBox("Ks With R"));
                _erKs = menu.Add("permaERKs", new CheckBox("Use R if R+E Damage Can Kill"));
                _gapcloser = menu.Add("miscGapcloseR", new CheckBox("Use R against gapclosers"));
                _interrupter = menu.Add("miscInterruptR", new CheckBox("Use R to interrupt dangerous spells"));

            }

            public static void Initialize()
            {
            }
        }
        
        public static class Drawing
        {
            public const string MenuName = "Drawing";

            private static readonly CheckBox _drawW;
            private static readonly CheckBox _drawEStacks;

            private static readonly CheckBox _healthbar;

            public static bool DrawW => _drawW.CurrentValue;
            public static bool DrawEStacks => _drawEStacks.CurrentValue;
            public static bool IndicatorHealthbar => _healthbar.CurrentValue;

            static Drawing()
            {
                // Initialize menu
                var menu = _menu.AddSubMenu(MenuName);

                menu.AddGroupLabel("Spell ranges");
                _drawW = menu.Add("drawW", new CheckBox("W range"));
                _drawEStacks = menu.Add("drawEStacks", new CheckBox("E Stacks"));

                menu.AddGroupLabel("Damage indicators");
                _healthbar = menu.Add("healthbar", new CheckBox("Healthbar overlay"));
            }

            public static void Initialize()
            {
            }
        }
    }
}