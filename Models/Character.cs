using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Character : NamedObject
    {
        #region Constructors
        public Character(SqlDataReader dr) {
            ID = (int)dr["CharacterID"];
            Name = (string)dr["Name"];
            Level = (int)dr["Level"];
            IsNPC = (bool)dr["IsNPC"];
            AlignmentID = (int)dr["AlignmentID"];
            DeityID = (int)dr["DeityID"];
            RaceID = (int)dr["RaceID"];
            CampaignID = (int)dr["CampaignID"];
        }

        public Character() {

        }
        #endregion

        private int _Level;
        private bool _IsNPC;
        private int _AlignmentID;
        private Alignment _Alignment;
        private int _DeityID;
        private Deity _Deity;
        private int _RaceID;
        private Race _Race;
        private int _CampaignID;
        private Campaign _Campaign;

        /// <summary>
        /// gets and sets the Character objects Level attribute
        /// </summary>
        public int Level {
            get {
                return _Level;
            }
            set {
                _Level = value;
            }
        }

        /// <summary>
        /// gets and sets the Character objects IsNPC attribute
        /// </summary>
        public bool IsNPC {
            get {
                return _IsNPC;
            }
            set {
                _IsNPC = value;
            }
        }

        /// <summary>
        /// gets and sets the Character objects AlignmentID attribute
        /// </summary>
        public int AlignmentID {
            get {
                return _AlignmentID;
            }
            set {
                _AlignmentID = value;
            }
        }

        /// <summary>
        /// gets and sets the Alignment attribute for the Armor object
        /// </summary>
        public Alignment Alignment {
            get {
                if(_Alignment == null && _AlignmentID > 0) {
                    _Alignment = DAL.GetAlignment(_AlignmentID);
                }
                return _Alignment;
            }
            set {
                _Alignment = value;
            }
        }

        /// <summary>
        /// gets and sets the Character objects DeityID attribute
        /// </summary>
        public int DeityID {
            get {
                return _DeityID;
            }
            set {
                _DeityID = value;
            }
        }

        /// <summary>
        /// gets and sets the Character objects Deity attribute
        /// </summary>
        public Deity Deity {
            get {
                if(_Deity == null && _DeityID > 0) {
                    _Deity = DAL.GetDeity(_DeityID);
                }
                return _Deity;
            }
            set {
                _Deity = value;
            }
        }

        /// <summary>
        /// gets and sets the Character objects RaceID attribute
        /// </summary>
        public int RaceID {
            get {
                return _RaceID;
            }
            set {
                _RaceID = value;
            }
        }

        /// <summary>
        /// gets and sets the Character objects Race attribute
        /// </summary>
        public Race Race {
            get {
                if(_Race == null && _RaceID > 0) {
                    _Race = DAL.GetRace(_RaceID);
                }
                return _Race;
            }
            set {
                _Race = value;
            }
        }

        /// <summary>
        /// gets and sets the Character objects CampaignID attribute
        /// </summary>
        public int CampaignID {
            get {
                return _CampaignID;
            }
            set {
                _CampaignID = value;
            }
        }

        /// <summary>
        /// gets and sets the Character objects Campaign attribute
        /// </summary>
        public Campaign Campaign {
            get {
                if(_Campaign == null && _CampaignID > 0) {
                    _Campaign = DAL.GetCampaign(_CampaignID);
                }
                return _Campaign;
            }
            set {
                _Campaign = value;
            }
        }
    }
}
