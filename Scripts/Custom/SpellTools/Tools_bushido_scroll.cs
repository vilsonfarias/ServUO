using System;
using Server;
using Server.Network;
using Server.Gumps;

namespace Server.Items
{
    public class Tools_bushido_scroll : Item
    {
        public int honorableExecution = 0;
        public int confidence = 0;
        public int counterAttack = 0;
        public int lightningStrike = 0;
        public int evasion = 0;
        public int momentumStrike = 0;

        [CommandProperty(AccessLevel.GameMaster)]
        public int B01HonorableExecution { get { return honorableExecution; } set { honorableExecution = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int BI02Confidence { get { return confidence; } set { confidence = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int BI03CounterAttack { get { return counterAttack; } set { counterAttack = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int BI04LightningStrike { get { return lightningStrike; } set { lightningStrike = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int BI05Evasion { get { return evasion; } set { evasion = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int BI06MomentumStrike { get { return momentumStrike; } set { momentumStrike = value; } }
        
        [Constructable]
        public Tools_bushido_scroll() : base(0x14F0)
        {
            LootType = LootType.Blessed;
            Hue = 0x58;
            Name = "Bushido Toolbar";
        }

        public Tools_bushido_scroll(Serial serial) : base(serial)
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
                from.CloseGump(typeof(Tools_bushido_scrollGump));
                from.SendGump(new Tools_bushido_scrollGump(from, this));
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
            writer.Write(honorableExecution);
            writer.Write(confidence);
            writer.Write(counterAttack);
            writer.Write(lightningStrike);
            writer.Write(evasion);
            writer.Write(momentumStrike);
    }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            honorableExecution = reader.ReadInt();
            confidence = reader.ReadInt();
            counterAttack = reader.ReadInt();
            lightningStrike = reader.ReadInt();
            evasion = reader.ReadInt();
            momentumStrike = reader.ReadInt();
        }
    }
}
