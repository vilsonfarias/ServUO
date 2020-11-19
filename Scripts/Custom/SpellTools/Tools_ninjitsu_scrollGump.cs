using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Network;
using Server.Spells;
using Server.Spells.Ninjitsu;
using Server.Prompts;

namespace Server.Gumps
{
    public class Tools_ninjitsu_scrollGump : Gump
    {
        private Tools_ninjitsu_scroll m_Scroll;

        public Tools_ninjitsu_scrollGump(Mobile from, Tools_ninjitsu_scroll scroll) : base(0, 0)
        {
            m_Scroll = scroll;

            int mI01FocusAttack = m_Scroll.I01FocusAttack;
            int mI02DeathStrike = m_Scroll.I02DeathStrike;
            int mI03AnimalForm = m_Scroll.I03AnimalForm;
            int mI04KiAttack = m_Scroll.I04KiAttack;
            int mI05SurpriseAttack = m_Scroll.I05SurpriseAttack;
            int mI06Backstab = m_Scroll.I06Backstab;
            int mI07Shadowjump = m_Scroll.I07Shadowjump;
            int mI08MirrorImage = m_Scroll.I08MirrorImage;

            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(52, 34, 160, 411, 9200);

            this.AddImage(60, 45, 21280);
            this.AddImage(60, 90, 21281);
            this.AddImage(60, 135, 21282);
            this.AddImage(60, 180, 21283);
            this.AddImage(60, 225, 21284);
            this.AddImage(60, 270, 21285);
            this.AddImage(60, 315, 21286);
            this.AddImage(60, 360, 21287);

            if (mI01FocusAttack == 1) { this.AddButton(110, 55, 2361, 2361, 1, GumpButtonType.Reply, 1); }
            if (mI02DeathStrike == 1) { this.AddButton(110, 100, 2361, 2361, 2, GumpButtonType.Reply, 1); }
            if (mI03AnimalForm == 1) { this.AddButton(110, 145, 2361, 2361, 3, GumpButtonType.Reply, 1); }
            if (mI04KiAttack == 1) { this.AddButton(110, 190, 2361, 2361, 4, GumpButtonType.Reply, 1); }
            if (mI05SurpriseAttack == 1) { this.AddButton(110, 235, 2361, 2361, 5, GumpButtonType.Reply, 1); }
            if (mI06Backstab == 1) { this.AddButton(110, 280, 2361, 2361, 6, GumpButtonType.Reply, 1); }
            if (mI07Shadowjump == 1) { this.AddButton(110, 325, 2361, 2361, 7, GumpButtonType.Reply, 1); }
            if (mI08MirrorImage == 1) { this.AddButton(110, 370, 2361, 2361, 8, GumpButtonType.Reply, 1); }

            if (mI01FocusAttack == 0) { this.AddButton(110, 55, 2360, 2360, 1, GumpButtonType.Reply, 1); }
            if (mI02DeathStrike == 0) { this.AddButton(110, 100, 2360, 2360, 2, GumpButtonType.Reply, 1); }
            if (mI03AnimalForm == 0) { this.AddButton(110, 145, 2360, 2360, 3, GumpButtonType.Reply, 1); }
            if (mI04KiAttack == 0) { this.AddButton(110, 190, 2360, 2360, 4, GumpButtonType.Reply, 1); }
            if (mI05SurpriseAttack == 0) { this.AddButton(110, 235, 2360, 2360, 5, GumpButtonType.Reply, 1); }
            if (mI06Backstab == 0) { this.AddButton(110, 280, 2360, 2360, 6, GumpButtonType.Reply, 1); }
            if (mI07Shadowjump == 0) { this.AddButton(110, 325, 2360, 2360, 7, GumpButtonType.Reply, 1); }
            if (mI08MirrorImage == 0) { this.AddButton(110, 370, 2360, 2360, 8, GumpButtonType.Reply, 1); }

            this.AddButton(149, 408, 2152, 2152, 9, GumpButtonType.Reply, 1); // TOOLBAR
            this.AddLabel(60, 412, 52, @"Open Toolbar");
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            switch (info.ButtonID)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        m_Scroll.I01FocusAttack = m_Scroll.I01FocusAttack == 0 ? 1 : 0;
                        from.SendGump(new Tools_ninjitsu_scrollGump(from, m_Scroll)); break;
                    }
                case 2:
                    {
                        m_Scroll.I02DeathStrike = m_Scroll.I02DeathStrike == 0 ? 1 : 0;
                        from.SendGump(new Tools_ninjitsu_scrollGump(from, m_Scroll)); break;
                    }
                case 3:
                    {
                        m_Scroll.I03AnimalForm = m_Scroll.I03AnimalForm == 0 ? 1 : 0;
                        from.SendGump(new Tools_ninjitsu_scrollGump(from, m_Scroll)); break;
                    }
                case 4:
                    {
                        m_Scroll.I04KiAttack = m_Scroll.I04KiAttack == 0 ? 1 : 0;
                        from.SendGump(new Tools_ninjitsu_scrollGump(from, m_Scroll)); break;
                    }
                case 5:
                    {
                        m_Scroll.I05SurpriseAttack = m_Scroll.I05SurpriseAttack == 0 ? 1 : 0;
                        from.SendGump(new Tools_ninjitsu_scrollGump(from, m_Scroll)); break;
                    }
                case 6:
                    {
                        m_Scroll.I06Backstab = m_Scroll.I06Backstab == 0 ? 1 : 0;
                        from.SendGump(new Tools_ninjitsu_scrollGump(from, m_Scroll)); break;
                    }
                case 7:
                    {
                        m_Scroll.I07Shadowjump = m_Scroll.I07Shadowjump == 0 ? 1 : 0;
                        from.SendGump(new Tools_ninjitsu_scrollGump(from, m_Scroll)); break;
                    }
                case 8:
                    {
                        m_Scroll.I08MirrorImage = m_Scroll.I08MirrorImage == 0 ? 1 : 0;
                        from.SendGump(new Tools_ninjitsu_scrollGump(from, m_Scroll)); break;
                    }
                case 9:
                    {
                        from.CloseGump(typeof(Tools_tools_ninjitsu));
                        from.SendGump(new Tools_tools_ninjitsu(from, m_Scroll));
                        break;
                    }
            }
        }
    }

    public class Tools_tools_ninjitsu : Gump
    {
        public static bool HasSpell(Mobile from, int spellID)
        {
            Spellbook book = Spellbook.Find(from, spellID);
            return (book != null && book.HasSpell(spellID));
        }

        private Tools_ninjitsu_scroll m_Scroll;

        public Tools_tools_ninjitsu(Mobile from, Tools_ninjitsu_scroll scroll) : base(0, 0)
        {
            m_Scroll = scroll;
            this.Closable = false;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddImage(0, 0, 11016, 1149);
            int dby = 50;

            if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(FocusAttack))) && m_Scroll.I01FocusAttack == 1)
            {
                this.AddButton(dby, 5, 21280, 21280, 1, GumpButtonType.Reply, 1);
                dby = dby + 45;
            }
            if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(DeathStrike))) && m_Scroll.I02DeathStrike == 1)
            {
                this.AddButton(dby, 5, 21281, 21281, 2, GumpButtonType.Reply, 1);
                dby = dby + 45;
            }
            if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(AnimalForm))) && m_Scroll.I03AnimalForm == 1)
            {
                this.AddButton(dby, 5, 21282, 21282, 3, GumpButtonType.Reply, 1);
                dby = dby + 45;
            }
            if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(KiAttack))) && m_Scroll.I04KiAttack == 1)
            {
                this.AddButton(dby, 5, 21283, 21283, 4, GumpButtonType.Reply, 1);
                dby = dby + 45;
            }
            if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(SurpriseAttack))) && m_Scroll.I05SurpriseAttack == 1)
            {
                this.AddButton(dby, 5, 21284, 21284, 5, GumpButtonType.Reply, 1);
                dby = dby + 45;
            }
            if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(Backstab))) && m_Scroll.I06Backstab == 1)
            {
                this.AddButton(dby, 5, 21285, 21285, 6, GumpButtonType.Reply, 1);
                dby = dby + 45;
            }
            if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(Shadowjump))) && m_Scroll.I07Shadowjump == 1)
            {
                this.AddButton(dby, 5, 21286, 21286, 7, GumpButtonType.Reply, 1);
                dby = dby + 45;
            }
            if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(MirrorImage))) && m_Scroll.I08MirrorImage == 1)
            {
                this.AddButton(dby, 5, 21287, 21287, 8, GumpButtonType.Reply, 1);
                dby = dby + 45;
            }
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;
            switch (info.ButtonID)
            {
                case 0: { break; }
                case 1:
                    {
                        if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(FocusAttack))))
                        {
                            new FocusAttack();
                            from.SendGump(new Tools_tools_ninjitsu(from, m_Scroll));
                        }
                        break;
                    }
                case 2:
                    {
                        if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(DeathStrike))))
                        {
                            new DeathStrike(); from.SendGump(new Tools_tools_ninjitsu(from, m_Scroll));
                        }
                        break;
                    }
                case 3:
                    {
                        if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(AnimalForm))))
                        {
                            new AnimalForm(from, null).Cast();
                            from.SendGump(new Tools_tools_ninjitsu(from, m_Scroll));
                        }
                        break;
                    }
                case 4:
                    {
                        if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(KiAttack))))
                        {
                            new KiAttack();
                            from.SendGump(new Tools_tools_ninjitsu(from, m_Scroll));
                        }
                        break;
                    }
                case 5:
                    {
                        if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(SurpriseAttack))))
                        {
                            new SurpriseAttack(); from.SendGump(new Tools_tools_ninjitsu(from, m_Scroll));
                        }
                        break;
                    }
                case 6:
                    {
                        if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(Backstab))))
                        {
                            new Backstab();
                            from.SendGump(new Tools_tools_ninjitsu(from, m_Scroll));
                        }
                        break;
                    }
                case 7:
                    {
                        if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(Shadowjump))))
                        {
                            new Shadowjump(from, null).Cast();
                            from.SendGump(new Tools_tools_ninjitsu(from, m_Scroll));
                        }
                        break;
                    }
                case 8:
                    {
                        if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(MirrorImage))))
                        {
                            new MirrorImage(from, null).Cast();
                            from.SendGump(new Tools_tools_ninjitsu(from, m_Scroll));
                        }
                        break;
                    }
            }
        }
    }
}
