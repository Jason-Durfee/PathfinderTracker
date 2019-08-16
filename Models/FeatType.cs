using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class FeatType : NamedObject
    {
        #region Constructors
        public FeatType(SqlDataReader dr) {
            ID = (int)dr["FeatTypeID"];
            Name = (string)dr["Name"];
        }

        public FeatType() {

        }
        #endregion
    }
}
