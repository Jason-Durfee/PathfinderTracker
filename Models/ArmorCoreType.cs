using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class ArmorCoreType : NamedObject
    {
        #region Constructors
        public ArmorCoreType(SqlDataReader dr) {
            ID = (int)dr["ArmorCoreTypeID"];
            Name = (string)dr["Name"];
        }

        public ArmorCoreType() {

        }
        #endregion
    }
}
