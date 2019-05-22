using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class Class : NamedObject
    {
        #region Constructors
        public Class(SqlDataReader dr) {
            ID = (int)dr["ClassID"];
            Name = (string)dr["Name"];
        }

        public Class() {

        }
        #endregion
    }
}
