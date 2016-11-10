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

        public static float GetWDamage(Obj_AI_Base unit)
        {
            var e = SpellManager.W;
            var p = Player.Instance;
            var damage = new[] { 0, 60, 110, 160, 210, 260 }[e.Level] + 0.5 * p.TotalMagicalDamage;
            return (float)damage;
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

                    damage = new float[] {60, 110, 160, 210, 260}[spellLevel] + 0.5f*Player.Instance.FlatMagicDamageMod;
                    break;

                case SpellSlot.E:
                    damageType = DamageType.Physical;
                    var baseDamage = new float[] {60, 70, 80, 90, 100}[spellLevel];
                    var baseBonusAdDamage = new [] {0.5f, 0.65f, 0.80f, 0.95f, 1.1f}[spellLevel];
                    const float baseBonusApDamage = 0.5f;
                    var stackDamage = new float[] { 18, 21, 24, 27, 30 }[spellLevel];
                    var stackAdDamage = new[] { 0.15f, 0.195f, 0.24f, 0.285f, 0.30f }[spellLevel];
                    const float stackApDamage = 0.15f;

                    damage = baseDamage + (baseBonusAdDamage*Player.Instance.FlatPhysicalDamageMod) +
                             (baseBonusApDamage*Player.Instance.FlatMagicDamageMod) +
                             (stackDamage*(GetStacks(target) - 1)) +
                             (stackAdDamage*Player.Instance.FlatPhysicalDamageMod*(GetStacks(target) - 1)) +
                             (stackApDamage*Player.Instance.FlatMagicDamageMod*(GetStacks(target) - 1));
                    break;

                case SpellSlot.R:

                    damage = new float[] {300, 400, 500}[spellLevel] + Player.Instance.TotalMagicalDamage;
                    break;
            }

            if (damage <= 0)
            {
                return 0;
            }

            return Player.Instance.CalculateDamageOnUnit(target, damageType, damage);
        }
    }
}
