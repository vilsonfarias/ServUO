namespace Server.Items
{
    public class Excalibur : PaladinSword
    {
        public override int StrengthReq => 150;
        public override int MinDamage => 22;
        public override int MaxDamage => 26;
        public override float Speed => 2.0f;

        [Constructable]
        public Excalibur()
        {
            Hue = 0x482;

            Weight = 4.0;
            LootType = LootType.Blessed;

            Attributes.AttackChance = 70;
            Attributes.WeaponSpeed = 60;
            Attributes.WeaponDamage = 40;

            Attributes.SpellChanneling = 1;
            Attributes.Luck = 1200;

            //ExtendedWeaponAttributes.AssassinHoned = 1;
            //ExtendedWeaponAttributes.Bane = 1;
            //ExtendedWeaponAttributes.BoneBreaker = 1;
            //ExtendedWeaponAttributes.Focus = 1;

            //WeaponAttributes.BattleLust = 1;
            //WeaponAttributes.BloodDrinker = 1;
            //WeaponAttributes.HitFatigue = 70;

            //WeaponAttributes.HitMagicArrow = 70;
            //WeaponAttributes.HitFireball = 70;
            //WeaponAttributes.HitLightning = 70;

            //ExtendedWeaponAttributes.HitExplosion = 70;
            //ExtendedWeaponAttributes.HitSparks = 70;
            //ExtendedWeaponAttributes.HitSwarm = 70;

            //WeaponAttributes.HitHarm = 70;
            //WeaponAttributes.HitCurse = 50;
            //WeaponAttributes.HitDispel = 50;

            WeaponAttributes.HitLeechHits = 100;
            WeaponAttributes.HitLeechMana = 100;
            WeaponAttributes.HitLeechStam = 100;
            WeaponAttributes.HitManaDrain = 70;

            //WeaponAttributes.HitLowerAttack = 70;
            //WeaponAttributes.HitLowerDefend = 70;

            //WeaponAttributes.ReactiveParalyze = 1;
            //WeaponAttributes.SplinteringWeapon = 30;
        }

        public Excalibur(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override void GetDamageTypes(Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct)
        {
            phys = fire = pois = nrgy = chaos = cold = 15;
            direct = 10;
        }
    }
}