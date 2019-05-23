using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class Weapon : NamedObject
    {
        #region Constructors
        public Weapon(SqlDataReader dr) {
            ID = (int)dr["WeaponID"];
            Name = (string)dr["Name"];
            AttackDiceSmall = (string)dr["AttackDiceSmall"];
            AttackDiceMedium = (string)dr["AttackDiceMedium"];
            Critical = (string)dr["Critical"];
            SpecialAttributes = (string)dr["SpecialAttributes"];
            GPValue = (int)dr["GPValue"];
            AttackRange = (int)dr["AttackRange"];
            Weight = (int)dr["Weight"];
            WeaponTypeID = (int)dr["WeaponTypeID"];
            DamageTypeID = (int)dr["DamageTypeID"];
            MaterialID = (int)dr["MaterialID"];
        }

        public Weapon() {

        }
        #endregion

        private string _AttackDiceSmall;
        private string _AttackDiceMedium;
        private string _Critical;
        private string _SpecialAttributes;
        private int _GPValue;
        private int _AttackRange;
        private int _Weight;
        private int _WeaponCategoryID;
        private WeaponCategory _WeaponCategory;
        private int _WeaponTypeID;
        private WeaponType _WeaponType;
        private int _DamageTypeID;
        private DamageType _DamageType;
        private int _MaterialID;
        private Material _Material;

        /// <summary>
        /// gets and sets the AttackDiceSmall attribute for the Weapon object
        /// </summary>
        [Display( Name = "Attack Dice Small")]
        public string AttackDiceSmall {
            get {
                return _AttackDiceSmall;
            }
            set {
                _AttackDiceSmall = value;
            }
        }

        /// <summary>
        /// gets and sets the AttackDiceMedium attribute for the Weapon object
        /// </summary>
        [Display( Name = "Attack Dice Medium")]
        public string AttackDiceMedium {
            get {
                return _AttackDiceMedium;
            }
            set {
                _AttackDiceMedium = value;
            }
        }

        /// <summary>
        /// gets and sets the Critical attribute for the Weapon object
        /// </summary>
        public string Critical {
            get {
                return _Critical;
            }
            set {
                _Critical = value;
            }
        }

        /// <summary>
        /// gets and sets the SpecialAttributes attribute for the Weapon object
        /// </summary>
        [Display( Name = "Special Attributes")]
        public string SpecialAttributes {
            get {
                return _SpecialAttributes;
            }
            set {
                _SpecialAttributes = value;
            }
        }

        /// <summary>
        /// gets and sets the GPValue attribute for the Weapon object
        /// </summary>
        [Display( Name = "Gold Value")]
        public int GPValue {
            get {
                return _GPValue;
            }
            set {
                _GPValue = value;
            }
        }

        /// <summary>
        /// gets and sets the AttackRange attribute for the Weapon object
        /// </summary>
        [Display( Name = "Attack Range")]
        public int AttackRange {
            get {
                return _AttackRange;
            }
            set {
                _AttackRange = value;
            }
        }

        /// <summary>
        /// gets and sets the Weight attribute for the Weapon object
        /// </summary>
        public int Weight {
            get {
                return _Weight;
            }
            set {
                _Weight = value;
            }
        }

        /// <summary>
        /// gets and sets the WeaponCategoryID attribute for the Weapon object
        /// </summary>
        public int WeaponCategoryID {
            get {
                return _WeaponCategoryID;
            }
            set {
                _WeaponCategoryID = value;
            }
        }

        /// <summary>
        /// gets and sets the WeaponCategory attribute for the Weapon object
        /// </summary>
        [Display( Name = "Weapon Category")]
        public WeaponCategory WeaponCategory {
            get {
                if(_WeaponCategory == null && _WeaponCategoryID > 0) {
                    _WeaponCategory = DAL.GetWeaponCategory(_WeaponCategoryID);
                }
                return _WeaponCategory;
            }
            set {
                _WeaponCategory = value;
            }
        }


        /// <summary>
        /// gets and sets the WeaponTypeID attribute for the Weapon object
        /// </summary>
        public int WeaponTypeID {
            get {
                return _WeaponTypeID;
            }
            set {
                _WeaponTypeID = value;
            }
        }

        /// <summary>
        /// gets and sets the WeaponType attribute for the Weapon object
        /// </summary>
        [Display( Name = "Weapon Type")]
        public WeaponType WeaponType {
            get {
                if(_WeaponType == null && _WeaponTypeID > 0) {
                    _WeaponType = DAL.GetWeaponType(_WeaponTypeID);
                }
                return _WeaponType;
            }
            set {
                _WeaponType = value;
            }
        }

        /// <summary>
        /// gets and sets the DamageTypeID attribute for the Weapon object
        /// </summary>
        public int DamageTypeID {
            get {
                return _DamageTypeID;
            }
            set {
                _DamageTypeID = value;
            }
        }


        /// <summary>
        /// gets and sets the DamageType attribute for the Weapon object
        /// </summary>
        [Display( Name = "Damage Type")]
        public DamageType DamageType {
            get {
                if(_DamageType == null && _DamageTypeID > 0) {
                    _DamageType = DAL.GetDamageType(_DamageTypeID);
                }
                return _DamageType;
            }
            set {
                _DamageType = value;
            }
        }

        /// <summary>
        /// gets and sets the MaterialID attribute for the Weapon object
        /// </summary>
        public int MaterialID {
            get {
                return _MaterialID;
            }
            set {
                _MaterialID = value;
            }
        }

        /// <summary>
        /// gets and sets the Material attribute for the Weapon object
        /// </summary>
        public Material Material {
            get {
                if(_Material == null && _MaterialID > 0) {
                    _Material = DAL.GetMaterial(_MaterialID);
                }
                return _Material;
            }
            set {
                _Material = value;
            }
        }
    }
}
