using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class ArmorAddon : DatabaseObject
    {
        #region Constructors
        public ArmorAddon(SqlDataReader dr) {
            ID = (int)dr["ArmorAddonID"];
            MaterialID = (int)dr["MaterialID"];
            ArmorAddonTypeID = (int)dr["ArmorAddonTypeID"];
        }

        public ArmorAddon() {

        }
        #endregion


        private int _MaterialID;
        private Material _Material;
        private int _ArmorAddonTypeID;
        private ArmorAddonType _ArmorAddonType;
        

        /// <summary>
        /// gets and sets the ArmorAddon object MaterialID attribute
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
        /// gets and sets the ArmorAddon object Material attribute
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
        /// gets and sets the ArmorAddon object ArmorAddonTypeID attribute
        /// </summary>
        public int ArmorAddonTypeID {
            get {
                return _ArmorAddonTypeID;
            }
            set {
                _ArmorAddonTypeID = value;
            }
        }

        /// <summary>
        /// gets and sets the ArmorAddon object ArmorAddonType attribute
        /// </summary>
        public ArmorAddonType ArmorAddonType {
            get {
                if(_ArmorAddonType == null && _ArmorAddonTypeID > 0) {
                    _ArmorAddonType = DAL.GetArmorAddonType(_ArmorAddonTypeID);
                }
                return _ArmorAddonType;
            }
            set {
                _ArmorAddonType = value;
            }
        }

        /// <summary>
        /// gets and sets the ArmorAddon object Name attribute
        /// </summary>
        public string Name {
            get {
                if(ArmorAddonType != null && Material != null) {
                    return Material.Name + " " + ArmorAddonType.Name;
                }
                return "Unknown";
            }
        }

        /// <summary>
        /// gets and sets the ArmorAddon object GPValue attribute
        /// </summary>
        public int GPValue {
            get {
                int retVal = 0;
                if(ArmorAddonType != null && Material != null && Material.BaseGoldMultiplier > 1) {
                    retVal += ArmorAddonType.BaseGPValue * Material.BaseGoldMultiplier;
                }
                if(ArmorAddonType != null && Material != null && Material.WeightGoldMultiplier > 1) {
                    retVal += ArmorAddonType.Weight * Material.WeightGoldMultiplier;
                }
                if(ArmorAddonType != null && Material != null) {
                    retVal += Material.WeaponAddedGold;
                    retVal += ArmorAddonType.BaseGPValue;
                }
                return retVal;
            }
        }
    }
}
