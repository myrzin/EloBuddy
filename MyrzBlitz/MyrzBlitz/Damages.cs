using EloBuddy;
using EloBuddy.SDK;

namespace MyrzBlitz
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

        public static float GetRealDamage(this Spell.SpellBase spell, Obj_AI_Base target)
        {
            return spell.Slot.GetRealDamage(target);
        }

        public static float GetRealDamage(this SpellSlot slot, Obj_AI_Base target)
        {
            // Helpers
            var spellLevel = Player.Instance.Spellbook.GetSpell(slot).Level;
            var damageType = DamageType.Magical;

            float damage = 0;

            // Validate spell level
            if (spellLevel == 0)
            {
                return 0;
            }
            spellLevel--;

            switch (slot)
            {
                case SpellSlot.Q:

                    damage = new float[] { 80, 135, 190, 245, 300 }[spellLevel] + 1f * Player.Instance.TotalMagicalDamage;
                    break;

                case SpellSlot.E:

                    damageType = DamageType.Physical;
                    damage = 1f * Player.Instance.TotalAttackDamage + 1f * (Player.Instance.TotalAttackDamage-Player.Instance.BaseAttackDamage);
                    break;

                case SpellSlot.R:

                    damage = new float[] { 250, 375, 500 }[spellLevel] + Player.Instance.TotalMagicalDamage;
                    break;
            }

            // No damage set
            if (damage <= 0)
            {
                return 0;
            }

            return Player.Instance.CalculateDamageOnUnit(target, damageType, damage);
        }
    }
}
