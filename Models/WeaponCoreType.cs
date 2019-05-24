using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class WeaponCoreType : NamedObject
    {
        #region Constructors
        public WeaponCoreType(SqlDataReader dr) {
            ID = (int)dr["WeaponCoreTypeID"];
            Name = (string)dr["Name"];
        }

        public WeaponCoreType() {

        }
        #endregion
    }
}
