using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class Spell : NamedObject
    {
        #region Constructors
        public Spell(SqlDataReader dr) {
            ID = (int)dr["SpellID"];
            Name = (string)dr["Name"];
            Description = (string)dr["Description"];
            MagicSchoolID = (int)dr["MagicSchoolID"];
            CastingTime = (string)dr["CastingTime"];
            RangeDistance = (string)dr["RangeDistance"];
            Target = (string)dr["Target"];
            Duration = (string)dr["Duration"];
            SavingThrow = (string)dr["SavingThrow"];
            SpellResistance = (string)dr["SpellResistance"];
        }

        public Spell() {

        }
        #endregion

        private string _Description;
        private int _MagicSchoolID;
        private MagicSchool _MagicSchool;
        private string _CastingTime;
        private string _RangeDistance;
        private string _Target;
        private string _Duration;
        private string _SavingThrow;
        private string _SpellResistance;

        /// <summary>
        /// gets and sets the MagicSchool objects MagicSchoolID attribute
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
        /// gets and sets the MagicSchool attribute for the Armor object
        /// </summary>
        [Display( Name = "Magic School")]
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

        /// <summary>
        /// gets and sets the Description attribute for the Spell object
        /// </summary>
        [Required]
        public string Description {
            get {
                return _Description;
            }
            set {
                _Description = value;
            }
        }

        /// <summary>
        /// gets and sets the CastingTime attribute for the Spell object
        /// </summary>
        [Display( Name = "Casting Time")]
        [Required]
        public string CastingTime {
            get {
                return _CastingTime;
            }
            set {
                _CastingTime = value;
            }
        }

        /// <summary>
        /// gets and sets the RangeDistance attribute for the Spell object
        /// </summary>
        [Display( Name = "Range")]
        [Required]
        public string RangeDistance {
            get {
                return _RangeDistance;
            }
            set {
                _RangeDistance = value;
            }
        }

        /// <summary>
        /// gets and sets the Target attribute for the Spell object
        /// </summary>
        [Required]
        public string Target {
            get {
                return _Target;
            }
            set {
                _Target = value;
            }
        }

        /// <summary>
        /// gets and sets the Duration attribute for the Spell object
        /// </summary>
        [Required]
        public string Duration {
            get {
                return _Duration;
            }
            set {
                _Duration = value;
            }
        }

        /// <summary>
        /// gets and sets the SavingThrow attribute for the Spell object
        /// </summary>
        [Display( Name = "Saving Throw")]
        [Required]
        public string SavingThrow {
            get {
                return _SavingThrow;
            }
            set {
                _SavingThrow = value;
            }
        }

        /// <summary>
        /// gets and sets the SpellResistance attribute for the Spell object
        /// </summary>
        [Display( Name = "Spell Resistance")]
        [Required]
        public string SpellResistance {
            get {
                return _SpellResistance;
            }
            set {
                _SpellResistance = value;
            }
        }

    }
}
