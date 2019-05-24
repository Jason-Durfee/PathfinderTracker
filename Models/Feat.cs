using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class Feat : NamedObject
    {
        #region Constructors
        public Feat(SqlDataReader dr) {
            ID = (int)dr["FeatID"];
            Name = (string)dr["Name"];
            Description = (string)dr["Description"];
            FeatTypeID = (int)dr["FeatTypeID"];
            Prerequisites = (string)dr["Prerequisites"];
        }

        public Feat() {

        }
        #endregion

        private string _Description;
        private string _Prerequisites;
        private int _FeatTypeID;
        private FeatType _FeatType;

        /// <summary>
        /// gets and sets the Description attribute for the Feat object
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
        /// gets and sets the Prerequisites attribute for the Feat object
        /// </summary>
        [Required]
        public string Prerequisites {
            get {
                return _Prerequisites;
            }
            set {
                _Prerequisites = value;
            }
        }

        /// <summary>
        /// gets and sets the FeatTypeID attribute for the Feat object
        /// </summary>
        public int FeatTypeID {
            get {
                return _FeatTypeID;
            }
            set {
                _FeatTypeID = value;
            }
        }

        /// <summary>
        /// gets and sets the FeatType attribute for the Feat object
        /// </summary>
        [Display( Name = "Feat Type")]
        public FeatType FeatType {
            get {
                if(_FeatType == null && _FeatTypeID > 0) {
                    _FeatType = DAL.GetFeatType(_FeatTypeID);
                }
                return _FeatType;
            }
            set {
                _FeatType = value;
            }
        }
    }
}
