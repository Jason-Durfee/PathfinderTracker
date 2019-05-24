using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class MagicSchool : NamedObject
    {
        #region Constructors
        public MagicSchool(SqlDataReader dr) {
            ID = (int)dr["MagicSchoolID"];
            Name = (string)dr["Name"];
            Description = (string)dr["Description"];
        }

        public MagicSchool() {

        }
        #endregion

        private string _Description;


        /// <summary>
        /// gets and sets the Description attribute for the MagicSchool object
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
    }
}
