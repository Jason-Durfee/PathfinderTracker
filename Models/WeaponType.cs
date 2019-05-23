using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class WeaponType : NamedObject
    {
        #region Constructors
        public WeaponType(SqlDataReader dr) {
            ID = (int)dr["WeaponTypeID"];
            Name = (string)dr["Name"];
            GPValue = (int)dr["GPValue"];
            Weight = (int)dr["Weight"];
            CoreTypeID = (int)dr["CoreTypeID"];
            WeaponCategoryID = (int)dr["WeaponCategoryID"];
            AttackRange = (int)dr["AttackRange"];
            AttackDiceSmall = (string)dr["AttackDiceSmall"];
            AttackDiceMedium = (string)dr["AttackDiceMedium"];
            Critical = (string)dr["Critical"];
        }

        public WeaponType() {

        }
        #endregion

        private int _AttackRange;
        private int _CoreTypeID;
        private CoreType _CoreType;
        private int _GPValue;
        private int _Weight;
        private string _AttackDiceSmall;
        private string _AttackDiceMedium;
        private string _Critical;
        private int _WeaponCategoryID;
        private WeaponCategory _WeaponCategory;

        /// <summary>
        /// gets and sets the AttackRange attribute for the WeaponType object
        /// </summary>
        [Display(Name = "Attack Range")]
        public int AttackRange {
            get {
                return _AttackRange;
            }
            set {
                _AttackRange = value;
            }
        }

        /// <summary>
        /// gets and sets the GPValue attribute for the WeaponType object
        /// </summary>
        [Display(Name = "Gold Value")]
        public int GPValue {
            get {
                return _GPValue;
            }
            set {
                _GPValue = value;
            }
        }

        /// <summary>
        /// gets and sets the AttackDiceSmall attribute for the WeaponType object
        /// </summary>
        [Display(Name = "Attack Dice Small")]
        public string AttackDiceSmall {
            get {
                return _AttackDiceSmall;
            }
            set {
                _AttackDiceSmall = value;
            }
        }

        /// <summary>
        /// gets and sets the AttackDiceMedium attribute for the WeaponType object
        /// </summary>
        [Display(Name = "Attack Dice Medium")]
        public string AttackDiceMedium {
            get {
                return _AttackDiceMedium;
            }
            set {
                _AttackDiceMedium = value;
            }
        }

        /// <summary>
        /// gets and sets the Critical attribute for the WeaponType object
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
        /// gets and sets the Weight attribute for the WeWeaponTypeapon object
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
        /// gets and sets the WeaponCategoryID attribute for the WeaponType object
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
        /// gets and sets the WeaponCategory attribute for the WeaponType object
        /// </summary>
        [Display(Name = "Weapon Category")]
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
        /// gets and sets the CoreTypeID attribute for the WeaponType object
        /// </summary>
        public int CoreTypeID {
            get {
                return _CoreTypeID;
            }
            set {
                _CoreTypeID = value;
            }
        }

        /// <summary>
        /// gets and sets the CoreType attribute for the WeaponType object
        /// </summary>
        [Display(Name = "Core Type")]
        public CoreType CoreType {
            get {
                if(_CoreType == null && _CoreTypeID > 0) {
                    _CoreType = DAL.GetCoreType(_CoreTypeID);
                }
                return _CoreType;
            }
            set {
                _CoreType = value;
            }
        }
    }
}
