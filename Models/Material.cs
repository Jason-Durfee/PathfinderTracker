using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class Material : NamedObject
    {
        #region Constructors
        public Material(SqlDataReader dr) {
            ID = (int)dr["MaterialID"];
            Name = (string)dr["Name"];
            AmmoAddedGold = (int)dr["AmmoAddedGold"];
            LightAddedGold = (int)dr["LightAddedGold"];
            MediumAddedGold = (int)dr["MediumAddedGold"];
            HeavyAddedGold = (int)dr["HeavyAddedGold"];
            WeaponAddedGold = (int)dr["WeaponAddedGold"];
            ShieldAddedGold = (int)dr["ShieldAddedGold"];
            BaseGoldMultiplier = (int)dr["BaseGoldMultiplier"];
            WeightGoldMultiplier = (int)dr["WeightGoldMultiplier"];
        }

        public Material() {

        }
        #endregion

        private int _WeaponAddedGold;
        private int _HeavyAddedGold;
        private int _MediumAddedGold;
        private int _LightAddedGold;
        private int _ShieldAddedGold;
        private int _AmmoAddedGold;
        private int _WeightGoldMultiplier;
        private int _BaseGoldMultiplier;


        /// <summary>
        /// gets and sets the added gold value for the material object
        /// </summary>
        [Display( Name = "Added Gold Value for Heavy Armor")]
        public int HeavyAddedGold {
            get {
                return _HeavyAddedGold;
            }
            set {
                _HeavyAddedGold = value;
            }
        }

        /// <summary>
        /// gets and sets the added gold value for the material object
        /// </summary>
        [Display(Name = "Added Gold Value for Medium Armor")]
        public int MediumAddedGold {
            get {
                return _MediumAddedGold;
            }
            set {
                _MediumAddedGold = value;
            }
        }

        /// <summary>
        /// gets and sets the added gold value for the material object
        /// </summary>
        [Display(Name = "Added Gold Value for Light Armor")]
        public int LightAddedGold {
            get {
                return _LightAddedGold;
            }
            set {
                _LightAddedGold = value;
            }
        }

        /// <summary>
        /// gets and sets the added gold value for the material object
        /// </summary>
        [Display(Name = "Added Gold Value for Weapons")]
        public int WeaponAddedGold {
            get {
                return _WeaponAddedGold;
            }
            set {
                _WeaponAddedGold = value;
            }
        }

        /// <summary>
        /// gets and sets the added gold value for the material object
        /// </summary>
        [Display(Name = "Added Gold Value for Shields")]
        public int ShieldAddedGold {
            get {
                return _ShieldAddedGold;
            }
            set {
                _ShieldAddedGold = value;
            }
        }

        /// <summary>
        /// gets and sets the added gold value for the material object
        /// </summary>
        [Display(Name = "Added Gold Value for Ammunition")]
        public int AmmoAddedGold {
            get {
                return _AmmoAddedGold;
            }
            set {
                _AmmoAddedGold = value;
            }
        }

        /// <summary>
        /// gets and sets the added gold value for the material object
        /// </summary>
        [Display(Name = "Gold Multiplier by Weight")]
        public int WeightGoldMultiplier {
            get {
                return _WeightGoldMultiplier;
            }
            set {
                _WeightGoldMultiplier = value;
            }
        }

        /// <summary>
        /// gets and sets the added gold value for the material object
        /// </summary>
        [Display(Name = "Gold Multiplier by Base Cost")]
        public int BaseGoldMultiplier {
            get {
                return _BaseGoldMultiplier;
            }
            set {
                _BaseGoldMultiplier = value;
            }
        }
    }
}
