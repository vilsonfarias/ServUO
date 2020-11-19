using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Network;
using Server.Spells;
using Server.Spells.Bushido;
using Server.Prompts;

namespace Server.Gumps
{
    public class Tools_bushido_scrollGump : Gump
    {
        private Tools_bushido_scroll m_Scroll;

        public Tools_bushido_scrollGump(Mobile from, Tools_bushido_scroll scroll) : base(0, 0)
        {
            m_Scroll = scroll;

            int honorableExecution = m_Scroll.B01HonorableExecution;
            int confidence = m_Scroll.BI02Confidence;
            int counterAttack = m_Scroll.BI03CounterAttack;
            int lightningStrike = m_Scroll.BI04LightningStrike;
            int evasion = m_Scroll.BI05Evasion;
            int momentumStrike = m_Scroll.BI06MomentumStrike;

            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(52, 34, 160, 411, 9200);

            this.AddImage(60, 45, 21536);
            this.AddImage(60, 90, 21537);
            this.AddImage(60, 135, 21538);
            this.AddImage(60, 180, 21539);
            this.AddImage(60, 225, 21540);
            this.AddImage(60, 270, 21541);

            if (honorableExecution == 1)
            {
                this.AddButton(110, 55, 2361, 2361, 1, GumpButtonType.Reply, 1);
            }
            if (confidence == 1)
            {
                this.AddButton(110, 100, 2361, 2361, 2, GumpButtonType.Reply, 1);
            }
            if (counterAttack == 1)
            {
                this.AddButton(110, 145, 2361, 2361, 3, GumpButtonType.Reply, 1);
            }
            if (lightningStrike == 1)
            {
                this.AddButton(110, 190, 2361, 2361, 4, GumpButtonType.Reply, 1);
            }
            if (evasion == 1)
            {
                this.AddButton(110, 235, 2361, 2361, 5, GumpButtonType.Reply, 1);
            }
            if (momentumStrike == 1)
            {
                this.AddButton(110, 280, 2361, 2361, 6, GumpButtonType.Reply, 1);
            }

            if (honorableExecution == 0)
            {
                this.AddButton(110, 55, 2360, 2360, 1, GumpButtonType.Reply, 1);
            }
            if (confidence == 0)
            {
                this.AddButton(110, 100, 2360, 2360, 2, GumpButtonType.Reply, 1);
            }
            if (counterAttack == 0)
            {
                this.AddButton(110, 145, 2360, 2360, 3, GumpButtonType.Reply, 1);
            }
            if (lightningStrike == 0)
            {
                this.AddButton(110, 190, 2360, 2360, 4, GumpButtonType.Reply, 1);
            }
            if (evasion == 0)
            {
                this.AddButton(110, 235, 2360, 2360, 5, GumpButtonType.Reply, 1);
            }
            if (momentumStrike == 0)
            {
                this.AddButton(110, 280, 2360, 2360, 6, GumpButtonType.Reply, 1);
            }

            this.AddButton(149, 408, 2152, 2152, 7, GumpButtonType.Reply, 1); // TOOLBAR
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
                        m_Scroll.B01HonorableExecution = m_Scroll.B01HonorableExecution == 0 ? 1 : 0;
                        from.SendGump(new Tools_bushido_scrollGump(from, m_Scroll)); break;
                    }
                case 2:
                    {
                        m_Scroll.BI02Confidence = m_Scroll.BI02Confidence == 0 ? 1 : 0;
                        from.SendGump(new Tools_bushido_scrollGump(from, m_Scroll)); break;
                    }
                case 3:
                    {
                        m_Scroll.BI03CounterAttack = m_Scroll.BI03CounterAttack == 0 ? 1 : 0;
                        from.SendGump(new Tools_bushido_scrollGump(from, m_Scroll)); break;
                    }
                case 4:
                    {
                        m_Scroll.BI04LightningStrike = m_Scroll.BI04LightningStrike == 0 ? 1 : 0;
                        from.SendGump(new Tools_bushido_scrollGump(from, m_Scroll)); break;
                    }
                case 5:
                    {
                        m_Scroll.BI05Evasion = m_Scroll.BI05Evasion == 0 ? 1 : 0;
                        from.SendGump(new Tools_bushido_scrollGump(from, m_Scroll)); break;
                    }
                case 6:
                    {
                        m_Scroll.BI06MomentumStrike = m_Scroll.BI06MomentumStrike == 0 ? 1 : 0;
                        from.SendGump(new Tools_bushido_scrollGump(from, m_Scroll)); break;
                    }
                case 7:
                    {
                        from.CloseGump(typeof(Tools_tools_bushido));
                        from.SendGump(new Tools_tools_bushido(from, m_Scroll));
                        break;
                    }
            }
        }
    }

    public class Tools_tools_bushido : Gump
    {
        public static bool HasSpell(Mobile from, int spellID)
        {
            Spellbook book = Spellbook.Find(from, spellID);
            return (book != null && book.HasSpell(spellID));
        }

        private Tools_bushido_scroll m_Scroll;

        public Tools_tools_bushido(Mobile from, Tools_bushido_scroll scroll) : base(0, 0)
        {
            m_Scroll = scroll;
            this.Closable = false;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddImage(0, 0, 11017, 1149);
            int dby = 50;

            if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(HonorableExecution))) && m_Scroll.B01HonorableExecution == 1)
            {
                this.AddButton(dby, 5, 21536, 21536, 1, GumpButtonType.Reply, 1); dby = dby + 45;
            }
            if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(Confidence))) && m_Scroll.BI02Confidence == 1)
            {
                this.AddButton(dby, 5, 21537, 21537, 2, GumpButtonType.Reply, 1); dby = dby + 45;
            }
            if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(CounterAttack))) && m_Scroll.BI03CounterAttack == 1)
            {
                this.AddButton(dby, 5, 21538, 21538, 3, GumpButtonType.Reply, 1); dby = dby + 45;
            }
            if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(LightningStrike))) && m_Scroll.BI04LightningStrike == 1)
            {
                this.AddButton(dby, 5, 21539, 21539, 4, GumpButtonType.Reply, 1); dby = dby + 45;
            }
            if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(Evasion))) && m_Scroll.BI05Evasion == 1)
            {
                this.AddButton(dby, 5, 21540, 21540, 5, GumpButtonType.Reply, 1); dby = dby + 45;
            }
            if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(MomentumStrike))) && m_Scroll.BI06MomentumStrike == 1)
            {
                this.AddButton(dby, 5, 21541, 21541, 6, GumpButtonType.Reply, 1); dby = dby + 45;
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
                        if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(HonorableExecution))))
                        {
                            new HonorableExecution();
                            from.SendGump(new Tools_tools_bushido(from, m_Scroll));
                        }
                        break;
                    }
                case 2:
                    {
                        if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(Confidence))))
                        {
                            new Confidence(from, null);
                            from.SendGump(new Tools_tools_bushido(from, m_Scroll));
                        }
                        break;
                    }
                case 3:
                    {
                        if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(CounterAttack))))
                        {
                            new CounterAttack(from, null).Cast();
                            from.SendGump(new Tools_tools_bushido(from, m_Scroll));
                        }
                        break;
                    }
                case 4:
                    {
                        if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(LightningStrike))))
                        {
                            new LightningStrike();
                            from.SendGump(new Tools_tools_bushido(from, m_Scroll));
                        }
                        break;
                    }
                case 5:
                    {
                        if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(Evasion))))
                        {
                            new Evasion(from, null);
                            from.SendGump(new Tools_tools_bushido(from, m_Scroll));
                        }
                        break;
                    }
                case 6:
                    {
                        if (HasSpell(from, SpellRegistry.GetRegistryNumber(typeof(MomentumStrike))))
                        {
                            new MomentumStrike();
                            from.SendGump(new Tools_tools_bushido(from, m_Scroll));
                        }
                        break;
                    }
            }
        }
    }
}
