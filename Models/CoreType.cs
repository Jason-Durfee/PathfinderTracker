using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class CoreType : NamedObject
    {
        #region Constructors
        public CoreType(SqlDataReader dr) {
            ID = (int)dr["CoreTypeID"];
            Name = (string)dr["Name"];
        }

        public CoreType() {

        }
        #endregion
    }
}
