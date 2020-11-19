using System;
using Server;
using Server.Network;
using Server.Gumps;

namespace Server.Items
{
    public class Tools_ninjitsu_scroll : Item
    {
        public int mI01FocusAttack = 0;
        public int mI02DeathStrike = 0;
        public int mI03AnimalForm = 0;
        public int mI04KiAttack = 0;
        public int mI05SurpriseAttack = 0;
        public int mI06Backstab = 0;
        public int mI07Shadowjump = 0;
        public int mI08MirrorImage = 0;

        [CommandProperty(AccessLevel.GameMaster)]
        public int I01FocusAttack { get { return mI01FocusAttack; } set { mI01FocusAttack = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int I02DeathStrike { get { return mI02DeathStrike; } set { mI02DeathStrike = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int I03AnimalForm { get { return mI03AnimalForm; } set { mI03AnimalForm = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int I04KiAttack { get { return mI04KiAttack; } set { mI04KiAttack = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int I05SurpriseAttack { get { return mI05SurpriseAttack; } set { mI05SurpriseAttack = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int I06Backstab { get { return mI06Backstab; } set { mI06Backstab = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int I07Shadowjump { get { return mI07Shadowjump; } set { mI07Shadowjump = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int I08MirrorImage { get { return mI08MirrorImage; } set { mI08MirrorImage = value; } }

        [Constructable]
        public Tools_ninjitsu_scroll() : base(0x14F0)
        {
            LootType = LootType.Blessed;
            Hue = 0x16b;
            Name = "Ninjtsu Toolbar";
        }

        public Tools_ninjitsu_scroll(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1042001);
            }
            else
            {
                from.CloseGump(typeof(Tools_ninjitsu_scrollGump));
                from.SendGump(new Tools_ninjitsu_scrollGump(from, this));
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
            writer.Write(mI01FocusAttack);
            writer.Write(mI02DeathStrike);
            writer.Write(mI03AnimalForm);
            writer.Write(mI04KiAttack);
            writer.Write(mI05SurpriseAttack);
            writer.Write(mI06Backstab);
            writer.Write(mI07Shadowjump);
            writer.Write(mI08MirrorImage);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            mI01FocusAttack = reader.ReadInt();
            mI02DeathStrike = reader.ReadInt();
            mI03AnimalForm = reader.ReadInt();
            mI04KiAttack = reader.ReadInt();
            mI05SurpriseAttack = reader.ReadInt();
            mI06Backstab = reader.ReadInt();
            mI07Shadowjump = reader.ReadInt();
            mI08MirrorImage = reader.ReadInt();
        }
    }
}
