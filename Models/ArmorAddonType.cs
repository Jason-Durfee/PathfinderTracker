using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class ArmorAddonType : NamedObject
    {
        #region Constructors
        public ArmorAddonType(SqlDataReader dr) {
            ID = (int)dr["ArmorAddonTypeID"];
            Name = (string)dr["Name"];
            BaseGPValue = (int)dr["BaseGPValue"];
            ArmorCheckPenalty = (int)dr["ArmorCheckPenalty"];
            Weight = (int)dr["Weight"];
        }

        public ArmorAddonType() {

        }
        #endregion

        private int _BaseGPValue;
        private int _ArmorCheckPenalty;
        private int _Weight;

        /// <summary>
        /// gets and sets the ArmorAddonType object GPValue attribute
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
        /// gets and sets the ArmorAddonType object ArmorCheckPenalty attribute
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
        /// gets and sets the ArmorAddonType object Weight attribute
        /// </summary>
        public int Weight {
            get {
                return _Weight;
            }
            set {
                _Weight = value;
            }
        }
    }
}
