using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class Item : NamedObject
    {
        #region Constructors
        public Item(SqlDataReader dr) {
            ID = (int)dr["ItemID"];
            Name = (string)dr["Name"];
            Description = (string)dr["Description"];
            ConstructionRequirements = (string)dr["ConstructionRequirements"];
            GPValue = (int)dr["GPValue"];
            SlotID = (int)dr["SlotID"];
        }

        public Item() {

        }
        #endregion

        private string _Description;
        private string _ConstructionRequirements;
        private int _GPValue;
        private int _SlotID;
        private Slot _Slot;


        /// <summary>
        /// gets and sets the Description attribute for the Item object
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
        /// gets and sets the ConstructionRequirements attribute for the Item object
        /// </summary>
        [Display(Name = "Construction Requirements")]
        public string ConstructionRequirements {
            get {
                if(_ConstructionRequirements == null) {
                    return "";
                }
                return _ConstructionRequirements;
            }
            set {
                _ConstructionRequirements = value;
            }
        }

        /// <summary>
        /// gets and sets the GPValue attribute for the Item object
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
        /// gets and sets the SlotID attribute for the Item object
        /// </summary>
        public int SlotID {
            get {
                return _SlotID;
            }
            set {
                _SlotID = value;
            }
        }

        /// <summary>
        /// gets and sets the Slot attribute for the Item object
        /// </summary>
        public Slot Slot {
            get {
                if(_Slot == null && _SlotID > 0) {
                    _Slot = DAL.GetSlot(_SlotID);
                }
                return _Slot;
            }
            set {
                _Slot = value;
            }
        }
    }
}
