using System;
using System.Collections.Generic;
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
            ClassID = (int)dr["ClassID"];
        }

        public ClassesToCharacter() {

        }
        #endregion

        private int _ClassLevel;
        private int _CharacterID;
        private Character _Character;
        private int _ClassID;
        private Class _Class;
        private int _SubClassID;
        private SubClass _SubClass;


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
        public int ClassID {
            get {
                return _ClassID;
            }
            set {
                _ClassID = value;
            }
        }

        /// <summary>
        /// gets and sets the ClassID attribute for the ClassesToClass object
        /// </summary>
        public Class Class {
            get {
                if(_Class == null && _ClassID > 0) {
                    _Class = DAL.GetClass(_ClassID);
                }
                return _Class;
            }
            set {
                _Class = value;
            }
        }

        /// <summary>
        /// gets and sets the SubClassID attribute for the ClassesToCharacter object
        /// </summary>
        public int SubClassID {
            get {
                return _SubClassID;
            }
            set {
                _SubClassID = value;
            }
        }

        /// <summary>
        /// gets and sets the SubClassID attribute for the ClassesToCharacter object
        /// </summary>
        public SubClass SubClass {
            get {
                if(_SubClass == null && _SubClassID > 0) {
                    _SubClass = DAL.GetSubClass(_SubClassID);
                }
                return _SubClass;
            }
            set {
                _SubClass = value;
            }
        }
    }
}
