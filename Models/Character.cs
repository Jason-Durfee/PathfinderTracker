using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class Character : NamedObject
    {
        #region Constructors
        public Character(SqlDataReader dr) {
            ID = (int)dr["CharacterID"];
            Name = (string)dr["Name"];
            Level = (int)dr["Level"];
            RaceID = (int)dr["RaceID"];
            CampaignID = (int)dr["CampaignID"];
        }

        public Character() {

        }
        #endregion

        private int _Level;
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
