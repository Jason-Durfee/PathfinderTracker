using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class CharacterClass : NamedObject
    {
        #region Constructors
        public CharacterClass(SqlDataReader dr) {
            ID = (int)dr["ClassID"];
            Name = (string)dr["Name"];
            HasBloodline = (bool)dr["HasBloodline"];
            HasDomain = (bool)dr["HasDomain"];
            HasMagicSchool = (bool)dr["HasMagicSchool"];
        }

        public CharacterClass() {

        }
        #endregion

        private bool _HasBloodline;
        private bool _HasDomain;
        private bool _HasMagicSchool;

        /// <summary>
        /// gets and sets the HasBloodline attribute for the CharacterClass object
        /// </summary>
        [Display(Name = "Has Bloodline")]
        public bool HasBloodline {
            get {
                return _HasBloodline;
            }
            set {
                _HasBloodline = value;
            }
        }

        /// <summary>
        /// gets and sets the HasDomain attribute for the CharacterClass object
        /// </summary>
        [Display(Name = "Has Domain")]
        public bool HasDomain {
            get {
                return _HasDomain;
            }
            set {
                _HasDomain = value;
            }
        }

        /// <summary>
        /// gets and sets the HasMagicSchool attribute for the CharacterClass object
        /// </summary>
        [Display(Name = "Has Magic School")]
        public bool HasMagicSchool {
            get {
                return _HasMagicSchool;
            }
            set {
                _HasMagicSchool = value;
            }
        }
    }
}
