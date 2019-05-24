using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class ArmorType : NamedObject
    {
        #region Constructors
        public ArmorType(SqlDataReader dr) {
            ID = (int)dr["ArmorTypeID"];
            Name = (string)dr["Name"];
            BaseGPValue = (int)dr["BaseGPValue"];
            ACBonus = (int)dr["ACBonus"];
            ArmorCheckPenalty = (int)dr["ArmorCheckPenalty"];
            Weight = (int)dr["Weight"];
            ArmorCoreTypeID = (int)dr["ArmorCoreTypeID"];
        }

        public ArmorType() {

        }
        #endregion

        private int _BaseGPValue;
        private int _ACBonus;
        private int _ArmorCheckPenalty;
        private int _Weight;
        private int _ArmorCoreTypeID;
        private ArmorCoreType _ArmorCoreType;


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
        /// gets and sets the ACBonus attribute for the ArmorType object
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
        /// gets and sets the ArmorCheckPenalty attribute for the ArmorType object
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
        /// gets and sets the Weight attribute for the ArmorType object
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
        /// gets and sets the ArmorCoreTypeID attribute for the ArmorType object
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
        /// gets and sets the ArmorCoreType attribute for the ArmorType object
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
    }
}
