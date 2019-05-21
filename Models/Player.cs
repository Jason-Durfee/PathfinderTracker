using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class Player : NamedObject
    {
        #region Constructors
        public Player(SqlDataReader dr) {
            ID = (int)dr["PlayerID"];
            Name = (string)dr["Name"];
            HPCurrent = (int)dr["HPCurrent"];
            HPMax = (int)dr["HPMax"];
            CharacterID = (int)dr["CharacterID"];
            Bonuses = (string)dr["Bonuses"];
        }

        public Player() {

        }
        #endregion

        private int _HPMax;
        private int _HPCurrent;
        private int _CharacterID;
        private Character _Character;
        private string _Bonuses;

        /// <summary>
        /// gets and sets the HPMax attribute for the Player object
        /// </summary>
        public int HPMax {
            get {
                return _HPMax;
            }
            set {
                _HPMax = value;
            }
        }

        /// <summary>
        /// gets and sets the HPCurrent attribute for the Player object
        /// </summary>
        public int HPCurrent {
            get {
                return _HPCurrent;
            }
            set {
                _HPCurrent = value;
            }
        }

        /// <summary>
        /// gets and sets the CharacterID attribute for the Player object
        /// </summary>
        public int CharacterID {
            get {
                return _CharacterID;
            }
            set {
                _CharacterID = value;
            }
        }

        /// <summary>
        /// gets and sets the Character attribute for the Player object
        /// </summary>
        public Character Character {
            get {
                if(_Character == null && _CharacterID > 0) {
                    _Character = DAL.GetCharacter(_CharacterID);
                }
                return _Character;
            }
            set {
                _Character = value;
            }
        }

        /// <summary>
        /// gets and sets the Bonuses attribute for the Player object
        /// </summary>
        public string Bonuses {
            get {
                return _Bonuses;
            }
            set {
                _Bonuses = value;
            }
        }

    }
}
