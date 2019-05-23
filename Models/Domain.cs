using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class Domain : NamedObject
    {
        #region Constructors
        public Domain(SqlDataReader dr) {
            ID = (int)dr["DomainID"];
            Name = (string)dr["Name"];
            Description = (string)dr["Description"];
        }

        public Domain() {

        }
        #endregion

        private string _Description;


        /// <summary>
        /// gets and sets the Description attribute for the Domain object
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
