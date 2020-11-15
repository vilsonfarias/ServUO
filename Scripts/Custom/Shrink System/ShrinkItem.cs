#region AuthorHeader
//
//	Shrink System version 2.1, by Xanthos
//
//
#endregion AuthorHeader
using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;
using Xanthos.Utilities;
using Xanthos.Interfaces;
using Server.Regions;

namespace Xanthos.ShrinkSystem
{
	public class ShrinkItem : Item, IShrinkItem
	{
		// Persisted
		private bool m_IsStatuette;
		private bool m_Locked;
		private Mobile m_Owner;
		private BaseCreature m_Pet;

		// Not persisted; lazy loaded.
		private bool m_PropsLoaded;
		private string m_Breed;
		private string m_Gender;
		private bool m_IsBonded;
		private string m_Name;
		private int m_RawStr;
		private int m_RawDex;
		private int m_RawInt;
        private double m_Wrestling;
		private double m_Tactics;
		private double m_Anatomy;
		private double m_Poisoning;
		private double m_Magery;
		private double m_EvalInt;
		private double m_MagicResist;
		private double m_Meditation;
		private double m_Archery;
		private double m_Fencing;
		private double m_Macing;
		private double m_Swords;
		private double m_Parry;
        private double max_Wrestling;
        private double max_Tactics;
        private double max_Anatomy;
        private double max_Poisoning;
        private double max_Magery;
        private double max_EvalInt;
        private double max_MagicResist;
        private double max_Meditation;
        private double max_Archery;
        private double max_Fencing;
        private double max_Macing;
        private double max_Swords;
        private double max_Parry;
        private int m_EvoEp;
		private int m_EvoStage;

		private bool m_IgnoreLockDown;	// Is only ever changed by staff

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsStatuette
		{
			get { return m_IsStatuette; }
			set
			{
				if ( null == ShrunkenPet )
				{
					ItemID = 0xFAA;
					Name = "unlinked shrink item!";
				}
				else if ( m_IsStatuette = value )
				{
					ItemID = ShrinkTable.Lookup( m_Pet );
					Name = "a shrunken pet";
				}
				else
				{
					ItemID = 0x14EF;
					Name = "a pet deed";
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IgnoreLockDown
		{
			get { return m_IgnoreLockDown; }
			set { m_IgnoreLockDown = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Locked
		{
			get { return m_Locked; }
			set { m_Locked = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get { return m_Owner; }
			set { m_Owner = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public BaseCreature ShrunkenPet
		{
			get { return m_Pet; }
			set { m_Pet = value; InvalidateProperties(); }
		}

		public ShrinkItem() : base()
		{
		}

		public ShrinkItem( Serial serial ) : base( serial )
		{
		}

		public ShrinkItem( BaseCreature pet ) : this()
		{
			ShrinkPet( pet );
			IsStatuette = ShrinkConfig.PetAsStatuette;
			m_IgnoreLockDown = false; // This is only used to allow GMs to bypass the lockdown, one pet at a time.
			Weight = ShrinkConfig.ShrunkenWeight;

			if ( !(m_Pet is IEvoCreature) || ((IEvoCreature)m_Pet).CanHue )
				Hue = m_Pet.Hue;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !m_PropsLoaded )
				PreloadProperties();

			if ( !IsChildOf( from.Backpack ) )
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.

			else if ( m_Pet == null || m_Pet.Deleted || ItemID == 0xFAA )
				from.SendMessage( "Due to unforseen circumstances your pet is lost forever." );

			else if ( m_Locked && m_Owner != from )
			{
				from.SendMessage( "This is locked and only the owner can claim this pet while locked." );
				from.SendMessage( "This item is now being returned to its owner." );
				m_Owner.AddToBackpack( this );
				m_Owner.SendMessage( "Your pet {0} has been returned to you because it was locked and {1} was trying to claim it.", m_Breed, from.Name );
			}
			else if ( from.Followers + m_Pet.ControlSlots > from.FollowersMax )
				from.SendMessage( "You have to many followers to claim this pet." );

			else if ( Server.Spells.SpellHelper.CheckCombat( from ) )
				from.SendMessage( "You cannot reclaim your pet while your fighting." );

			else if ( ShrinkCommands.LockDown == true && !m_IgnoreLockDown )
				from.SendMessage( 54, "The server is on a shrinkitem lockdown. You cannot unshrink your pet at this time." );

			else if ( !m_Pet.CanBeControlledBy( from ))
				from.SendMessage( "You do not have the required skills to control this pet.");

			else
				UnshrinkPet( from );
		}

		private void ShrinkPet( BaseCreature pet )
		{
			m_Pet = pet;
			m_Owner = pet.ControlMaster;
				
			if ( ShrinkConfig.LootStatus == ShrinkConfig.BlessStatus.All
				|| ( m_Pet.IsBonded && ShrinkConfig.LootStatus == ShrinkConfig.BlessStatus.BondedOnly ))
				LootType = LootType.Blessed;
			else
				LootType = LootType.Regular;

			m_Pet.Internalize();
			m_Pet.SetControlMaster( null );
			m_Pet.ControlOrder = OrderType.Stay;
			m_Pet.SummonMaster = null;
			m_Pet.IsStabled = true;

			if ( pet is IEvoCreature )
				((IEvoCreature)m_Pet).OnShrink( this );
		}

		private void UnshrinkPet( Mobile from )
		{
			m_Pet.SetControlMaster( from );
			m_Pet.IsStabled = false;
			m_Pet.MoveToWorld( from.Location, from.Map );
			if ( from != m_Owner )
				m_Pet.IsBonded = false;

			m_Pet = null;
			this.Delete();
		}

		// Summoning ball was used so dispose of the shrink item
		public void OnPetSummoned()
		{
			m_Pet = null;
			Delete();
		}

		public override void Delete()
		{
			if ( m_Pet != null )	// Don't orphan pets on the internal map
				m_Pet.Delete();

			base.Delete();
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			if (( ShrinkConfig.AllowLocking || m_Locked == true ) && from.Alive && m_Owner == from )
			{
				if ( m_Locked == false )
					list.Add( new LockShrinkItem( from, this ) );
				else
					list.Add( new UnLockShrinkItem( from, this ) );
			}
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );

			if ( null == m_Pet || m_Pet.Deleted )
				return;

			if ( !m_PropsLoaded )
				PreloadProperties();

			if ( m_IsBonded && ShrinkConfig.BlessStatus.None == ShrinkConfig.LootStatus )	// Only show bonded when the item is not blessed
				list.Add( 1049608 );

			if ( ShrinkConfig.AllowLocking || m_Locked )	// Only show lock status when locking enabled or already locked
				list.Add( 1049644, ( m_Locked == true ) ? "Locked" : "Unlocked" );
		
			if ( ShrinkConfig.ShowPetDetails )
			{
				list.Add( 1060663, "Name\t{0} Breed: {1} Gender: {2}", m_Name, m_Breed, m_Gender );
				list.Add( 1061640, ( null == m_Owner ) ? "nobody (WILD)" : m_Owner.Name ); // Owner: ~1_OWNER~
                list.Add(1060659, "Stats\tStrength {0}, Dexterity {1}, Intelligence {2}", m_RawStr, m_RawDex, m_RawInt);

				addCombatSkills(list);
				addMagicSkills(list);
				addWeaponSkills(list);

				if ( m_EvoEp > 0 )
					list.Add( 1060662, "EP\t{0}, Stage: {1}", m_EvoEp, m_EvoStage + 1 );
			}
			else
				list.Add( 1060663, "Name\t{0}", m_Name );
		}

		private void addCombatSkills(ObjectPropertyList list)
        {
			string combat = "";
			if (m_Anatomy > 0)
			{
				combat += "Anatomy " + m_Anatomy + "/" + max_Anatomy + ", ";
			}
			if (m_Tactics > 0)
			{
				combat += "Tactics " + m_Tactics + "/" + max_Tactics + ", ";
            }
			if (m_Wrestling > 0)
			{
				combat += "Wrestling " + m_Wrestling + "/" + max_Wrestling + ", ";
            }
            if (m_Parry > 0)
            {
                combat += "Parry " + m_Parry + "/" + max_Parry + ", ";
            }
            if (m_Poisoning > 0)
			{
				combat += "Poisoning " + m_Poisoning + "/" + max_Poisoning + ", ";
            }
			if (combat.Length > 0)
			{
				list.Add(1060660, "Combat\t{0}", combat.Remove(combat.Length - 2));
			}
		}

		private void addMagicSkills(ObjectPropertyList list)
		{
			string magic = "";
			if (m_Magery > 0)
			{
				magic += "Magery " + m_Magery + "/" + max_Magery + ", ";
            }
			if (m_EvalInt > 0)
			{
				magic += "Eval Int " + m_EvalInt + "/" + max_EvalInt + ", ";
            }
			if (m_MagicResist > 0)
			{
				magic += "Magic Resist " + m_MagicResist + "/" + max_MagicResist + ", ";
            }
			if (m_Meditation > 0)
			{
				magic += "Meditation " + m_Meditation + "/" + max_Meditation + ", ";
            }
			if (magic.Length > 0)
			{
				list.Add(1060661, "Magic\t{0}", magic.Remove(magic.Length - 2));
			}
		}

		private void addWeaponSkills(ObjectPropertyList list)
		{
			string weapon = "";
			if (m_Swords > 0)
			{
				weapon += "Swords " + m_Swords + "/" + max_Swords + ", ";
            }
			if (m_Fencing > 0)
			{
				weapon += "Fencing " + m_Fencing + "/" + max_Fencing + ", ";
            }
			if (m_Macing > 0)
			{
				weapon += "Macing " + m_Macing + "/" + max_Macing + ", ";
            }
			if (m_Archery > 0)
			{
				weapon += "Archery " + m_Archery + "/" + max_Archery + ", ";
            }
			if (weapon.Length > 0)
			{
				list.Add(1060662, "Weapon\t{0}", weapon.Remove(weapon.Length - 2));
			}
		}

		private void PreloadProperties()
		{
			if ( null == m_Pet )
				return;

			m_IsBonded = m_Pet.IsBonded;
			m_Name = m_Pet.Name;
			
			m_Gender = (m_Pet.Female ? "Female" : "Male");
			m_Breed = Xanthos.Utilities.Misc.GetFriendlyClassName( m_Pet.GetType().Name );
			m_RawStr = m_Pet.RawStr;
			m_RawDex = m_Pet.RawDex;
			m_RawInt = m_Pet.RawInt;

            m_Wrestling = m_Pet.Skills[SkillName.Wrestling].Base;
            m_Tactics = m_Pet.Skills[SkillName.Tactics].Base;
            m_Anatomy = m_Pet.Skills[SkillName.Anatomy].Base;
			m_Poisoning = m_Pet.Skills[SkillName.Poisoning].Base;
			m_Magery = m_Pet.Skills[SkillName.Magery].Base;
			m_EvalInt = m_Pet.Skills[SkillName.EvalInt].Base;
			m_MagicResist = m_Pet.Skills[SkillName.MagicResist].Base;
			m_Meditation = m_Pet.Skills[SkillName.Meditation].Base;
			m_Parry = m_Pet.Skills[SkillName.Parry].Base;
			m_Archery = m_Pet.Skills[SkillName.Archery].Base;
			m_Fencing = m_Pet.Skills[SkillName.Fencing].Base;
			m_Swords = m_Pet.Skills[SkillName.Swords].Base;
			m_Macing = m_Pet.Skills[SkillName.Macing].Base;

            max_Wrestling = m_Pet.Skills[SkillName.Wrestling].Cap;
            max_Tactics = m_Pet.Skills[SkillName.Tactics].Cap;
            max_Anatomy = m_Pet.Skills[SkillName.Anatomy].Cap;
            max_Poisoning = m_Pet.Skills[SkillName.Poisoning].Cap;
            max_Magery = m_Pet.Skills[SkillName.Magery].Cap;
            max_EvalInt = m_Pet.Skills[SkillName.EvalInt].Cap;
            max_MagicResist = m_Pet.Skills[SkillName.MagicResist].Cap;
            max_Meditation = m_Pet.Skills[SkillName.Meditation].Cap;
            max_Parry = m_Pet.Skills[SkillName.Parry].Cap;
            max_Archery = m_Pet.Skills[SkillName.Archery].Cap;
            max_Fencing = m_Pet.Skills[SkillName.Fencing].Cap;
            max_Swords = m_Pet.Skills[SkillName.Swords].Cap;
            max_Macing = m_Pet.Skills[SkillName.Macing].Cap;

            IEvoCreature evo = m_Pet as IEvoCreature;

			if ( null != evo )
			{
				m_EvoEp = evo.Ep;
				m_EvoStage = evo.Stage;
			}

			m_PropsLoaded = true;
		}

		public static bool IsPackAnimal( BaseCreature pet )
		{
			if ( null == pet || pet.Deleted )
				return false;

			Type breed = pet.GetType();

			foreach ( Type packBreed in ShrinkConfig.PackAnimals )
				if ( packBreed == breed )
					return true;
	
			return false;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
			writer.Write( m_IsStatuette );
			writer.Write( m_Locked );
			writer.Write( (Mobile)m_Owner );
			writer.Write( (Mobile)m_Pet );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			switch ( reader.ReadInt() )
			{
				case 0:
				{
					m_IsStatuette = reader.ReadBool();
					m_Locked = reader.ReadBool();
					m_Owner = (PlayerMobile)reader.ReadMobile();
					m_Pet = (BaseCreature)reader.ReadMobile();

					if (null != m_Pet )
						m_Pet.IsStabled = true;

					break;
				}
			}
		}
	}

	public class LockShrinkItem : ContextMenuEntry
	{
		private Mobile m_From;
		private ShrinkItem m_ShrinkItem;

		public LockShrinkItem( Mobile from, ShrinkItem shrink ) : base( 2029, 5 )
		{
			m_From = from;
			m_ShrinkItem = shrink;
		}

		public override void OnClick()
		{
			m_ShrinkItem.Locked = true;
			m_From.SendMessage( 38, "You have locked this shrunken pet so only you can reclaim it." );
		}
	}

	public class UnLockShrinkItem : ContextMenuEntry
	{
		private Mobile m_From;
		private ShrinkItem m_ShrinkItem;

		public UnLockShrinkItem( Mobile from, ShrinkItem shrink ) : base( 2033, 5 )
		{
			m_From = from;
			m_ShrinkItem = shrink;
		}

		public override void OnClick()
		{
			m_ShrinkItem.Locked = false;
			m_From.SendMessage( 38, "You have unlocked this shrunken pet, now anyone can reclaim it as theirs." );
		}
	}
}
