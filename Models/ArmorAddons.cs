using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class ArmorAddon : NamedObject
    {
        #region Constructors
        public ArmorAddon(SqlDataReader dr) {
            ID = (int)dr["ArmorAddonID"];
            Name = (string)dr["Name"];
            GPValue = (int)dr["GPValue"];
            ArmorCheckPenalty = (int)dr["ArmorCheckPenalty"];
            Weight = (int)dr["Weight"];
            MaterialID = (int)dr["MaterialID"];
        }

        public ArmorAddon() {

        }
        #endregion

        private int _GPValue;
        private int _ArmorCheckPenalty;
        private int _Weight;
        private int _MaterialID;
        private Material _Material;

        /// <summary>
        /// gets and sets the ArmorAddon object GPValue attribute
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
        /// gets and sets the ArmorAddon object ArmorCheckPenalty attribute
        /// </summary>
        [Display( Name = "AC Penalty")]
        public int ArmorCheckPenalty {
            get {
                return _ArmorCheckPenalty;
            }
            set {
                _ArmorCheckPenalty = value;
            }
        }

        /// <summary>
        /// gets and sets the ArmorAddon object Weight attribute
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
    }
}
