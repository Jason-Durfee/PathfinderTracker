using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class Armor : DatabaseObject
    {
        #region Constructors
        public Armor(SqlDataReader dr) {
            ID = (int)dr["ArmorID"];
            BaseGPValue = (int)dr["BaseGPValue"];
            ACBonus = (int)dr["ACBonus"];
            ArmorCheckPenalty = (int)dr["ArmorCheckPenalty"];
            Weight = (int)dr["Weight"];
            MaterialID = (int)dr["MaterialID"];
            ArmorTypeID = (int)dr["ArmorTypeID"];
            ArmorCoreTypeID = (int)dr["ArmorCoreTypeID"];
            ArmorAddonID = (int)dr["ArmorAddonID"];
            SpecialAttributes = (string)dr["SpecialAttributes"];
        }

        public Armor() {

        }
        #endregion

        private int _BaseGPValue;
        private int _ACBonus;
        private int _ArmorCheckPenalty;
        private int _Weight;
        private int _MaterialID;
        private Material _Material;
        private int _ArmorTypeID;
        private ArmorType _ArmorType;
        private int _ArmorCoreTypeID;
        private ArmorCoreType _ArmorCoreType;
        private int _ArmorAddonID;
        private ArmorAddon _ArmorAddon;
        private string _SpecialAttributes;

        /// <summary>
        /// gets and sets the GPValue attribute for the Armor object
        /// </summary>
        [Display(Name = "Gold Value")]
        public int BaseGPValue {
            get {
                return _BaseGPValue;
            }
            set {
                _BaseGPValue = value;
            }
        }

        /// <summary>
        /// gets and sets the ACBonus attribute for the Armor object
        /// </summary>
        [Display(Name = "AC Bonus")]
        public int ACBonus {
            get {
                return _ACBonus;
            }
            set {
                _ACBonus = value;
            }
        }

        /// <summary>
        /// gets and sets the ArmorCheckPenalty attribute for the Armor object
        /// </summary>
        [Display(Name = "AC Penalty")]
        public int ArmorCheckPenalty {
            get {
                return _ArmorCheckPenalty;
            }
            set {
                _ArmorCheckPenalty = value;
            }
        }

        /// <summary>
        /// gets and sets the Weight attribute for the Armor object
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
        /// gets and sets the MaterialID attribute for the Armor object
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
        /// gets and sets the Material attribute for the Armor object
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
        /// gets and sets the ArmorCoreTypeID attribute for the Armor object
        /// </summary>
        public int ArmorCoreTypeID {
            get {
                return _ArmorCoreTypeID;
            }
            set {
                _ArmorCoreTypeID = value;
            }
        }

        /// <summary>
        /// gets and sets the ArmorCoreType attribute for the Armor object
        /// </summary>
        [Display(Name = "Armor Core Type")]
        public ArmorCoreType ArmorCoreType {
            get {
                if(_ArmorCoreType == null && _ArmorCoreTypeID > 0) {
                    _ArmorCoreType = DAL.GetArmorCoreType(_ArmorCoreTypeID);
                }
                return _ArmorCoreType;
            }
            set {
                _ArmorCoreType = value;
            }
        }

        /// <summary>
        /// gets and sets the ArmorTypeID attribute for the Armor object
        /// </summary>
        public int ArmorTypeID {
            get {
                return _ArmorTypeID;
            }
            set {
                _ArmorTypeID = value;
            }
        }

        /// <summary>
        /// gets and sets the ArmorType attribute for the Armor object
        /// </summary>
        [Display(Name = "Armor Type")]
        public ArmorType ArmorType {
            get {
                if(_ArmorType == null && _ArmorTypeID > 0) {
                    _ArmorType = DAL.GetArmorType(_ArmorTypeID);
                }
                return _ArmorType;
            }
            set {
                _ArmorType = value;
            }
        }

        /// <summary>
        /// gets and sets the ArmorAddonID attribute for the Armor object
        /// </summary>
        public int ArmorAddonID {
            get {
                return _ArmorAddonID;
            }
            set {
                _ArmorAddonID = value;
            }
        }

        /// <summary>
        /// gets and sets the ArmorAddon attribute for the Armor object
        /// </summary>
        [Display(Name = "Armor Addon")]
        public ArmorAddon ArmorAddon {
            get {
                if(_ArmorAddon == null && _ArmorAddonID > 0) {
                    _ArmorAddon = DAL.GetArmorAddon(_ArmorAddonID);
                }
                return _ArmorAddon;
            }
            set {
                _ArmorAddon = value;
            }
        }

        /// <summary>
        /// gets and sets the SpecialAttributes attribute for the Armor object
        /// </summary>
        [Display(Name = "Special Attributes")]
        public string SpecialAttributes {
            get {
                return _SpecialAttributes;
            }
            set {
                _SpecialAttributes = value;
            }
        }

        /// <summary>
        /// gets and sets the SpecialAttributes attribute for the Armor object
        /// </summary>
        [Display(Name = "Gold Value")]
        public string GPValue {
            get {
                if(Material != null) {
                    int retVal = 0;
                    int baseVal = BaseGPValue;
                    if(ArmorCoreType.Name == "Heavy") {
                        retVal = baseVal + Material.HeavyAddedGold;
                    }
                    else if(ArmorCoreType.Name == "Medium") {
                        retVal = baseVal + Material.MediumAddedGold;
                    }
                    else if(ArmorCoreType.Name == "Light") {
                        retVal = baseVal + Material.LightAddedGold;
                    }
                    else if(ArmorCoreType.Name == "Shield") {
                        retVal = baseVal + Material.ShieldAddedGold;
                    }
                    if(Material.WeightGoldMultiplier > 1) {
                        retVal = Material.WeightGoldMultiplier * Weight;
                    }
                    if(Material.BaseGoldMultiplier > 1) {
                        retVal = Material.BaseGoldMultiplier * BaseGPValue;
                    }
                    return retVal.ToString() + " gp";
                }
                return "Unknown";
            }
        }
    }
}
