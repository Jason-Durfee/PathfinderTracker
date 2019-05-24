using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class Weapon : DatabaseObject
    {
        #region Constructors
        public Weapon(SqlDataReader dr) {
            ID = (int)dr["WeaponID"];
            SpecialAttributes = (string)dr["SpecialAttributes"];
            WeaponTypeID = (int)dr["WeaponTypeID"];
            MaterialID = (int)dr["MaterialID"];
        }

        public Weapon() {

        }
        #endregion


        private string _SpecialAttributes;
        private int _WeaponTypeID;
        private WeaponType _WeaponType;
        private int _MaterialID;
        private Material _Material;

        /// <summary>
        /// gets and sets the SpecialAttributes attribute for the Weapon object
        /// </summary>
        [Display(Name = "Special Attributes")]
        public string SpecialAttributes {
            get {
                if(_SpecialAttributes == null) {
                    return "";
                }
                return _SpecialAttributes;
            }
            set {
                _SpecialAttributes = value;
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
        [Display(Name = "Weapon Type")]
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

        /// <summary>
        /// gets the material and weapon type for the weapon
        /// </summary>
        [Display(Name = "Weapon")]
        public string Details {
            get {
                if(Material != null && WeaponType != null) {
                    return Material.Name + " " + WeaponType.Name;
                }
                else {
                    return "Unknown";
                }
            }
        }

        /// <summary>
        /// gets the damage statistics for the weapon object
        /// </summary>
        [Display(Name = "Attack Dice S/M")]
        public string Damage {
            get {
                if(WeaponType != null) {
                    return WeaponType.AttackDiceSmall + "/" + WeaponType.AttackDiceMedium;
                }
                else {
                    return "Unknown";
                }
            }
        }

        /// <summary>
        /// gets the damage statistics for the weapon object
        /// </summary>
        [Display(Name = "Gold Value")]
        public string GPValue {
            get {
                if(WeaponType != null && Material != null) {
                    return WeaponType.GPValue + Material.WeaponAddedGold + " gp";
                }
                else {
                    return "Unknown";
                }
            }
        }
    }
}
