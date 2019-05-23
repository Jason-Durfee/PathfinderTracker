using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class ClassesToCharacter : DatabaseObject
    {
        #region Constructors
        public ClassesToCharacter(SqlDataReader dr) {
            ID = (int)dr["ClassesToCharacterID"];
            ClassLevel = (int)dr["ClassLevel"];
            CharacterID = (int)dr["CharacterID"];
            CharacterClassID = (int)dr["CharacterClassID"];
            BloodlineID = (int)dr["BloodlineID"];
            DomainID = (int)dr["DomainID"];
            MagicSchoolID = (int)dr["MagicSchoolID"];
        }

        public ClassesToCharacter() {

        }
        #endregion

        private int _ClassLevel;
        private int _CharacterID;
        private Character _Character;
        private int _CharacterClassID;
        private CharacterClass _CharacterClass;
        private int _BloodlineID;
        private Bloodline _Bloodline;
        private int _DomainID;
        private Domain _Domain;
        private int _MagicSchoolID;
        private MagicSchool _MagicSchool;


        /// <summary>
        /// gets and sets the ClassLevel attribute for the ClassesToCharacter object
        /// </summary>
        public int ClassLevel {
            get {
                return _ClassLevel;
            }
            set {
                _ClassLevel = value;
            }
        }

        /// <summary>
        /// gets and sets the CharacterID attribute for the ClassesToCharacter object
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
        /// gets and sets the CharacterID attribute for the ClassesToCharacter object
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
        /// gets and sets the ClassID attribute for the ClassesToCharacter object
        /// </summary>
        public int CharacterClassID {
            get {
                return _CharacterClassID;
            }
            set {
                _CharacterClassID = value;
            }
        }

        /// <summary>
        /// gets and sets the ClassID attribute for the ClassesToClass object
        /// </summary>
        public CharacterClass CharacterClass {
            get {
                if(_CharacterClass == null && _CharacterClassID > 0) {
                    _CharacterClass = DAL.GetCharacterClass(_CharacterClassID);
                }
                return _CharacterClass;
            }
            set {
                _CharacterClass = value;
            }
        }

        /// <summary>
        /// gets and sets the Bloodline attribute for the ClassesToCharacter object
        /// </summary>
        public int BloodlineID {
            get {
                return _BloodlineID;
            }
            set {
                _BloodlineID = value;
            }
        }

        /// <summary>
        /// gets and sets the BloodlineID attribute for the ClassesToCharacter object
        /// </summary>
        public Bloodline Bloodline {
            get {
                if(_Bloodline == null && _BloodlineID > 0) {
                    _Bloodline = DAL.GetBloodline(_BloodlineID);
                }
                return _Bloodline;
            }
            set {
                _Bloodline = value;
            }
        }

        /// <summary>
        /// gets and sets the DomainID attribute for the ClassesToCharacter object
        /// </summary>
        public int DomainID {
            get {
                return _DomainID;
            }
            set {
                _DomainID = value;
            }
        }

        /// <summary>
        /// gets and sets the Domain attribute for the ClassesToCharacter object
        /// </summary>
        public Domain Domain {
            get {
                if(_Domain == null && _DomainID > 0) {
                    _Domain = DAL.GetDomain(_DomainID);
                }
                return _Domain;
            }
            set {
                _Domain = value;
            }
        }

        /// <summary>
        /// gets and sets the MagicSchoolID attribute for the ClassesToCharacter object
        /// </summary>
        public int MagicSchoolID {
            get {
                return _MagicSchoolID;
            }
            set {
                _MagicSchoolID = value;
            }
        }

        /// <summary>
        /// gets and sets the MagicSchool attribute for the ClassesToCharacter object
        /// </summary>
        public MagicSchool MagicSchool {
            get {
                if(_MagicSchool == null && _MagicSchoolID > 0) {
                    _MagicSchool = DAL.GetMagicSchool(_MagicSchoolID);
                }
                return _MagicSchool;
            }
            set {
                _MagicSchool = value;
            }
        }
    }
}
