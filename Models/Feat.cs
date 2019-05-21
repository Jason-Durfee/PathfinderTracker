using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Feat : NamedObject
    {
        #region Constructors
        public Feat(SqlDataReader dr) {
            ID = (int)dr["FeatID"];
            Name = (string)dr["Name"];
            Description = (string)dr["Description"];
        }

        public Feat() {

        }
        #endregion

        private string _Description;


        /// <summary>
        /// gets and sets the Description attribute for the Feat object
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
