using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SubClass : NamedObject
    {
        #region Constructors
        public SubClass(SqlDataReader dr) {
            ID = (int)dr["SubClassID"];
            Name = (string)dr["Name"];
            Description = (string)dr["Description"];
        }

        public SubClass() {

        }
        #endregion

        private string _Description;


        /// <summary>
        /// gets and sets the Description attribute for the SubClass object
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
