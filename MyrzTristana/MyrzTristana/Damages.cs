using EloBuddy;
using EloBuddy.SDK;

namespace MyrzTristana
{
    public static class Damages
    {
        public static float GetTotalDamage(AIHeroClient target)
        {
            // Auto attack
            var damage = Player.Instance.GetAutoAttackDamage(target);

            // Q
            if (SpellManager.Q.IsReady())
            {
                damage += SpellManager.Q.GetRealDamage(target);
            }

            // W
            if (SpellManager.W.IsReady())
            {
                damage += SpellManager.W.GetRealDamage(target);
            }

            // E
            if (SpellManager.E.IsReady())
            {
                damage += SpellManager.E.GetRealDamage(target);
            }

            // R
            if (SpellManager.R.IsReady())
            {
                damage += SpellManager.R.GetRealDamage(target);
            }

            return damage;
        }

        public static int GetStacks(Obj_AI_Base target)
        {
            var totalstacks = 0;
            var stacks1 = target.GetBuffCount("TristanaECharge");
            var stacks2 = target.GetBuffCount("TristanaEChargeSound");

            if (stacks1 == -1 && stacks2 == -1)
            {
                totalstacks = 0;
            }
            if (stacks1 == -1 && stacks2 == 1)
            {
                totalstacks = 1;
            }
            if (stacks1 == 1 && stacks2 == 1)
            {
                totalstacks = 2;
            }
            if (stacks1 == 2 && stacks2 == 1)
            {
                totalstacks = 3;
            }
            if (stacks1 == 3 && stacks2 == 1)
            {
                totalstacks = 4;
            }

            return totalstacks;
        }

        public static float GetRealDamage(this Spell.SpellBase spell, Obj_AI_Base target)
        {
            return spell.Slot.GetRealDamage(target);
        }

        public static float GetEpDamage(Obj_AI_Base unit)
        {
            var e = SpellManager.E;
            var p = Player.Instance;
            var s = GetStacks(unit);
            var damage = ((new[] {0f, 60f, 70f, 80f, 90f, 100f}[e.Level]) +
                          (new[] {0f, 0.5f, 0.65f, 0.8f, 0.95f, 1.1f}[e.Level]*p.FlatPhysicalDamageMod) +
                          (0.5f*p.TotalMagicalDamage)) +
                         ((new[] {0f, 18f, 21f, 24f, 27f, 30f}[e.Level]*s) +
                          (new[] {0f, 0.15f, 0.195f, 0.24f, 0.285f, 0.33f}[e.Level]*s*p.FlatPhysicalDamageMod) +
                          (s*0.15*p.TotalMagicalDamage));
            return (float) damage;
        }

        public static float GetEmDamage(Obj_AI_Base unit)
        {
            var e = SpellManager.E;
            var p = Player.Instance;
            var damage = new[] {0,50,75,100,125,150}[e.Level]+0.25*p.TotalMagicalDamage;
            return (float) damage;
        }

        public static float GetRealDamage(this SpellSlot slot, Obj_AI_Base target)
        {
            var spellLevel = Player.Instance.Spellbook.GetSpell(slot).Level;
            float damage = 0;
            var damageType = DamageType.Magical;

            if (spellLevel == 0)
            {
                return 0;
            }
            spellLevel--;

            switch (slot)
            {
                case SpellSlot.W:

                    damage = new float[] {60, 110, 160, 210, 260}[spellLevel] + 0.5f*Player.Instance.TotalMagicalDamage;
                    break;

                case SpellSlot.E:
                    damageType = DamageType.Mixed;
                    damage = GetEmDamage(target) + GetEpDamage(target);
                    break;

                case SpellSlot.R:

                    damage = new float[] {300, 400, 500}[spellLevel] + Player.Instance.TotalMagicalDamage;
                    break;
            }

            if (damage <= 0)
            {
                return 0;
            }

            return Player.Instance.CalculateDamageOnUnit(target, damageType, damage) - 20;
        }
    }
}
