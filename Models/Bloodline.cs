using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class Bloodline : NamedObject
    {
        #region Constructors
        public Bloodline(SqlDataReader dr) {
            ID = (int)dr["BloodlineID"];
            Name = (string)dr["Name"];
            Description = (string)dr["Description"];
        }

        public Bloodline() {

        }
        #endregion

        private string _Description;


        /// <summary>
        /// gets and sets the Description attribute for the Bloodline object
        /// </summary>
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
